using System;
using System.IO;
using System.Linq;
using Assets.BionicVisionVR.Coding.Structs;
using BionicVisionVR.Coding.Structs;
using BionicVisionVR.Resources;
using BionicVisionVR.Structs;
using UnityEngine;
using UnityEngine.UI;
using Tobii.XR;
using Random = UnityEngine.Random;

/// <summary>
/// Attach this to your VR Camera.
/// Initializes and runs all required shaders for SPV
/// </summary>
public class BackendShaderHandler : MonoBehaviour {
    public static BackendShaderHandler Instance { get; private set; }
    
    public RawImage pleaseWait;
    public Electrode[] electrodes;
    private Electrode[] allMaxElectrodes; 
    public AxonMap axonMap = new AxonMap();
    public PulseTrain pulseTrain;
    public ComputeBuffer axonSegmentGaussToElectrodes;
    [SerializeField] private Camera vrCamera;
    public float simulation_xMin;
    public float simulation_xMax;
    public float simulation_yMin;
    public float simulation_yMax;
    public float simulation_xyStep;
    public int simulationAreaArrayLength;
    public int xResolution;
    public int yResolution;
    
    private UnitConverter unitConverter = new UnitConverter();
    private ElectrodesHandler electrodesHandler = new ElectrodesHandler();
    private PulseTrainHandler pulseTrainHandler = new PulseTrainHandler();
    private AxonMapHandler axonMapHandler = new AxonMapHandler();
    
    private ComputeBuffer electrodesBuffer ;
    private ComputeBuffer simulationVariablesBuffer ;
    private ComputeBuffer axonContributionBuffer ;
    private ComputeBuffer axonIdxStartBuffer;
    private ComputeBuffer axonIdxEndBuffer;
    
	private RenderTexture processedTexture; 
	private RenderTexture temp; 
	private RenderTexture blackScreen; 

    private int currentFrame = 0;
    private int debugFrame = 0;
    private int[] randomArray;
    private PreDefinedBlocks lastPredefinedBlock = new PreDefinedBlocks();
    private Vector3 _lastGazeDirection;

	private int startingResX;
	private int startingResY; 

	private double lastDisplayFrame;
    private double minimumFrameTime = 1.0 / 60.0;
    private bool displayed = false;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        startingResX = source.width;
        startingResY = source.height; 
        
        if (VariableManagerScript.Instance.temporalModel == TemporalModels.Pulsated &&
            Time.realtimeSinceStartupAsDouble - lastDisplayFrame < VariableManagerScript.Instance.pulsatedWaitTime)
        {
            if (Time.realtimeSinceStartup - lastDisplayFrame > minimumFrameTime && displayed)
                Graphics.Blit(blackScreen, destination);
            else
            {
                Graphics.Blit(processedTexture, destination);
                displayed = true;
                //Debug.Log("Displayed: "+Time.realtimeSinceStartupAsDouble); 
            }

            currentFrame++; 
		}
		else{
            if (VariableManagerScript.Instance.runShaders)
            {
                lastDisplayFrame = Time.realtimeSinceStartupAsDouble;
                RenderTexture.ReleaseTemporary(processedTexture); 
           	    if (currentFrame == 0)
                    RunStartup();

           	    if (!(lastPredefinedBlock.Equals(VariableManagerScript.Instance.predefinedSettings)) && VariableManagerScript.Instance.usePreDefinedBlock)
                    UpdateConfiguration();
                
           	    SetShaderVariables();
                
           	    PerformDownscale(source); 

			    RunPreprocessing();

                RandomizeElectrodes(); 

                GeneratePercepts(); 
                
                GazeLock();
                
                BlurFinal();

                ElectrodeDebug();
                Graphics.Blit(processedTexture, destination);
                displayed = false; 
                currentFrame++;
			    lastDisplayFrame = Time.realtimeSinceStartupAsDouble; 
                //Debug.Log(Time.realtimeSinceStartupAsDouble); 
                lastPredefinedBlock = VariableManagerScript.Instance.predefinedSettings;
            }

            else
                Graphics.Blit(source,destination);
		}
    }

    private void RunStartup()
    {
        pleaseWait.enabled = true;
        GetPredefinedBlockSettings();
        SetSimulationBounds(); 

		blackScreen = new RenderTexture(startingResX, startingResY, 0); 
		
        if (VariableManagerScript.Instance.useTemporal)
            pulseTrainHandler.SetPulseTrain();

        LoadMapping(); 

        //Convert retinal coordinates to screen positions for use in shader:
        if (VariableManagerScript.Instance.debugMode)
            axonMapHandler.AxonSegmentsToScreenPosCoords();
        electrodesHandler.ElectrodeGridToScreenPosCoords();
        
        InitializeBuffers();
                    
        SetRandomizerArray();
                    
        GC.Collect();
        pleaseWait.enabled = false;
    }

    private void SetSimulationBounds()
    {
        xResolution =
            (int) Math.Floor(
                (double) startingResX / (double) VariableManagerScript.Instance.downscaleFactor);
        yResolution = (int) Math.Floor((double) startingResY /
                                       (double) VariableManagerScript.Instance.downscaleFactor);

        Debug.Log("X-res" + xResolution);
        Debug.Log("Y-res" + yResolution);

        simulation_xyStep =
            VariableManagerScript.Instance.headset_fov / xResolution;

        float xCenter = VariableManagerScript.Instance.xPosition;
        int numberStepsPerDirection =
            (int) ((VariableManagerScript.Instance.implant_fov /
                    (2 * simulation_xyStep)) - 1);

        simulation_xMin =
            (xCenter - .5f * simulation_xyStep) -
            (simulation_xyStep * (numberStepsPerDirection));
        simulation_xMax =
            (xCenter + .5f * simulation_xyStep) +
            (simulation_xyStep * (numberStepsPerDirection));
        float yCenter = VariableManagerScript.Instance.yPosition;
        numberStepsPerDirection = (int) ((VariableManagerScript.Instance.implant_fov /
                                          (2 * simulation_xyStep)) - 1);

        simulation_yMin =
            (yCenter - .5f * simulation_xyStep) -
            (simulation_xyStep * (numberStepsPerDirection));
        simulation_yMax =
            (yCenter + .5f * simulation_xyStep) +
            (simulation_xyStep * (numberStepsPerDirection));
    }

    private void SetRandomizerArray()
    {
        randomArray = Enumerable.Range(0, electrodes.Length).ToArray();
        for (int t = 0; t < randomArray.Length; t++)
        {
            int tmp = randomArray[t];
            int r = Random.Range(t, randomArray.Length);
            randomArray[t] = randomArray[r];
            randomArray[r] = tmp;
        }
    }

	private void LoadMapping()
	{
        VariableManagerScript.Instance.updateConfigurationPath();
		if (!VariableManagerScript.Instance.loadFromBinaries || !File.Exists(VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss"))
        {
            electrodesHandler.SetRectangularGrid();
            if (VariableManagerScript.Instance.useAxonMap)
            {
                axonMapHandler.SetAxonMap();
                axonMapHandler.SetElectrodeToAxonSegmentGauss();
            }
        }
        else
            LoadBinaryData();
	}

    private void SetShaderVariables()
    {
        VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetBuffer("electrodesBuffer", electrodesBuffer);
        VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetInt("numberElectrodes", electrodes.Length);
        VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetInt("xResolution", xResolution);
        VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetInt("yResolution", yResolution);
        VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetFloat("amplitude", VariableManagerScript.Instance.amplitude);

        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("debugMode",
            VariableManagerScript.Instance.debugMode ? 1 : 0);
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("showElectrodes",
            VariableManagerScript.Instance.showElectrodes ? 1 : 0);
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("numberAxonTraces",
            VariableManagerScript.Instance.debugAxonTraces);
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("specificTrace",
            VariableManagerScript.Instance.specificPixelToDebug);
        
        VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("electrodesBuffer", electrodesBuffer);
        VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("simulationVariables", simulationVariablesBuffer);

        if (VariableManagerScript.Instance.useAxonMap)
        {
            VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonSegmentGaussToElectrodesBuffer", axonSegmentGaussToElectrodes);
            VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonContributionBuffer", axonContributionBuffer);
            VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxStartBuffer", axonIdxStartBuffer);
            VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxEndBuffer", axonIdxEndBuffer);
            VariableManagerScript.Instance.perceptShaderMaterial.SetInt("axonBufferLength", axonMap.axonIdxStart.Length);
        }

        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("xResolution", xResolution); //(int) Math.Ceiling((double) xResolution / (double) VariableManagerScript.Instance.downscaleFactor));
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("yResolution", yResolution); //(int) Math.Ceiling((double) yResolution / (double) VariableManagerScript.Instance.downscaleFactor));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("xPixelSizeDivBy2", (float) (1.0f / (xResolution*2.0f)));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("yPixelSizeDivBy2", (float) (1.0f / (yResolution*2.0f)));

        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("numberElectrodes", (electrodes.Length));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("implant_fov", simulation_xMax +  -simulation_xMin);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("headset_fov", VariableManagerScript.Instance.headset_fov);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("rho", VariableManagerScript.Instance.rho);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("amplitudeMultiplier", VariableManagerScript.Instance.amplitude);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("threshold", VariableManagerScript.Instance.threshold);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("dt", VariableManagerScript.Instance.simulationTimeStep);
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("frameDur", (int)pulseTrain.timeStepsPerFrame);
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("minimumScreenPositionX", unitConverter.degreeToScreenPos(simulation_xMin));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("maximumScreenPositionX", unitConverter.degreeToScreenPos(simulation_xMax));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("minimumScreenPositionY", unitConverter.degreeToScreenPos(simulation_yMin));
        VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("maximumScreenPositionY", unitConverter.degreeToScreenPos(simulation_yMax));
        VariableManagerScript.Instance.perceptShaderMaterial.SetInt("simulatedColumns", (int) Math.Ceiling((double) yResolution ));
    }
      
    private void InitializeBuffers()
    {
        simulationAreaArrayLength =
            (int) (Math.Ceiling(VariableManagerScript.Instance.implant_fov * xResolution /
                                VariableManagerScript.Instance.headset_fov)) *
            (int) (Math.Ceiling(VariableManagerScript.Instance.implant_fov * yResolution /
                                VariableManagerScript.Instance.headset_fov));
        Debug.Log("Sim size: " + simulationAreaArrayLength);

        electrodesBuffer =
            new ComputeBuffer(electrodes.Length,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
        Graphics.SetRandomWriteTarget(2, electrodesBuffer, true);
        electrodesBuffer.SetData(electrodes);

        if (VariableManagerScript.Instance.useTemporal)
        {
            SimulationVariables[] simulationVariableArray = new SimulationVariables[simulationAreaArrayLength];
            for (int i = 0; i < simulationAreaArrayLength; i++)
                simulationVariableArray[i] = new SimulationVariables(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

            simulationVariablesBuffer = new ComputeBuffer(simulationAreaArrayLength,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(SimulationVariables)), ComputeBufferType.Default);
            Graphics.SetRandomWriteTarget(1, simulationVariablesBuffer, true);
            simulationVariablesBuffer.SetData(simulationVariableArray);
        }

        if (VariableManagerScript.Instance.useAxonMap)
        {
            axonContributionBuffer =
                new ComputeBuffer(axonMap.axonSegmentContributions.Length,
                    System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)), ComputeBufferType.Default);
            axonContributionBuffer.SetData(axonMap.axonSegmentContributions);

            axonIdxStartBuffer = new ComputeBuffer(axonMap.axonIdxStart.Length,System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)), ComputeBufferType.Default);
            axonIdxStartBuffer.SetData(axonMap.axonIdxStart);
            
            axonIdxEndBuffer = new ComputeBuffer(axonMap.axonIdxEnd.Length,System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)), ComputeBufferType.Default);
            axonIdxEndBuffer.SetData(axonMap.axonIdxEnd);
            
        }
        
        Debug.Log("X: " + simulation_xMin + "," + simulation_xMax);
        Debug.Log("Y: " + simulation_yMin + "," + simulation_yMax);

    }

	private void RandomizeElectrodes(){
 		if (VariableManagerScript.Instance.randomShuffle)
        {
            var shuffleIndexBuffer = new ComputeBuffer(electrodes.Length, sizeof(int));
            var processedTextureBuffer = new ComputeBuffer(electrodes.Length, sizeof(float));
            var shuffleKernel = VariableManagerScript.Instance.randomShuffler.FindKernel("CSMain");
            
            shuffleIndexBuffer.SetData(randomArray);
            VariableManagerScript.Instance.randomShuffler.SetInt("numberElectrodes", electrodes.Length);
            VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"electrodesBuffer", electrodesBuffer);
            VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"randomIndex", shuffleIndexBuffer);
            VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"processedTextureBuffer", processedTextureBuffer);
            
            VariableManagerScript.Instance.randomShuffler.Dispatch(shuffleKernel, electrodes.Length/1024 + 1 , 1, 1);
            
            processedTextureBuffer.Release();
            shuffleIndexBuffer.Release();
        }
	}

	private void PerformDownscale(RenderTexture source)
	{
		processedTexture = RenderTexture.GetTemporary((int) source.width/VariableManagerScript.Instance.downscaleFactor, 
        (int) source.height/VariableManagerScript.Instance.downscaleFactor, 0); // create new texture with dimensions from screen's render texture
		Graphics.Blit(source, processedTexture);
	}

	private void RunPreprocessing()
	{
        if (VariableManagerScript.Instance.allElectrodesMax)
            electrodesBuffer.SetData(allMaxElectrodes);
        else
        {
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0);
            Graphics.Blit(temp, processedTexture, VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor]);
            RenderTexture.ReleaseTemporary(temp);
        }
    }

    private void GeneratePercepts(){
        if (VariableManagerScript.Instance.useBionicVisionShader)
        {
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0); 
            Graphics.Blit(temp, processedTexture, VariableManagerScript.Instance.perceptShaderMaterial);
            RenderTexture.ReleaseTemporary(temp); 
        }
    }

	private void GazeLock()
	{
		if (VariableManagerScript.Instance.gazeLock) 
        {
            temp = processedTexture;
			processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0); 

            var provider = TobiiXR.Internal.Provider;
            var eyeTrackingData = new TobiiXR_EyeTrackingData();
            provider.GetEyeTrackingDataLocal(eyeTrackingData);

            var interpolatedGazeDirection = Vector3.Lerp(_lastGazeDirection, eyeTrackingData.GazeRay.Direction, 
                VariableManagerScript.Instance.smoothMoveSpeed * Time.unscaledDeltaTime);
            var usedDirection = VariableManagerScript.Instance.smoothMove ? interpolatedGazeDirection.normalized : eyeTrackingData.GazeRay.Direction.normalized;
            _lastGazeDirection = usedDirection; 

            var screenPos = vrCamera.WorldToScreenPoint(vrCamera.transform.position + vrCamera.transform.rotation * usedDirection);

            VariableManagerScript.Instance.gazeLockShader.SetFloat("gazeY", screenPos.y/startingResY);
            VariableManagerScript.Instance.gazeLockShader.SetFloat("gazeX", screenPos.x/startingResX);

            Graphics.Blit(temp, processedTexture, VariableManagerScript.Instance.gazeLockShader);
            RenderTexture.ReleaseTemporary(temp); 
        }
	}

    private void BlurFinal()
    {
        if(VariableManagerScript.Instance.blurFinalImage){
            VariableManagerScript.Instance.blurShader.SetInt("_KernelSize", VariableManagerScript.Instance.blurIntensity);
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(startingResX*2, startingResY*2, 0);
            Graphics.Blit(temp, processedTexture, VariableManagerScript.Instance.blurShader);
            RenderTexture.ReleaseTemporary(temp);

            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(startingResX, startingResY, 0);
            Graphics.Blit(temp, processedTexture, VariableManagerScript.Instance.blurShader);
            RenderTexture.ReleaseTemporary(temp);
        }
    }

	private void ElectrodeDebug()
	{
        if (VariableManagerScript.Instance.debugMode)
        {
            electrodesBuffer.GetData(electrodes);
            if (currentFrame == debugFrame)
            {
                for (int i = 0; i < electrodes.Length; i++)
                {
                    Debug.Log(i + ":  " + electrodes[i].xPosition + "," +
                              electrodes[i].yPosition + " -- " +
                              electrodes[i].current);
                }

                debugFrame += 250;
            }
        }
	}

    private void LoadBinaryData()
      {
          pleaseWait.enabled = true; 
          BinaryHandler binaryHandler = new BinaryHandler();
          VariableManagerScript.Instance.updateConfigurationPath();
                   
          float[] axonSegmentToElectrodes =
              binaryHandler.ReadFloatsFromBinaryFile(
                  VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss").ToArray();
          axonSegmentGaussToElectrodes = new ComputeBuffer(axonSegmentToElectrodes.Length,
              System.Runtime.InteropServices.Marshal.SizeOf(typeof(float)), ComputeBufferType.Default);
          axonSegmentGaussToElectrodes.SetData(axonSegmentToElectrodes); 
                    
          axonMap = binaryHandler.ReadAxonMap(VariableManagerScript.Instance.configurationPath);
          axonContributionBuffer =
              new ComputeBuffer(axonMap.axonSegmentContributions.Length,
                  System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)), ComputeBufferType.Default);
          axonContributionBuffer.SetData(axonMap.axonSegmentContributions);
          
          electrodes = binaryHandler.ReadElectrodeLocations(VariableManagerScript.Instance.configurationPath);
          electrodesBuffer =
              new ComputeBuffer(electrodes.Length,
                  System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
          Graphics.SetRandomWriteTarget(2, electrodesBuffer, true);
          electrodesBuffer.SetData(electrodes);

          pleaseWait.enabled = false; 
      }

      public void GetPredefinedBlockSettings()
      {
          if (VariableManagerScript.Instance.usePreDefinedBlock)
          {
              BlockSettings blockSettings =
                  BlockSettings.GetPreDefinedBlockSettings(VariableManagerScript.Instance.predefinedSettings);
              VariableManagerScript.Instance.rho = blockSettings.rho;
              VariableManagerScript.Instance.lambda = blockSettings.lambda;
              VariableManagerScript.Instance.numberXelectrodes = blockSettings.xElectrodeCount;
              VariableManagerScript.Instance.numberYelectrodes = blockSettings.yElectrodeCount;
              VariableManagerScript.Instance.electrodeSpacing = blockSettings.electrodeSpacing;
              VariableManagerScript.Instance.xPosition = blockSettings.xPosition;
              VariableManagerScript.Instance.yPosition = blockSettings.yPosition;
              VariableManagerScript.Instance.rotation = blockSettings.rotation;
          }
      }

      public void UpdateConfiguration()
      {
          GetPredefinedBlockSettings();
          pleaseWait.enabled = true; 
          Debug.Log("UPDATE CALLED"); 
          VariableManagerScript.Instance.updateConfigurationPath();
          
          if (!VariableManagerScript.Instance.loadFromBinaries || !File.Exists(VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss"))
          {
              electrodesHandler.SetRectangularGrid();
              if (VariableManagerScript.Instance.useAxonMap)
              {
                  axonMapHandler.SetAxonMap();
                  axonMapHandler.SetElectrodeToAxonSegmentGauss();
              }
          }
          else
          {
              LoadBinaryData();
          }

          if (VariableManagerScript.Instance.debugMode)
              axonMapHandler.AxonSegmentsToScreenPosCoords();
          electrodesHandler.ElectrodeGridToScreenPosCoords(); 
          InitializeBuffers();
          
          VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetBuffer("electrodesBuffer", electrodesBuffer);
          VariableManagerScript.Instance.preprocessingShaderMaterial[VariableManagerScript.Instance.whichPreprocessor].SetInt("numberElectrodes", electrodes.Length);
          
          VariableManagerScript.Instance.perceptShaderMaterial.SetInt("numberElectrodes", (electrodes.Length));
          VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("rho", VariableManagerScript.Instance.rho);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonSegmentGaussToElectrodesBuffer", axonSegmentGaussToElectrodes);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonContributionBuffer", axonContributionBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxStartBuffer", axonIdxStartBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxEndBuffer", axonIdxEndBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetInt("axonBufferLength", axonMap.axonIdxStart.Length);
          pleaseWait.enabled = false; 
          
          allMaxElectrodes = electrodes;
          for (int i = 0; i < allMaxElectrodes.Length; i++)
          {
              allMaxElectrodes[i].current = 1; 
          }
      }

    private void OnApplicationQuit()
    {
        if(electrodesBuffer != null && electrodesBuffer.IsValid()){
            electrodesBuffer.Release();
        }
        if (simulationVariablesBuffer != null && simulationVariablesBuffer.IsValid())
        {
            simulationVariablesBuffer.Release();
        }
        if (VariableManagerScript.Instance.useAxonMap && axonContributionBuffer != null && axonContributionBuffer.IsValid())
        {
            axonContributionBuffer.Release();
        }
    }
    
    private void Awake()
    {

        if ( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }
    }

}
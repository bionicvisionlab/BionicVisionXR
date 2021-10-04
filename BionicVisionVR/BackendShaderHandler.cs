using System;
using System.IO;
using System.Linq;
using Assets.BionicVisionVR.Coding.Structs;
using BionicVisionVR.Coding.Structs;
using BionicVisionVR.Resources;
using BionicVisionVR.Structs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BackendShaderHandler : MonoBehaviour { 

    public static BackendShaderHandler Instance { get; private set; }

    public Camera mainCamera;
    public Camera miniCamera;
 

    public RawImage pleaseWait; 
    
    public Electrode[] electrodes;
    public AxonMap axonMap = new AxonMap();
    public PulseTrain pulseTrain;
    //public float[] axonSegmentGaussToElectrodes;
    
    public ComputeBuffer axonSegmentGaussToElectrodes;

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
    
    private int currentFrame = 0;
    private int debugFrame = 0;
    private int[] randomArray;
    private PreDefinedBlocks lastPredefinedBlock = new PreDefinedBlocks(); 

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

    }
    
     private void SetShaderVariables()
    {
        VariableManagerScript.Instance.preprocessingShaderMaterial.SetBuffer("electrodesBuffer", electrodesBuffer);
        VariableManagerScript.Instance.preprocessingShaderMaterial.SetInt("numberElectrodes", electrodes.Length);
        VariableManagerScript.Instance.preprocessingShaderMaterial.SetInt("xResolution", xResolution);
        VariableManagerScript.Instance.preprocessingShaderMaterial.SetInt("yResolution", yResolution);

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
     
      private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (VariableManagerScript.Instance.runShaders)
        {
            if (currentFrame == 0)
            {

                GetPredefinedBlockSettings();

                    pleaseWait.enabled = true;

                    xResolution =
                        (int) Math.Floor(
                            (double) source.width / (double) VariableManagerScript.Instance.downscaleFactor);
                    yResolution = (int) Math.Floor((double) source.height /
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

                    if (VariableManagerScript.Instance.useTemporal)
                        pulseTrainHandler.SetPulseTrain();

                    if (!VariableManagerScript.Instance.loadFromBinaries ||
                        !File.Exists(VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss"))
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

                    //Convert retinal coordinates to screen positions for use in shader:
                    if (VariableManagerScript.Instance.debugMode)
                        axonMapHandler.AxonSegmentsToScreenPosCoords();
                    electrodesHandler.ElectrodeGridToScreenPosCoords();


                    InitializeBuffers();
                    Debug.Log("X: " + simulation_xMin + "," +
                              simulation_xMax);
                    Debug.Log("Y: " + simulation_yMin + "," +
                              simulation_yMax);

                    GC.Collect();
                    pleaseWait.enabled = false;

                    randomArray = Enumerable.Range(0, electrodes.Length).ToArray();
                    for (int t = 0; t < randomArray.Length; t++)
                    {
                        int tmp = randomArray[t];
                        int r = Random.Range(t, randomArray.Length);
                        randomArray[t] = randomArray[r];
                        randomArray[r] = tmp;
                    }
                    
                    Debug.Log("Starting, cancel now to avoid resets"); 
                    new WaitForSeconds(5);
                    
                }


                if (!(lastPredefinedBlock.Equals(VariableManagerScript.Instance.predefinedSettings)) && VariableManagerScript.Instance.usePreDefinedBlock)
                {
                    UpdateConfiguration(
                        BlockSettings.GetPreDefinedBlockSettings(VariableManagerScript.Instance.predefinedSettings));
                    
                }

//*******************************************************************************************************//
            SetShaderVariables();
            RenderTexture temp = RenderTexture.GetTemporary((int) source.width/VariableManagerScript.Instance.downscaleFactor, 
                (int) source.height/VariableManagerScript.Instance.downscaleFactor, 0); // create new texture with dimensions from screen's render texture
            Graphics.Blit(source, temp);
            
            RenderTexture temp2 = RenderTexture.GetTemporary((int) temp.width, temp.height, 0);
            Graphics.Blit(temp, temp2, VariableManagerScript.Instance.preprocessingShaderMaterial);
            RenderTexture.ReleaseTemporary(temp);


            // TODO: Add computer shader that randomize the electrodesBuffer
            if (VariableManagerScript.Instance.randomShuffle)
            {
               
                var shuffleIndexBuffer = new ComputeBuffer(electrodes.Length, sizeof(int));
                var tempBuffer = new ComputeBuffer(electrodes.Length, sizeof(float));
                var shuffleKernel = VariableManagerScript.Instance.randomShuffler.FindKernel("CSMain");
                
                shuffleIndexBuffer.SetData(randomArray);
                VariableManagerScript.Instance.randomShuffler.SetInt("numberElectrodes", electrodes.Length);
                VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"electrodesBuffer", electrodesBuffer);
                VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"randomIndex", shuffleIndexBuffer);
                VariableManagerScript.Instance.randomShuffler.SetBuffer(shuffleKernel,"tempBuffer", tempBuffer);
                
                VariableManagerScript.Instance.randomShuffler.Dispatch(shuffleKernel, electrodes.Length/1024 + 1 , 1, 1);
                
                tempBuffer.Release();
                shuffleIndexBuffer.Release();
            }


            if (VariableManagerScript.Instance.useBionicVisionShader)
            {
                // Run percept shader and save it to temp3, upsample to same size as original texture
                RenderTexture temp3 = RenderTexture.GetTemporary((int)temp2.width, temp2.height, 0);
                Graphics.Blit(temp2, temp3, VariableManagerScript.Instance.perceptShaderMaterial);
                RenderTexture.ReleaseTemporary(temp2);

                if (VariableManagerScript.Instance.blurFinalImage)
                {
                    RenderTexture temp4 = RenderTexture.GetTemporary((int)source.width, source.height, 0);
                    Graphics.Blit(temp3, temp4);
                    RenderTexture.ReleaseTemporary(temp3);
                    Graphics.Blit(temp4, destination, VariableManagerScript.Instance.blurShader);
                    RenderTexture.ReleaseTemporary(temp4);
                }
                else
                {
                    Graphics.Blit(temp3, destination);  
                    RenderTexture.ReleaseTemporary(temp3); 
                }
            }
            else
            {
                Graphics.Blit(temp2, destination); 
                RenderTexture.ReleaseTemporary(temp2);
            }

            //**DISPLAY ELECTRODE INFO**//
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

            currentFrame++;
            lastPredefinedBlock = VariableManagerScript.Instance.predefinedSettings; 
        }
        else
        {
            Graphics.Blit(source,destination);
        }
    }

      private void LoadBinaryData()
      {
          pleaseWait.enabled = true; 
          BinaryHandler binaryHandler = new BinaryHandler();
                   
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
          BlockSettings blockSettings = BlockSettings.GetPreDefinedBlockSettings(VariableManagerScript.Instance.predefinedSettings);
          VariableManagerScript.Instance.rho = blockSettings.rho;
          VariableManagerScript.Instance.lambda = blockSettings.lambda;
          VariableManagerScript.Instance.numberXelectrodes = blockSettings.xElectrodeCount;
          VariableManagerScript.Instance.numberYelectrodes = blockSettings.yElectrodeCount;
          VariableManagerScript.Instance.electrodeSpacing = blockSettings.electrodeSpacing;
          VariableManagerScript.Instance.xPosition = blockSettings.xPosition;
          VariableManagerScript.Instance.yPosition = blockSettings.yPosition;
          VariableManagerScript.Instance.rotation = blockSettings.rotation;
      }

      public void UpdateConfiguration(BlockSettings settings)
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
          
          VariableManagerScript.Instance.preprocessingShaderMaterial.SetBuffer("electrodesBuffer", electrodesBuffer);
          VariableManagerScript.Instance.preprocessingShaderMaterial.SetInt("numberElectrodes", electrodes.Length);
          
          VariableManagerScript.Instance.perceptShaderMaterial.SetInt("numberElectrodes", (electrodes.Length));
          VariableManagerScript.Instance.perceptShaderMaterial.SetFloat("rho", VariableManagerScript.Instance.rho);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonSegmentGaussToElectrodesBuffer", axonSegmentGaussToElectrodes);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonContributionBuffer", axonContributionBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxStartBuffer", axonIdxStartBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetBuffer("axonIdxEndBuffer", axonIdxEndBuffer);
          VariableManagerScript.Instance.perceptShaderMaterial.SetInt("axonBufferLength", axonMap.axonIdxStart.Length);
          pleaseWait.enabled = false; 
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
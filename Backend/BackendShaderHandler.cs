using System;
using System.IO;
using System.Linq;
using BionicVisionVR.Backend.Resources;
using BionicVisionVR.Backend.Structs;
using BionicVisionVR.Coding.Structs;
using BionicVisionVR.Resources;
using BionicVisionVR.Structs;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// [Singleton] Initializes and runs all required shaders for SPV
/// Attach to the VR Camera.
/// Uses RasterizationHandler 'rh'
/// Uses VariableManager 'vm'
///     void OnRenderImage(RenderTexture source, RenderTexture destination)
///     void SetSimulationBounds()
///     void SetRandomizerArray()
///     void SetShaderVariables()
///     void SetRasterGroups()
///     void InitializeBuffers()
///     void RandomizeElectrodes()
///     void PerformDownscale(RenderTexture source)
///     void RasterizeElectrodes()
///     void RunPreprocessing()
///     void GeneratePercepts()
///     void RunTemporal()
///     void GetGazeScreenPos()
///     void BlurFinal()
///     void ElectrodeDebug()
///     void LoadBinaryData()
///     void RunShaderAfterFrames
///     void UpdateAfterFrames()
///     void UpdateConfiguration
///     void OnApplicationQuit()
/// On Awake:
///     Initializes singleton and buffers
/// On Start:
///     Creates references to RasterizationHandler and VariableManager
/// On Update:
///     Moves to VR camera location
/// </summary>
public class BackendShaderHandler : MonoBehaviour {
    public Electrode[] electrodes;
    
    private TemporalVariables[] tempVars;
    private ComputeBuffer tempVarsBuffer;  

    private Electrode[] allMaxElectrodes; 
    public AxonMap axonMap = new AxonMap();
    public ComputeBuffer axonSegmentGaussToElectrodes; 
    public Camera vrCamera, monitorCamera;
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
    private AxonMapHandler axonMapHandler = new AxonMapHandler();
    private RasterizationHandler rh; 
    
    private ComputeBuffer electrodesBuffer; 
    private ComputeBuffer simulationVariablesBuffer;

    private ComputeBuffer axonContributionBuffer; 
    private ComputeBuffer axonIdxStartBuffer; 
    private ComputeBuffer axonIdxEndBuffer; 
    
	private RenderTexture processedTexture; 
	private RenderTexture temp;

    private float lastRasterChange = 0.0f;
    private int currentRasterGroup = 0;

    private int currentFrame = 0;
    private int debugFrame = 250;
    private int[] randomArray;
    private Vector3 _lastGazeDirection;

	private int startingResX;
	private int startingResY;
    public bool resetTemporalValues;

    private Vector3 screenPos; 
    
    private int framesToWait = 5;
    private int frameDelay = 5;

    private int framesToWaitRunShaders = 5;
    private int framesDelayRunShaders = 5; 
    
    private VariableManagerScript vm; // Initialized at start to the singleton instance

    /// <summary>
    /// Calls all the shader functions and blits final processed texture to destination
    /// </summary>
    /// <param name="source">A RenderTexture containing the source image</param>
    /// <param name="destination">The RenderTexture to update with the modified image</param>
    /// <returns></returns>
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if ((vm.OverRideRunShaders & vm.OverRiddenRunShaders) || (vm.runShaders & !vm.OverRideRunShaders)){
            if (currentFrame == 0) { 
                startingResX = source.width;
                startingResY = source.height; 
                SetSimulationBounds(); }
            
            RenderTexture.ReleaseTemporary(processedTexture);

            if (vm.CheckForUpdate() || (framesToWait != frameDelay )) {
                UpdateAfterFrames(); // Insures the loading screen is loaded before doing calculations
                return; }

            SetShaderVariables();
            
           	PerformDownscale(source);

            RasterizeElectrodes();
            
            GetGazeScreenPos();

            BlurImage(false); 
            
			RunPreprocessing();

            RandomizeElectrodes(); 

            GeneratePercepts();

            GazeLock();
            
            RunTemporal(); 
             
            BlurImage(true);

            ElectrodeDebug();
            Graphics.Blit(processedTexture, destination);

            currentFrame++; 
        } else
            Graphics.Blit(source,destination); }

    /// <summary>
    /// Handles turning the cropped and downscaled image into a grid
    /// lying between simulation_xMin/y_min to simulation_xMax/y_max.
    /// xy_Step is based on the field of view specified
    /// </summary>
    private void SetSimulationBounds() {
        xResolution =
            (int) Math.Floor(
                (double) startingResX / (double) vm.downscaleFactor);
        yResolution = (int) Math.Floor((double) startingResY /
                                       (double) vm.downscaleFactor);

        ErrorDebug.Log("X-res" + xResolution);
        ErrorDebug.Log("Y-res" + yResolution);

        simulation_xyStep =
            vm.headset_fov / xResolution;

        float xCenter = vm.xPosition;
        int numberStepsPerDirection =
            (int) ((vm.implant_fov /
                    (2 * simulation_xyStep)) - 1);

        simulation_xMin =
            (xCenter - .5f * simulation_xyStep) -
            (simulation_xyStep * (numberStepsPerDirection));
        simulation_xMax =
            (xCenter + .5f * simulation_xyStep) +
            (simulation_xyStep * (numberStepsPerDirection));
        float yCenter = vm.yPosition;
        numberStepsPerDirection = (int) ((vm.implant_fov /
                                          (2 * simulation_xyStep)) - 1);

        simulation_yMin =
            (yCenter - .5f * simulation_xyStep) -
            (simulation_xyStep * (numberStepsPerDirection));
        simulation_yMax =
            (yCenter + .5f * simulation_xyStep) +
            (simulation_xyStep * (numberStepsPerDirection)); }

    /// <summary>
    /// Generates a randomly ordered array of ints from 0 thru electrodes.Length-1
    /// </summary>
    private void SetRandomizerArray() {
        randomArray = Enumerable.Range(0, electrodes.Length).ToArray();
        for (int t = 0; t < randomArray.Length; t++) {
            int tmp = randomArray[t];
            int r = Random.Range(t, randomArray.Length);
            randomArray[t] = randomArray[r];
            randomArray[r] = tmp; } }
    /// <summary>
    /// Sets values of the shader material being used by VariableManager
    /// </summary>
    private void SetShaderVariables() {
        vm.preprocessingShaderMaterial[vm.whichPreprocessor]
            .SetBuffer("electrodesBuffer", electrodesBuffer);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor]
            .SetInt("numberElectrodes",  rh.currentRasterType!= RasterizationHandler.RasterType.None ? 
                electrodes.Length/rh.rasterizeGroups : electrodes.Length);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor]
            .SetInt("xResolution", xResolution);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor]
            .SetInt("yResolution", yResolution);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor]
            .SetFloat("amplitude", vm.amplitude);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor].SetInt("invert", vm.invertPreprocessing?1:0);

        vm.preprocessingShaderMaterial[vm.whichPreprocessor].SetFloat("gazeShiftX", screenPos.x/startingResX);
        vm.preprocessingShaderMaterial[vm.whichPreprocessor].SetFloat("gazeShiftY", screenPos.y/startingResY);

        vm.perceptShaderMaterial.SetInt("debugMode",
        vm.debugMode ? 1 : 0);
        vm.perceptShaderMaterial.SetInt("showElectrodes",
            vm.showElectrodes ? 1 : 0);
        vm.perceptShaderMaterial.SetInt("numberAxonTraces",
            vm.debugAxonTraces);
        vm.perceptShaderMaterial.SetInt("specificTrace",
            vm.specificPixelToDebug);
        
        vm.perceptShaderMaterial.SetBuffer("electrodesBuffer", electrodesBuffer);
        vm.perceptShaderMaterial.SetBuffer("simulationVariables", simulationVariablesBuffer);

        if (vm.useAxonMap) {
            vm.perceptShaderMaterial.SetBuffer("axonSegmentGaussToElectrodesBuffer", axonSegmentGaussToElectrodes);
            vm.perceptShaderMaterial.SetBuffer("axonContributionBuffer", axonContributionBuffer);
            vm.perceptShaderMaterial.SetBuffer("axonIdxStartBuffer", axonIdxStartBuffer);
            vm.perceptShaderMaterial.SetBuffer("axonIdxEndBuffer", axonIdxEndBuffer);
            vm.perceptShaderMaterial.SetInt("axonBufferLength", axonMap.axonIdxStart.Length); }

        vm.perceptShaderMaterial.SetInt("xResolution", xResolution); 
        vm.perceptShaderMaterial.SetInt("yResolution", yResolution); 
        vm.perceptShaderMaterial.SetFloat("xPixelSizeDivBy2", (float) (1.0f / (xResolution*2.0f)));
        vm.perceptShaderMaterial.SetFloat("yPixelSizeDivBy2", (float) (1.0f / (yResolution*2.0f)));


        vm.perceptShaderMaterial.SetInt("numberRastGroups", rh.currentRasterType!=RasterizationHandler.RasterType.None ? rh.rasterizeGroups : 1);
        vm.perceptShaderMaterial.SetInt("numberElectrodes", rh.currentRasterType!=RasterizationHandler.RasterType.None ? electrodes.Length/rh.rasterizeGroups : electrodes.Length);
        vm.perceptShaderMaterial.SetFloat("implant_fov", simulation_xMax +  -simulation_xMin);
        vm.perceptShaderMaterial.SetFloat("headset_fov", vm.headset_fov);
        vm.perceptShaderMaterial.SetFloat("rho", vm.rho);
        vm.perceptShaderMaterial.SetFloat("amplitude", vm.amplitude);
        vm.perceptShaderMaterial.SetFloat("threshold", vm.threshold);
        vm.perceptShaderMaterial.SetFloat("minimumScreenPositionX", unitConverter.degreeToScreenPos(simulation_xMin));
        vm.perceptShaderMaterial.SetFloat("maximumScreenPositionX", unitConverter.degreeToScreenPos(simulation_xMax));
        vm.perceptShaderMaterial.SetFloat("minimumScreenPositionY", unitConverter.degreeToScreenPos(simulation_yMin));
        vm.perceptShaderMaterial.SetFloat("maximumScreenPositionY", unitConverter.degreeToScreenPos(simulation_yMax));
        vm.perceptShaderMaterial.SetInt("simulatedColumns", (int) Math.Ceiling((double) yResolution )); }

    /// <summary>
    /// Sets raster groups in RasterizationHandler
    /// </summary>
    private void SetRasterGroups() {
        rh.SetRasterGroups();

        var newArray = new Electrode[electrodes.Length/rh.rasterizeGroups];
        Array.Copy(allMaxElectrodes, 1, newArray, 0, newArray.Length); }
    
    /// <summary>
    /// Sets up the electrodes buffer
	/// When relevant, also sets up raster groups, temporal buffers and axon map buffers.
    /// </summary>
    private void InitializeBuffers() {
        simulationAreaArrayLength =
            (int) (Math.Ceiling(vm.implant_fov * xResolution /
                                vm.headset_fov)) *
            (int) (Math.Ceiling(vm.implant_fov * yResolution /
                                vm.headset_fov));
        ErrorDebug.Log("Sim size: " + simulationAreaArrayLength);

        if (rh.currentRasterType!=RasterizationHandler.RasterType.None) {
            SetRasterGroups();
            electrodesBuffer.Dispose();
            electrodesBuffer = new ComputeBuffer(electrodes.Length/rh.rasterizeGroups,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
            Graphics.SetRandomWriteTarget(2, electrodesBuffer, true);
            electrodesBuffer.SetData(rh.rasterizedGroups[0]); }
        else {
            electrodesBuffer.Dispose(); 
            electrodesBuffer = new ComputeBuffer(electrodes.Length,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
            Graphics.SetRandomWriteTarget(2, electrodesBuffer, true);
            electrodesBuffer.SetData(electrodes); }


        // Temporal Initialization
        if (vm.useTemporal) {
            tempVarsBuffer.Dispose();
            tempVars = new TemporalVariables[startingResX * startingResY];
            for (int i = 0; i < startingResX * startingResY; i++)
                tempVars[i] = new TemporalVariables(0f, 0f);
            tempVarsBuffer = new ComputeBuffer(startingResX * startingResY,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(TemporalVariables)), ComputeBufferType.Default);
            Graphics.SetRandomWriteTarget(1, tempVarsBuffer, true);
            tempVarsBuffer.SetData(tempVars);

            vm.temporalShader.SetBuffer("tempVariables", tempVarsBuffer);
            vm.temporalShader.SetFloat("xResolution", startingResX);
            vm.temporalShader.SetFloat("yResolution", startingResY);
            vm.temporalShader.SetFloat("tau_ca", vm.tau_ca);
            vm.temporalShader.SetFloat("tau_bp", vm.tau_bp);
            vm.temporalShader.SetFloat("ca_scale", vm.ca_scale);
            vm.temporalShader.SetFloat("amplitude", vm.amplitude ); 
        }

        if (vm.useAxonMap) {
            axonIdxEndBuffer.Dispose();
            axonIdxStartBuffer.Dispose();
            
            axonContributionBuffer.Dispose();
            axonContributionBuffer =
                new ComputeBuffer(axonMap.axonSegmentContributions.Length,
                    System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)), ComputeBufferType.Default);
            axonContributionBuffer.SetData(axonMap.axonSegmentContributions);
            
            axonIdxStartBuffer = new ComputeBuffer(axonMap.axonIdxStart.Length,System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)), ComputeBufferType.Default);
            axonIdxStartBuffer.SetData(axonMap.axonIdxStart);
            
            axonIdxEndBuffer = new ComputeBuffer(axonMap.axonIdxEnd.Length,System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)), ComputeBufferType.Default);
            axonIdxEndBuffer.SetData(axonMap.axonIdxEnd); 
        }
        
        ErrorDebug.Log("X: " + simulation_xMin + "," + simulation_xMax);
        ErrorDebug.Log("Y: " + simulation_yMin + "," + simulation_yMax); }

    /// <summary>
    /// If VariableManagerScript.randomShuffle is true, this sets the buffers of VariableManagerScript.randomShuffler to reflect that
    /// </summary>
	private void RandomizeElectrodes(){
 		if (vm.randomShuffle) {
            var shuffleIndexBuffer = new ComputeBuffer(electrodes.Length, sizeof(int));
            var processedTextureBuffer = new ComputeBuffer(electrodes.Length, sizeof(float));
            var shuffleKernel = vm.randomShuffler.FindKernel("CSMain");
            
            shuffleIndexBuffer.SetData(randomArray);
            vm.randomShuffler.SetInt("numberElectrodes", rh.currentRasterType!=RasterizationHandler.RasterType.None 
                 ? electrodes.Length/rh.rasterizeGroups : electrodes.Length);
            vm.randomShuffler.SetBuffer(shuffleKernel,"electrodesBuffer", electrodesBuffer);
            vm.randomShuffler.SetBuffer(shuffleKernel,"randomIndex", shuffleIndexBuffer);
            vm.randomShuffler.SetBuffer(shuffleKernel,"processedTextureBuffer", processedTextureBuffer);
            
            vm.randomShuffler.Dispatch(shuffleKernel, electrodes.Length/1024 + 1 , 1, 1);
            
            processedTextureBuffer.Release();
            shuffleIndexBuffer.Release(); } }

    /// <summary>
    /// Downscales source texture by a factor of VariableManagerScript.downscaleFactor and give it to processedTexture
    /// </summary>
    /// <param name="source">RenderTexture to downscale</param>
	private void PerformDownscale(RenderTexture source) {
		processedTexture = RenderTexture.GetTemporary((int) source.width/vm.downscaleFactor, 
        (int) source.height/vm.downscaleFactor, 0); // create new texture with dimensions from screen's render texture
		Graphics.Blit(source, processedTexture); }

    /// <summary>
    /// If we are using a RasterType:
    /// -Set the data of the current raster group.
    /// -Increment current raster group every 1/(rasterizeGroup*rasterTiming) seconds
    /// </summary>
    private void RasterizeElectrodes() {
        if (rh.currentRasterType != RasterizationHandler.RasterType.None) {
            electrodesBuffer.SetData(rh.rasterizedGroups[currentRasterGroup]);

            if (Time.realtimeSinceStartup - lastRasterChange > (1.0f / (rh.rasterTiming * rh.rasterizeGroups))) {
                lastRasterChange = Time.realtimeSinceStartup;
               
                currentRasterGroup++;
                currentRasterGroup = currentRasterGroup >= rh.rasterizeGroups
                    ? 0
                    : currentRasterGroup; } } }

    /// <summary>
    /// If VariableManagerScript.allElectrodesMax set the buffer data of allMaxElectrodes.
    /// Else: Run the processedTexture through a preprocessing shader material
    /// </summary>
	private void RunPreprocessing() {
        if (vm.allElectrodesMax)
            electrodesBuffer.SetData(allMaxElectrodes);
        else {
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0);
            Graphics.Blit(temp, processedTexture, vm.preprocessingShaderMaterial[vm.whichPreprocessor]);
            RenderTexture.ReleaseTemporary(temp); } }

    /// <summary>
    /// If using the bionic vision shader, run the processed texture through the perceptShaderMaterial
    /// </summary>
    private void GeneratePercepts(){
        if (vm.useBionicVisionShader) {
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0); 
            Graphics.Blit(temp, processedTexture, vm.perceptShaderMaterial);
            RenderTexture.ReleaseTemporary(temp); } }

    /// <summary>
    /// Reset temporal values when relevant.
    /// If using temporal, run the processedTexture through the temporalShader
    /// </summary>
    private void RunTemporal() {
        if (resetTemporalValues) {
            Debug.Log("Resetting temporal variables");
            vm.useTemporal = true;
            InitializeBuffers();
            Debug.Log("Reset temporal variables");
            
            resetTemporalValues = false; }
        
        if (vm.useTemporal) {
            vm.temporalShader.SetFloat("dt", Time.deltaTime);
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0);
            Graphics.Blit(temp, processedTexture, vm.temporalShader);
            RenderTexture.ReleaseTemporary(temp); } }

    /// <summary>
    ///  Cannot provide TOBII code, please use sXR for gaze...
    /// https://github.com/simpleOmnia/sXR
    /// Change screen position based off of where user's eye is looking if gazeLock is true
    /// </summary>
    private void GetGazeScreenPos() {
        /* screenPos = sxr.GetGazeScreenPos();*/
    } 
    
    
    /// <summary>
    /// If gazeLock is true, apply the gazeLockShader to processedTexture
    /// </summary>
	private void GazeLock() {
		if (vm.gazeLock) {
            temp = processedTexture;
			processedTexture = RenderTexture.GetTemporary(temp.width, temp.height, 0); 
            
            vm.gazeLockShader.SetFloat("gazeY", screenPos.y/startingResY);
            vm.gazeLockShader.SetFloat("gazeX", screenPos.x/startingResX);
 
            Graphics.Blit(temp, processedTexture, vm.gazeLockShader);
            RenderTexture.ReleaseTemporary(temp); } }
    /// <summary>
    /// If blurFinalImage is true, apply blurShader twice to processedTexture
    /// </summary>
    private void BlurImage(bool final) {
        if((final && vm.blurFinalImage) || (!final && vm.preprocessingBlur){
            vm.blurShader.SetInt("_KernelSize", final ? vm.postBlurIntensity : preBlurIntensity);
            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(startingResX, startingResY, 0);
            Graphics.Blit(temp, processedTexture, vm.blurShader);
            RenderTexture.ReleaseTemporary(temp);

            temp = processedTexture;
            processedTexture = RenderTexture.GetTemporary(startingResX, startingResY, 0);
            Graphics.Blit(temp, processedTexture, vm.blurShader);
            RenderTexture.ReleaseTemporary(temp); } }

    /// <summary>
    /// If in debugMode, log info to ErrorDebug
    /// </summary>
	private void ElectrodeDebug() {
        if (vm.debugMode) {
            if (electrodesBuffer != null & electrodesBuffer.IsValid() & electrodesBuffer.count>0) {
                Electrode[] tempArray = new Electrode[rh.currentRasterType!=RasterizationHandler.RasterType.None
                    ? electrodes.Length / rh.rasterizeGroups
                    : electrodes.Length];
                electrodesBuffer.GetData(tempArray);

                if (currentFrame == debugFrame) {
                    ErrorDebug.Log((rh.currentRasterType!=RasterizationHandler.RasterType.None ? rh.rasterizedGroups[currentRasterGroup] : electrodes));
                    for (int i = 0; i < tempArray.Length; i++) {
                        Electrode currentElectrode = tempArray[i];
                        ErrorDebug.Log(i + ":  " + currentElectrode.xPosition + "," +
                                       currentElectrode.yPosition + " -- " +
                                       currentElectrode.current); }

                    debugFrame += 250; } } } }

    /// <summary>
    /// Reads binary data from files and then sets up relevant axon and electrode values
    /// </summary>
    private void LoadBinaryData() {
          UI_Handler.Instance.pleaseWait.enabled = true; 
          BinaryHandler binaryHandler = new BinaryHandler();
          vm.updateConfigurationPath();
                   
          float[] axonSegmentToElectrodes =
              binaryHandler.ReadFloatsFromBinaryFile(
                  vm.configurationPath + "_axonElectrodeGauss").ToArray();
          axonSegmentGaussToElectrodes.Dispose();
          axonSegmentGaussToElectrodes = new ComputeBuffer(axonSegmentToElectrodes.Length,
              System.Runtime.InteropServices.Marshal.SizeOf(typeof(float)), ComputeBufferType.Default);
          axonSegmentGaussToElectrodes.SetData(axonSegmentToElectrodes); 
                    
          axonMap = binaryHandler.ReadAxonMap(vm.configurationPath);
          axonContributionBuffer =
              new ComputeBuffer(axonMap.axonSegmentContributions.Length,
                  System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)), ComputeBufferType.Default);
          axonContributionBuffer.SetData(axonMap.axonSegmentContributions);
          
          electrodes = binaryHandler.ReadElectrodeLocations(vm.configurationPath);
          electrodesBuffer =
              new ComputeBuffer(electrodes.Length,
                  System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
          Graphics.SetRandomWriteTarget(2, electrodesBuffer, true);
          electrodesBuffer.SetData(electrodes);

          UI_Handler.Instance.pleaseWait.enabled = false; }
    /// <summary>
    /// Sets VariableManagerScript.runShaders to true
    /// </summary>
    /// <returns></returns>
    public void RunShaderAfterFrames()
    {
        vm.runShaders = true; //TODO Update to work like UpdateAfterFrames but for runshader
    }

    /// <summary>
    /// Calls UpdateConfiguration every [frameDelay] frames
    /// </summary>
    public void UpdateAfterFrames() {
      framesToWait--; 
      vm.UpdateConfig(); 
      UI_Handler.Instance.pleaseWait.enabled = true;

      if (framesToWait == 0 || axonMap.axonIdxStart == null) {
          framesToWait = frameDelay;
          Debug.Log("UPDATE CALLED: " +
                    (vm.usePreDefinedBlock ? vm.predefinedSettings.ToString() : vm.configurationName));
          UpdateConfiguration(); } }
      
      /// <summary>
      /// Updates configuration of VariableManagerScript and electrodes
      /// </summary>
      private void UpdateConfiguration()
      {
          vm.UpdateConfig();
          electrodesHandler.SetRectangularGrid();
          
          if (!vm.loadFromBinaries || !File.Exists(vm.configurationPath + "_axonElectrodeGauss")){
              if (vm.useAxonMap)
                  axonMapHandler.SetAxonsAndElectrodesGauss();
          } else
              LoadBinaryData();
          
          allMaxElectrodes = electrodes;
                    for (int i = 0; i < allMaxElectrodes.Length; i++)
                      allMaxElectrodes[i].current = 1; 
                    
          electrodesHandler.ElectrodeGridToScreenPosCoords(); 
          InitializeBuffers();
          
          UI_Handler.Instance.pleaseWait.enabled = false; 
          
          SetRandomizerArray(); }
      
    /// <summary>
    /// Release buffers
    /// </summary>
    private void OnApplicationQuit() {
        if(electrodesBuffer != null && electrodesBuffer.IsValid())
            electrodesBuffer.Release();
        if (simulationVariablesBuffer != null && simulationVariablesBuffer.IsValid())
            simulationVariablesBuffer.Release();
        if (vm.useAxonMap && axonContributionBuffer != null && axonContributionBuffer.IsValid())
            axonContributionBuffer.Release(); }
    
    private void Start() {
        vm = VariableManagerScript.Instance;
        rh = RasterizationHandler.Instance; }
    
    public static BackendShaderHandler Instance { get; private set; }
    private void Awake() {
        tempVarsBuffer = new ComputeBuffer(10,
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(TemporalVariables)));
        axonSegmentGaussToElectrodes = new ComputeBuffer(10,
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)));
        electrodesBuffer = new ComputeBuffer(10,
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)));
        simulationVariablesBuffer = new ComputeBuffer(10,
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonMapSettings)));
        axonContributionBuffer = new ComputeBuffer(10, 
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(float))); 
        axonIdxStartBuffer = new ComputeBuffer(10, 
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)));
        axonIdxEndBuffer = new ComputeBuffer(10, 
            System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)));
        
        if ( Instance == null) {Instance = this;  DontDestroyOnLoad(gameObject); vrCamera = gameObject.GetComponent<Camera>(); }
        else Destroy(gameObject); }
}
using System.IO;
using BionicVisionVR.Resources;
using BionicVisionVR.Structs;
using UnityEngine;

public class VariableManagerScript : MonoBehaviour { 

    public static VariableManagerScript Instance { get; private set; }

    [Header("Data Settings")] 
    public int subjectNumber = -1;
    public bool usePreDefinedBlock = false; 
    public PreDefinedBlocks predefinedSettings;
    public bool loadFromBinaries = false;
    public bool useLeftEye = false;

    [Header("Shader Settings")]
    public bool runShaders = true;
    public Material preprocessingShaderMaterial;
    public bool useBionicVisionShader = true; 
    public Material perceptShaderMaterial;
    public bool blurFinalImage= true;
    public Material blurShader; 
    public ComputeShader randomShuffler;
    public bool useAxonMap = false;
    public bool calculateAxonMap = false;
    public bool useTemporal = false;
    public bool randomShuffle = false;
    

    [Header("Debug Settings")]
    public bool debugMode = false;
    public bool showElectrodes = false;
    public int specificPixelToDebug = 0;
    public int debugAxonTraces = 0; 
    
    [Header("Electrode Array Settings")]
     public int numberXelectrodes = 10;
     public int numberYelectrodes = 10;
   
   //TODO ?
   //public float implant_fovX =  40.0f;
    //public float implant_fovY =  40.0f;
    //public float headset_fovX =  55.0f;
    //public float headset_fovY =  55.0f;
    public float implant_fov = 40.0f; 
    public float headset_fov = 55.0f;
    public float rotation = 0;
    public float xPosition = 0;
    public float yPosition = 0;
    public float electrodeSpacing = 525;
    public float amplitude = 20.0f; 
    
    [Header("Simulation settings")]
    public float rho = 100.0f;
    public float lambda = 100.0f;
    public float threshold = .0001f;
    public float axonContributionThreshold = .0001f; 
     public int downscaleFactor = 1;
     public int number_axons = 300;
     public int number_axon_segments = 300; 
    
     [Header("Temporal model settings")]
    public float frameDuration = .011111f; //(sec) Should match screen refresh rate (11.111ms for HTC Vive pro)
    public float pulseFrequency = 20.0f; //(hz) How many times a second the pulse occurs
    public float pulseDuration = 0.00045f; // Single pulse duration
    public float interphasePulseDuration = 0.00045f; // Time between positive and negative portions of pulse train 
    public float simulationTimeStep = .00001f; // Amount of time for each iteration step (dt)

    [Header("Storage settings")] 
    public bool savePremadeConfiguration = false;
    public bool useAutomaticNaming = false; 
    public string configurationName = ""; 
    public string configurationPath;

    public void updateConfigurationPath()
    {
        if (useAutomaticNaming)
        {
           configurationName = predefinedSettings.ToString() + (useLeftEye ? "Left" : "Right"); 
        }
        configurationPath = Application.dataPath + Path.DirectorySeparatorChar + "BionicVisionVR" +
        Path.DirectorySeparatorChar
        + "Coding" + Path.DirectorySeparatorChar + "PremadeConfigurations" + Path.DirectorySeparatorChar+configurationName;
    }
    
    private void Awake()
    {
        updateConfigurationPath();
        
        TextFileHandler handleTextFile = new TextFileHandler(); 
        if ( Instance == null)
        {
            Instance = this;
            //handleTextFile.ClearP2PFile(); 
            //handleTextFile.WriteP2PVariables();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
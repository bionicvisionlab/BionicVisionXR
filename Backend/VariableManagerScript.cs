using System.IO;
using BionicVisionVR.Resources;
using UnityEngine;

/// <summary>
/// TODO: Document everything like this: 
/// [Singleton] Handles settings and keeps track of certain variables used across multiple files
/// Contains:
///     void UpdateConfig()
///     void UpdateConfig(PreDefinedSettings newSetting)
///     string GetCurrentConfig()
///     void updateConfigurationPath()
///     bool CheckForUpdate()
/// On Awake:
///     Initializes singleton and a TextFileHandler
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class VariableManagerScript : MonoBehaviour { 

    public static VariableManagerScript Instance { get; private set; }

    [Header("Data Settings")] 
    public int subjectNumber = -1;
    public bool usePreDefinedBlock = false; 
    public AxonMapSettings_Enum predefinedSettings;
    private AxonMapSettings_Enum lastUsedPredefSettings;
    public bool loadFromBinaries = false;
    public bool useLeftEye = false;
    public int zoom; 

    [Header("Shader Settings")]
    public bool runShaders = true;
    private bool overRideRunShaders;
    private bool overRiddenRunShaders; 
    public bool invertPreprocessing = false; 
    public Material[] preprocessingShaderMaterial = new Material[3];
    public int whichPreprocessor = 0;
    public bool depthDetection = false; 
    public bool useBionicVisionShader = true; 
    public Material perceptShaderMaterial;
    public bool preprocessingblur = true;
    public int preBlurIntensity = 5; 
    public bool blurFinalImage= true;
    public int postBlurIntensity = 15;
    public Material blurShader;
    public ComputeShader randomShuffler;
    

    [Header("Gaze Lock")]
    public Material gazeLockShader; 
    public bool gazeLock = true; 
    public bool smoothMove = true;
    [Range(1, 30)] public int smoothMoveSpeed = 18;

    [Header("Debug Settings")] 
    public bool allElectrodesMax = false; 
    public bool debugMode = false;
    public bool showElectrodes = false;
    public int specificPixelToDebug = 0;
    public int debugAxonTraces = 0; 
    
    [Header("Electrode Array Settings")]
    public int numberXelectrodes = 10;
    public int numberYelectrodes = 10;
    public float implant_fov = 40.0f; 
    public float headset_fov = 55.0f;
    public float rotation = 0;
    public float xPosition = 0;
    public float yPosition = 0;
    public float electrodeSpacing = 525;
    public float amplitude = 20.0f;

    [Header("Simulation settings")]
    public bool useAxonMap = false;
    public bool calculateAxonMap = false;
    public bool randomShuffle = false;
    public float rho = 100.0f;
    public float lambda = 100.0f;
    public float threshold = .0001f;
    public float axonContributionThreshold = .0001f; 
    public int downscaleFactor = 1;
    public int number_axons = 300;
    public int number_axon_segments = 300;

    [Header("Temporal model settings")]
    public bool useTemporal = false;
    public Material temporalShader;
    public float tau_ca = 0.2f; // Charge Dissipation Rate (slow decay)
    public float tau_bp = 0.15f;  // Brightness Fading Rate (fast decay)
    public float ca_scale = 0.25f;

    [Header("Storage settings")] 
    public bool savePremadeConfiguration = false;
    public bool useAutomaticNaming = true; 
    public string configurationName = "N/A"; 
    public string configurationPath;
    public string backendPath = "N/A";
    
    public bool OverRideRunShaders
    {
        get => overRideRunShaders;
        set => overRideRunShaders = value;
    }
    public bool OverRiddenRunShaders
    {
        get => overRiddenRunShaders;
        set => overRiddenRunShaders = value;
    }
    /// <summary>
    /// Updates settings and configuration path
    /// </summary>
    public void UpdateConfig()
    {
        lastUsedPredefSettings = predefinedSettings;
        if (usePreDefinedBlock)
        {
            AxonMapSettings c_block = AxonMapSettings.GetPredefinedSettings(predefinedSettings);
            c_block.UpdateSettings();
        }
        updateConfigurationPath();
        configurationName = GetCurrentConfig(); 
    }
    /// <summary>
    /// Change settings and update them
    /// </summary>
    /// <param name="newSetting">New setting to update to</param>
    public void UpdateConfig(AxonMapSettings_Enum newSetting)
    {
        predefinedSettings = newSetting;
        UpdateConfig();
    }
    /// <summary>
    /// Get a string representation of electrode values
    /// </summary>
    /// <returns>Current configuration of electrode values</returns>
    public string GetCurrentConfig()
    {
        return numberXelectrodes + "_" + numberYelectrodes + "_" + xPosition + "_" + yPosition +
                            "_" + rotation + "_" + rho + "_" + lambda + "_" + useLeftEye + "_" + 
                            RasterizationHandler.Instance.currentRasterType;
    }
    /// <summary>
    /// Updates the configuration path
    /// </summary>
    /// <returns></returns>
    public void updateConfigurationPath()
    {
       backendPath = Application.dataPath + Path.DirectorySeparatorChar + "BionicVisionVR" +
        Path.DirectorySeparatorChar
        + "Backend" + Path.DirectorySeparatorChar ;
        configurationPath = backendPath + "PremadeConfigurations" + Path.DirectorySeparatorChar + GetCurrentConfig();

    }
    /// <summary>
    /// if last used settings == current settings ? return true : return false
    /// </summary>
    /// <returns></returns>
    public bool CheckForUpdate()
    {
        return lastUsedPredefSettings != predefinedSettings || GetCurrentConfig() != configurationName; 
    }

    private void Awake()
    {
        TextFileHandler handleTextFile = new TextFileHandler(); 
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
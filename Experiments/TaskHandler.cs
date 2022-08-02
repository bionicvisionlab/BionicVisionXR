using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.BionicVisionVR.Coding.Structs;
using BionicVisionVR.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Handles the current task
/// </summary>
public class TaskHandler : MonoBehaviour
{
    public enum DifficultyAdjust{ //TODO  : Implement -  Method of difficulty adjustment for constant adjustments
        up2down1,
        up3down1,
        None }

    public enum BlockChanges{ // TODO : Implement - What should be updated during each new block
        SimulationSettings,
        RasterSettings, }
    
    //TODO: Update to allow TaskHandler to handle any Type<T> of difficulty to adjust
    [SerializeField] private DifficultyAdjust adjustSimulationDifficulty = DifficultyAdjust.None;

    [SerializeField] private string BackupDirectory =
        "G:\\.shortcut-targets-by-id\\1phWEnOM4p2w-OojKOan4aRd0ztJa1muY\\2022 Kasowski ArgusRasterization\\Data\\"; 
    
    public int num_correct=0;
    public int num_attempted=0;
    public int currentDifficulty = 3;
    public float trialTimer;
    public bool updateDifficulty = true;
    public int numberAmpSettingRounds = 6; 
    
    public bool cameraTracking = true;
    public bool gazeTracking = true; 
    public bool triggerPressed = false;
    
    public int currentBlock = 0;
    public int currentTrial = 0;
    private int numberOfTrials = 10;
    private int lastUpdate;
    // todo update trial structure
    private FileHandler fileHandler = new FileHandler();
    
    public int phase = 0;  // Used to track where in the experiment we are
    private string[] phases = new string[0]; 
    
    public int step = 0; // Used to track where in the experiment's phase we are
    private string[] steps = new string[0];

    public bool getDifficultyRating, setAmplitude = false; 
    public bool finished = false; 
    public string subjectFile, backupSubjectFile;
    private float triggerTimer = 0.0f; 

    public PreDefinedSettings[] simulationSettings = new PreDefinedSettings[] {};

    private int[] rasterOrder = new[] {0, 1, 2, 3, 4, 5};
    
    //TODO: Update to take in any Type<T>, this will be any type of thing that can be changed during testing
    // e.g. simulation settings, preprocessing shaders, etc...
    public RasterizationHandler.RasterType[] rasterTypes = new[] {
        RasterizationHandler.RasterType.None,
        RasterizationHandler.RasterType.HorizontalLineRaster, 
        RasterizationHandler.RasterType.VerticalLineRaster,
        RasterizationHandler.RasterType.RandomRaster,
        RasterizationHandler.RasterType.RadialRaster, 
        RasterizationHandler.RasterType.CheckerboardRaster};
    
    public TextMeshProUGUI output1, output2, output3, output4;
    public RawImage amplitudeStimulus; 
    private int difficultyRating = 3;
    private float inputTimer = 0.0f;
    public Queue<int> lastTwentyCorrect = new Queue<int>(20);
	private bool rateRasters, rateRasters2; 
	private int favoriteRaster;
    private int lastDifficultyChange;
    private int numCorrectSinceUpdate = 0;

    // public void ThreeDownOneUp(bool correctAnswer) {
    //     if (!correctAnswer) {
    //         numCorrectSinceUpdate = 0; 
    //         currentDifficulty--;
    //         return; }
    //     
    //     numCorrectSinceUpdate++;
    //     if (numCorrectSinceUpdate > 2) {
    //         lastDifficultyChange = currentTrial;
    //         currentDifficulty++; 
    //         numCorrectSinceUpdate = 0; } }

    public void Update() {
        if (!finished) {
            UI_Handler.Instance.label.text = "Raster " + rasterOrder[step].ToString();
            if (Input.GetKey(KeyCode.X)) {
                VariableManagerScript.Instance.OverRideRunShaders = true;
                VariableManagerScript.Instance.OverRiddenRunShaders = Input.GetKeyDown(KeyCode.T); }
            else if (Input.GetKeyDown(KeyCode.Z))
                VariableManagerScript.Instance.OverRideRunShaders = false;

            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) currentDifficulty = 0;
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) currentDifficulty = 1;
                if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) currentDifficulty = 2;
                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) currentDifficulty = 3;
                if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) currentDifficulty = 4;
                if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) currentDifficulty = 5;
                if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) currentDifficulty = 6;
                if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) currentDifficulty = 7;
                if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) currentDifficulty = 8;
                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) currentDifficulty = 9;
                if (Input.GetKeyDown(KeyCode.Asterisk)) currentDifficulty = 10;
            }

            if (Input.GetKey(KeyCode.Q)) {
                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) lastTwentyCorrect.Enqueue(0);
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) lastTwentyCorrect.Enqueue(1); }

            if (lastTwentyCorrect.Count > 20)
                lastTwentyCorrect.Dequeue();

            if (!finished && phase > 1 && !(step > rasterTypes.Length))
                RasterizationHandler.Instance.SetRasterType(rasterTypes[step]);

            if (phase < 3)
                VariableManagerScript.Instance.useTemporal = false;

            if (updateDifficulty & phase == 2 /*& currentTrial % 3 == 0*/ & lastUpdate != Instance.currentTrial)
                UpdateDifficulty();

            if (phase > 0)
                VariableManagerScript.Instance.predefinedSettings =
                    MainController.Instance.sharedSimulationSettings[currentDifficulty];


            //TODO: Move experimenter output text to UI_Handler for prefab
            output1.text = "Phase/step: " + phase + "/" + step;
            output2.text = "Trial: " + currentTrial;
            output3.text = "Accuracy: " + (num_attempted == 0
                ? "N/A"
                : (GetAccuracy()) + ": " + lastTwentyCorrect.ToArray().ToCommaSeparatedString());
            output4.text = "Difficulty: " + currentDifficulty;

            if (getDifficultyRating) {
                UI_Handler.Instance.DisableAllComponentsUI();
                UI_Handler.Instance.textbox1.text = "How difficult did you find the last block of trials?";
                UI_Handler.Instance.textbox1.enabled = true;
                UI_Handler.Instance.textbox2.enabled = true;
                UI_Handler.Instance.textbox2.text = difficultyRating.ToString();
                if (((Input.GetAxis("Vertical") > .9) ||
                     SteamVR_Actions._default.SnapTurnRight.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    difficultyRating++;
                    inputTimer = Time.realtimeSinceStartup; }

                if (((Input.GetAxis("Vertical") < -.9) ||
                     SteamVR_Actions._default.SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    difficultyRating--;
                    inputTimer = Time.realtimeSinceStartup; }

                difficultyRating = Math.Clamp(difficultyRating, 0, 5);

                if (GetTrigger()) {
                    if (UI_Handler.Instance.textbox3.enabled) {
                        UI_Handler.Instance.textbox1.enabled = false;
                        UI_Handler.Instance.textbox2.enabled = false;
                        UI_Handler.Instance.textbox3.enabled = false;
                        WriteToTaggedFile("Difficulty", difficultyRating.ToString(), false);
                        getDifficultyRating = false;

                        if (rasterTypes[rasterOrder[step]] == RasterizationHandler.RasterType.RandomRaster)
                            WriteToTaggedFile("randomRaster", RasterizationHandler.Instance.PrintRasterGroups(), false);

                        Debug.Log("Rasterization Updated");

                        if (step >= rasterTypes.Length - 1) {
                            Debug.Log(step + 1 + "/" + rasterTypes.Length +
                                      " Rasterizations Complete - finished");
                            rateRasters = true;
                            return; }

                        step++;
                        UI_Handler.Instance.DisableAllComponentsUI();
                        setAmplitude = true;
                        BackendShaderHandler.Instance.resetTemporalValues = true;

                        VariableManagerScript.Instance.amplitude =
                            rasterTypes[rasterOrder[step]] == RasterizationHandler.RasterType.None ? 1 : 3;
                    } else
                        UI_Handler.Instance.textbox3.enabled = true;  } }

            if (!finished & setAmplitude) {
                amplitudeStimulus.enabled = true;

                UI_Handler.Instance.textbox4.enabled = true;
                if (GetTrigger()) {
                    Debug.Log("Amp set");
                    setAmplitude = false;
                    UI_Handler.Instance.DisableAllComponentsUI();
                    amplitudeStimulus.enabled = false;
                    UI_Handler.Instance.textbox4.enabled = false; } }

            if (rateRasters) {
                UI_Handler.Instance.textbox1.text = "Which raster pattern did you prefer most?";
                UI_Handler.Instance.textbox2.text = favoriteRaster.ToString();
                UI_Handler.Instance.textbox1.enabled = true;
                UI_Handler.Instance.textbox2.enabled = true;

                if (((Input.GetAxis("Vertical") > .9) ||
                     SteamVR_Actions._default.SnapTurnRight.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    favoriteRaster++;
                    inputTimer = Time.realtimeSinceStartup; }

                if (((Input.GetAxis("Vertical") < -.9) ||
                     SteamVR_Actions._default.SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    favoriteRaster--;
                    inputTimer = Time.realtimeSinceStartup; }

                favoriteRaster = Math.Clamp(favoriteRaster, 1, 5);
                if (GetTrigger()) {
                    UI_Handler.Instance.textbox1.enabled = false;
                    UI_Handler.Instance.textbox2.enabled = false;
                    WriteToTaggedFile("favoriteRaster",
                        MainController.Instance.nextTask + "," + rasterTypes[favoriteRaster].ToString(), false);
                    rateRasters2 = true;
                    rateRasters = false; } }

            if (rateRasters2)
            {
                UI_Handler.Instance.textbox1.text = "Which raster pattern did you prefer overall across tasks?";
                UI_Handler.Instance.textbox2.text = favoriteRaster.ToString();
                UI_Handler.Instance.textbox1.enabled = true;
                UI_Handler.Instance.textbox2.enabled = true;

                if (((Input.GetAxis("Vertical") > .9) ||
                     SteamVR_Actions._default.SnapTurnRight.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    favoriteRaster++;
                    inputTimer = Time.realtimeSinceStartup; }

                if (((Input.GetAxis("Vertical") < -.9) ||
                     SteamVR_Actions._default.SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.Any)) &
                    Time.realtimeSinceStartup - inputTimer > .25f) {
                    favoriteRaster--;
                    inputTimer = Time.realtimeSinceStartup; }

                favoriteRaster = Math.Clamp(favoriteRaster, 1, 5);
                if (GetTrigger()) {
                    UI_Handler.Instance.textbox1.enabled = false;
                    UI_Handler.Instance.textbox2.enabled = false;
                    step--;
                    WriteToTaggedFile("favoriteRasterOverall",
                        MainController.Instance.nextTask + "," + rasterTypes[favoriteRaster].ToString(), false);
                    step++;
                    rateRasters2 = false;
                    finished = true; } }

            VariableManagerScript.Instance.amplitude =
                Math.Clamp(VariableManagerScript.Instance.amplitude, 0.5f, 30.0f);
        } else 
            UI_Handler.Instance.finished.enabled = true; }

    
    public bool RunTask() {  return !finished && !getDifficultyRating && !setAmplitude && !rateRasters && !rateRasters2; }
    
    // Should be called at the beginning of each experiment
    public void TaskStart(string taskName, string[] filesList, PreDefinedSettings[] simulationSettings,
        bool randomizeSimulationSettings, bool skipFirstSetting, int numberOfTrials) 
    {
        Debug.Log("Experiment start"); 
        string dataDirectory = Application.dataPath + Path.DirectorySeparatorChar + "Experiments" +
                               Path.DirectorySeparatorChar +
                               taskName + Path.DirectorySeparatorChar +
                               "Experiment_Data" + Path.DirectorySeparatorChar;
        System.IO.Directory.CreateDirectory(dataDirectory);
        subjectFile = dataDirectory + VariableManagerScript.Instance.subjectNumber + "_"+ System.DateTime.Today.Month+"_"+System.DateTime.Today.Day+ ".csv";
        backupSubjectFile = BackupDirectory + taskName + Path.DirectorySeparatorChar +
                               "Experiment_Data" + Path.DirectorySeparatorChar + VariableManagerScript.Instance.subjectNumber + "_"+ System.DateTime.Today.Month+"_"+System.DateTime.Today.Day+ ".csv";
        Random.InitState(VariableManagerScript.Instance.subjectNumber * 10 + MainController.Instance.nextTask);

        if (randomizeSimulationSettings) {
            for (int t = 0; t < simulationSettings.Length; t++) {
                if (t > 0 || !skipFirstSetting) {
                    PreDefinedSettings tmp = simulationSettings[t];

                    int r = Random.Range(t, simulationSettings.Length);
                    simulationSettings[t] = simulationSettings[r];
                    simulationSettings[r] = tmp; } } }

        WriteHeaderToTaggedFile(
            "_camera_tracker",
            "CamRotX,CamRotY,CamRotZ,CamPosX,CamPosY,CamPosZ,ScreenGazeX,ScreenGazeY,WorldGazeX,WorldGazeY,WorldGazeZ",
            false);
        this.simulationSettings = simulationSettings;
        this.numberOfTrials = numberOfTrials;
        
        updateDifficulty = phase <= 2; }

    public bool GetTrigger() {
        if(Time.realtimeSinceStartup - triggerTimer > .25 & 
           (triggerPressed || Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire1") > 0)){
            triggerTimer = Time.realtimeSinceStartup;
            return true; 
        }
        return false; }

    public string SimulationSettingsInfo() {
        return SimulationSettings.GetPreDefinedSimulationSettings(simulationSettings[step]).printSettings(); }

    public void WriteToTaggedFile(string fileTag, string toWrite, bool printSimulationSettings)
    {
        fileHandler.AppendLine(subjectFile.Replace(".csv", "_" + fileTag + ".csv"), 
            Time.realtimeSinceStartup +"," + 
            phase + "," + step + "," + currentTrial + "," + (Time.realtimeSinceStartup-trialTimer) + "," + 
            (printSimulationSettings ? SimulationSettingsInfo() +"," : simulationSettings[currentDifficulty].ToString()) 
            + "," +  rasterTypes[step].ToString() +","+ VariableManagerScript.Instance.amplitude + "," + toWrite);
        
        if (!String.IsNullOrEmpty(BackupDirectory))
            fileHandler.AppendLine(backupSubjectFile.Replace(".csv", "_" + fileTag + ".csv"), 
                Time.realtimeSinceStartup +"," + 
                phase + "," + step + "," + currentTrial + "," + (Time.realtimeSinceStartup-trialTimer) + "," + 
                (printSimulationSettings ? SimulationSettingsInfo()  : simulationSettings[currentDifficulty].ToString()) 
                + "," + rasterTypes[step].ToString() +","+ VariableManagerScript.Instance.amplitude + "," + toWrite); 
    } public void WriteToTaggedFile(string fileTag, string toWrite){ WriteToTaggedFile(fileTag, toWrite, false);}

    public void WriteHeaderToTaggedFile(string fileTag, string toWrite, bool printSimulationSettings) {
        fileHandler.AppendLine(subjectFile.Replace(".csv", "_" + fileTag + ".csv"), 
            "ExperimentTime,Phase,Step,CurrentTrial,TrialTime," + 
            (printSimulationSettings ? (SimulationSettings.GetPreDefinedSimulationSettings(simulationSettings[currentBlock]).printSettings(true)) 
                : "SimulationSettings") + ",RasterSettings,amplitude," + toWrite); 
        if(!String.IsNullOrEmpty(BackupDirectory))
            fileHandler.AppendLine(backupSubjectFile.Replace(".csv", "_" + fileTag + ".csv"), 
                "ExperimentTime,Phase,Step,CurrentTrial,TrialTime," + 
                (printSimulationSettings ? (SimulationSettings.GetPreDefinedSimulationSettings(simulationSettings[currentBlock]).printSettings(true)) 
                    : "SimulationSettings") + ",RasterSettings,amplitude," + toWrite); }
    
    public void WriteToSubjectFile(string toWrite, bool printSimulationSettings) {
        WriteToTaggedFile("", toWrite, printSimulationSettings); } 
    public void WriteToSubjectFile(string toWrite){WriteToSubjectFile(toWrite, false);}

    public void NextRaster() {
        getDifficultyRating = true; }

    float GetAccuracy() { return (float) lastTwentyCorrect.Sum() / lastTwentyCorrect.Count(); }
    
    void UpdateDifficulty()
    {
        if (lastTwentyCorrect.Count() > 15 && GetAccuracy() < .7f ||
            lastTwentyCorrect.Count > 9 && GetAccuracy() < .6f ||
            lastTwentyCorrect.Count > 5 && GetAccuracy() < .5f ||
            lastTwentyCorrect.Count > 2 && GetAccuracy() == 0) 
        {
            currentDifficulty--;
            lastTwentyCorrect.Clear(); }

        else if (lastTwentyCorrect.Count() > 15 && GetAccuracy() > .9f ||
            lastTwentyCorrect.Count > 7 && GetAccuracy() > .95f)
        {
            currentDifficulty++;
            lastTwentyCorrect.Clear(); }
        
        currentDifficulty = Math.Clamp(currentDifficulty, 0, 10);
       // Debug.Log("Set difficulty: "+currentDifficulty);
       }

    public void SetRasterOrder() {
        int[] randomOrder = new int[rasterOrder.Length];
        randomOrder[0] = 0; 
        for (int i = 1; i < rasterOrder.Length; i++)
            randomOrder[i] = (int)Random.Range(0, 100f);
        Array.Sort(randomOrder, rasterTypes); }
    
    public static TaskHandler Instance { get; private set; }
    private void Awake() {
        subjectFile = Application.dataPath + Path.DirectorySeparatorChar + "Experiments" +
                      Path.DirectorySeparatorChar + "Debug" +
                      Path.DirectorySeparatorChar + "subjectFile.csv";
        
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); } } 
}

using System;
using UnityEngine;
using UnityEngine.Video;
/// <summary>
/// Used for tasks with eight choices
/// TODO where to attach
/// Uses TaskHandler as "th", UI_Handler as "ui", VariableManagerScript as "vm", 
/// </summary>
public class EightFC_Task : MonoBehaviour
{
    [SerializeField] private GameObject VideoCanvas; 
    [SerializeField] private VideoClip rightMov, upRightMov, upMov, upLeftMov, leftMov, downLeftMov, downMov, downRightMov;
    [SerializeField] private Texture2D intro1, intro2, practiceStart, preExperiment, preRaster, startTrial, breakScreen, requestInput;
    [SerializeField] private Texture2D left, upLeft, up, upRight, right, downRight, down, downLeft; 
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] string experimentName = "Output";

    private enum Step
    {
        Intro1,
        Intro2,
        Practice,
        PreExperiment,
        PreRaster,
        Start,
        None
    }

    private Step currentStep;
    
    private Camera spvCamera;
    private JoystickController jsc = new JoystickController();
    public float speedUpFactor = 1.0f; 
    
    private string[] includedFiles = {"continuousMovement"};
    
    private double continousTimer; 
    private double continousInterval = 0.25;
    private bool waitingForInput; 
    private bool currentlyInTrial = false;
    private bool displayPreRasterScreen = false; 

    private int[] breaks; 
    private int nextBreak = 0;
    private VideoClip[] videoClips;
    private JoystickController.JoyStickDirection currentClipDirection;

    private int numberOfTrials = 8; // Number of trials per block
    private int numberOfPracticeTrials = 2; // Number of trials without SPV
    private int numberOfPracticeTrialsBV = 10; 
    private static int numberVideos = 8;
    private TaskHandler th;
    private UI_Handler ui;
    private VariableManagerScript vm;
    private SoundHandler sh;
    private CameraTracker ct;
    private BackendShaderHandler bsh;
    

    void RandomizeVideos() {
        UnityEngine.Random.InitState(vm.subjectNumber + th.currentBlock);
        for (int i = 0; i < numberVideos; i++) {
            var tmp = videoClips[i];
            int randomSpot = UnityEngine.Random.Range(i, numberVideos);
            videoClips[i] = videoClips[randomSpot];
            videoClips[randomSpot] = tmp; } }

    void GetFinalInput() {
        ct.PauseRecording();
        VideoCanvas.SetActive(false);
        ui.EnableComponentUI(requestInput, true); 
        waitingForInput = true; 
        jsc.DisplayDirectionUI(); }
    
    bool TimeElapsed(float time) { return Time.realtimeSinceStartup -th.trialTimer > time; }
    /*  phase 1 = intro
        phase 2 = pre experiment
        phase 3 = experiment
        phase 4 = between rounds of experiment*/
    private void Update()
    {
        videoPlayer.playbackSpeed = speedUpFactor;
        if (!th.RunTask())
            return;
        //if esc key is pressed and trial hasn't started, change breakScreen setting
        if (Input.GetKeyDown(KeyCode.Escape) && !currentlyInTrial) {
            ui.FlipComponentUI(breakScreen); }
        
        //if in phase 1 always, do this stuff
        //could be put into input if statement
        if (th.phase == 1)
        {
            //after a few more practice trials with runShaders=true, then move to phase 2
            if (th.currentTrial >= numberOfPracticeTrials + numberOfPracticeTrialsBV)
            {
                RandomizeVideos();
                th.phase = 2;
                SetStep(Step.PreExperiment);
            }
            //after running a few practice trials, set runShaders to true
            else if (th.currentTrial >= numberOfPracticeTrials)
            {
                vm.runShaders = true;
            }
        }
        if (th.phase == 3 && !th.getDifficultyRating && !th.setAmplitude && !currentlyInTrial && !IsStep(Step.PreRaster))
            SetStep(Step.Start); 
        if (PressedKey())
        {
            if (th.phase == 1) {
                //display intro1 for at least 3 seconds
                if (IsStep(Step.Intro1) && TimeElapsed(3.0f)) {
                    SetStep(Step.Intro2);
                    th.trialTimer = Time.realtimeSinceStartup; }
                //display intro2 for at least .25 seconds
                if (IsStep(Step.Intro2) && TimeElapsed(.25f)) {
                    SetStep(Step.Practice);
                    th.trialTimer = Time.realtimeSinceStartup; }
                //display practiceStart for at least .25 seconds
                if (IsStep(Step.Practice) && TimeElapsed(.25f)) {
                    SetStep(Step.Start);
                    th.trialTimer = Time.realtimeSinceStartup; } }
            
            if (IsStep(Step.PreRaster) && !displayPreRasterScreen) {
                th.NextRaster();
                displayPreRasterScreen = true; }
            else if (IsStep(Step.PreExperiment) && TimeElapsed(.25f)) {
                Debug.Log("Closing UI Component");
                SetStep(Step.Start);
                th.trialTimer = Time.realtimeSinceStartup; }
            else if (displayPreRasterScreen && !currentlyInTrial && th.step==0 && th.currentTrial==0 && !IsStep(Step.Start)) {
                SetStep(Step.PreRaster); }
            //preExperiment or experiment phase
            else if (th.phase > 1)
            {
                //if user has input an answer
                if (waitingForInput && jsc.GetDirection() != JoystickController.JoyStickDirection.None)
                {
                    bool correctResponse = false;
                    if (jsc.GetDirection() == currentClipDirection)
                    {
                        correctResponse = true;
                        ui.textbox1.text = "Correct";
                        sh.Ding();
                    }
                    else
                    {
                        ui.textbox1.text = "Incorrect";
                        sh.Buzz();
                    }

                    ui.textbox1.enabled = true;

                    th.lastTwentyCorrect.Enqueue(correctResponse ? 1 : 0);
                    th.num_correct += correctResponse ? 1 : 0;
                    th.num_attempted++;

                    float horizontal_input = Input.GetAxis("Horizontal");
                    float vertical_input = Input.GetAxis("Vertical");


                    th.WriteToSubjectFile(videoPlayer.clip.ToString().Replace(" (UnityEngine.VideoClip)", "") + "," +
                                          horizontal_input + "," + vertical_input +
                                          "," + correctResponse, true);

                    ui.DisableAllComponentsUI();
                    waitingForInput = false;
                    currentlyInTrial = false;

                    th.currentTrial++; // Change trial without recording
                    if (th.currentTrial >= 42 ) {
                        th.phase = 3;
                        th.currentTrial = 0;
                        th.step = 0;
                        th.updateDifficulty = false; 
                        SetStep(Step.PreRaster); }
                    else
                        SetStep(Step.Start);

                    if (th.currentTrial % 8 == 0) {
                        RandomizeVideos();
                        if (th.phase == 3 && th.currentTrial > th.numberAmpSettingRounds) {
                            th.currentTrial = 0;
                            th.phase++; }
                        if (th.phase == 4 && th.currentTrial >= numberOfTrials ) {
                            th.currentTrial = 0;
                            th.NextRaster();
                            th.phase--; } }
                           
                    SetupClip();
                    th.trialTimer = Time.realtimeSinceStartup;
                }
                else if (TimeElapsed(.25f) && IsStep(Step.Start) && !currentlyInTrial)
                {
                    ui.DisableAllComponentsUI();
                    currentlyInTrial = true;
                    ui.textbox1.enabled = false;

                    Debug.Log("Play Video Player");
                    VideoCanvas.SetActive(true);
                    videoPlayer.Play();

                    continousTimer = Time.realtimeSinceStartup;
                    th.trialTimer = Time.realtimeSinceStartup;
                    ct.StartRecording(true);
                    if (th.phase == 3)
                    {
                        bsh.resetTemporalValues = true;
                        th.updateDifficulty = false;
                    }
                }
            }
        }
        if (th.phase > 1 && currentlyInTrial) {
            if (Time.realtimeSinceStartup - continousTimer > continousInterval) {
                th.WriteToTaggedFile("continuousMovement", videoPlayer.clip.ToString().Replace(" (UnityEngine.VideoClip)", "") +
                                                           "," + Input.GetAxis("Horizontal").ToString() + "," +
                                                           Input.GetAxis("Vertical").ToString(), true);
                continousTimer = Time.realtimeSinceStartup; } }

            
        if (th.phase > 0 && currentlyInTrial && Time.realtimeSinceStartup -th.trialTimer >= videoPlayer.clip.length/speedUpFactor) {
            GetFinalInput();
            videoPlayer.Stop(); }
    }

    //returns true if user has pressed button
    private bool PressedKey()
    {
        return TimeElapsed(.25f) & (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire1") > 0);
    }
    /*
    void Update() {
        videoPlayer.playbackSpeed = speedUpFactor; 
        
        if (th.RunTask()) {

            if (Input.GetKeyDown(KeyCode.Escape) && !trialStarted) {
                ui.FlipComponentUI(breakScreen); }

            if (th.phase == 1)
            {
                //after a few more practice trials with runShaders=true, then move to phase 2
                if (th.currentTrial >= numberOfPracticeTrials + numberOfPracticeTrialsBV)
                {
                    RandomizeVideos();
                    th.phase = 2;
                    SetStep(Step.PreExperiment);
                }
                //after running a few practice trials, set runShaders to true
                else if (th.currentTrial >= numberOfPracticeTrials)
                {
                    vm.runShaders = true;
                }
            }

            if (th.phase == 3 && !th.getDifficultyRating && !th.setAmplitude && !trialStarted && !IsStep(Step.PreRaster))
                SetStep(Step.Start); 
            //if .25 seconds have passed and user is pressing the button
            if (TimeElapsed(.25f) & (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire1") > 0)) {
                if (th.phase == 1) {
                    //display intro1 for at least 3 seconds
                    if (IsStep(Step.Intro1) && TimeElapsed(3.0f)) {
                        SetStep(Step.Intro2);
                        th.trialTimer = Time.realtimeSinceStartup; }
                    //display intro2 for at least .25 seconds
                    if (IsStep(Step.Intro2) && TimeElapsed(.25f)) {
                        SetStep(Step.Practice);
                        th.trialTimer = Time.realtimeSinceStartup; }
                    //display practiceStart for at least .25 seconds
                    if (IsStep(Step.Practice) && TimeElapsed(.25f)) {
                        SetStep(Step.Start);
                        th.trialTimer = Time.realtimeSinceStartup; } }
                
                if (IsStep(Step.PreRaster) && !displayPreRasterScreen) {
                    th.NextRaster();
                    displayPreRasterScreen = true; }
                else if (IsStep(Step.PreExperiment) && TimeElapsed(.25f)) {
                    Debug.Log("Closing UI Component");
                    SetStep(Step.Start);
                    th.trialTimer = Time.realtimeSinceStartup; }
                else if (displayPreRasterScreen && !trialStarted && th.step==0 && th.currentTrial==0 && !IsStep(Step.Start)) {
                    SetStep(Step.PreRaster); }
                else {
                    if (trialStarted) {
                        //if user has input an answer
                        if (waitingForInput && jsc.GetDirection() != JoystickController.JoyStickDirection.None) {
                            bool correctResponse = false;
                            if (jsc.GetDirection() == currentClipDirection)
                            {
                                correctResponse = true;
                                ui.textbox1.text = "Correct";
                                sh.Ding(); 
                            } else {
                                ui.textbox1.text = "Incorrect"; 
                                sh.Buzz(); }
                            ui.textbox1.enabled = true; 

                            if (th.phase > 1) {
                                th.lastTwentyCorrect.Enqueue(correctResponse ? 1 : 0);
                                th.num_correct += correctResponse ? 1 : 0;
                                th.num_attempted++;

                                float horizontal_input = Input.GetAxis("Horizontal");
                                float vertical_input = Input.GetAxis("Vertical");
                                
                                
                                th.WriteToSubjectFile(videoPlayer.clip.ToString().Replace(" (UnityEngine.VideoClip)", "") + "," +
                                                      horizontal_input + "," + vertical_input +
                                                      "," + correctResponse, true); }

                            ui.DisableAllComponentsUI();
                            waitingForInput = false;
                            trialStarted = false;

                            th.currentTrial++; // Change trial without recording

                            if (th.currentTrial >= 42 ) {
                               th.phase = 3;
                               th.currentTrial = 0;
                               th.step = 0;
                               th.updateDifficulty = false; 
                               SetStep(Step.PreRaster); }
                            else
                               SetStep(Step.Start);

                            if (th.currentTrial % 8 == 0) {
                               RandomizeVideos();
                               if (th.phase == 3 && th.currentTrial > th.numberAmpSettingRounds) {
                                   th.currentTrial = 0;
                                   th.phase++; }
                               if (th.phase == 4 && th.currentTrial >= numberOfTrials ) {
                                   th.currentTrial = 0;
                                   th.NextRaster();
                                   th.phase--; } }
                           
                            SetupClip();
                            th.trialTimer = Time.realtimeSinceStartup; } }
                    //else they've asked to start the next trial
                    else if (TimeElapsed(.25f) && IsStep(Step.Start)) {
                        ui.DisableAllComponentsUI();
                        trialStarted = true;
                        ui.textbox1.enabled = false; 

                        Debug.Log("Play Video Player");
                        VideoCanvas.SetActive(true);
                        videoPlayer.Play();
                        
                        continousTimer = Time.realtimeSinceStartup;
                        th.trialTimer = Time.realtimeSinceStartup;
                        ct.StartRecording(true);
                        if (th.phase == 3) {
                            bsh.resetTemporalValues = true;
                            th.updateDifficulty = false; } } } } 

            if (trialStarted && th.phase > 1) {
                if (Time.realtimeSinceStartup - continousTimer > continousInterval) {
                    th.WriteToTaggedFile("continuousMovement", videoPlayer.clip.ToString().Replace(" (UnityEngine.VideoClip)", "") +
                                                               "," + Input.GetAxis("Horizontal").ToString() + "," +
                                                               Input.GetAxis("Vertical").ToString(), true);
                    continousTimer = Time.realtimeSinceStartup; } }

            
            if (th.phase > 0 && trialStarted && Time.realtimeSinceStartup -th.trialTimer >= videoPlayer.clip.length/speedUpFactor) {
                GetFinalInput();
                videoPlayer.Stop(); } } }*/
    void Start() {
        th = TaskHandler.Instance;
        ui = UI_Handler.Instance;
        vm = VariableManagerScript.Instance;
        sh = SoundHandler.Instance;
        ct = CameraTracker.Instance;
        bsh = BackendShaderHandler.Instance;
        
        spvCamera = bsh.vrCamera;
        ui.SetPosition(UI_Handler.Position.FullScreen1, intro1); 
        ui.SetPosition(UI_Handler.Position.FullScreen2,  intro2); 
        ui.SetPosition(UI_Handler.Position.FullScreen3,  practiceStart); 
        ui.SetPosition(UI_Handler.Position.FullScreen4,  preExperiment);
        ui.SetPosition(UI_Handler.Position.FullScreen5,  preRaster);

        ui.SetPosition(UI_Handler.Position.PartialScreenMiddle1,  startTrial); 
        ui.SetPosition(UI_Handler.Position.PartialScreenMiddle2, breakScreen); 
        ui.SetPosition(UI_Handler.Position.PartialScreenMiddle3,  requestInput);
        
        ui.SetPosition(UI_Handler.Position.PartialScreenLeft, left);
        ui.SetPosition(UI_Handler.Position.PartialScreenTopLeft, upLeft);
        ui.SetPosition(UI_Handler.Position.PartialScreenTop, up);
        ui.SetPosition(UI_Handler.Position.PartialScreenTopRight, upRight);
        ui.SetPosition(UI_Handler.Position.PartialScreenRight, right);
        ui.SetPosition(UI_Handler.Position.PartialScreenBottomRight, downRight);
        ui.SetPosition(UI_Handler.Position.PartialScreenBottom, down);
        ui.SetPosition(UI_Handler.Position.PartialScreenBottomLeft, downLeft);
        
        th.trialTimer = Time.realtimeSinceStartup;
        SetStep(Step.Intro1);

        vm.runShaders = false; 
        th.TaskStart(experimentName, includedFiles, MainController.Instance.sharedSimulationSettings,
            false, true, numberOfTrials);
        th.WriteHeaderToTaggedFile("", "clip,horizontal,vertical,correct", true);
        th.WriteHeaderToTaggedFile("continuousMovement", "clip,horizontal,vertical,correct", true);
        RandomizeVideos();
        SetupClip();

        if (th.phase != 0) {
            Debug.Log("Starting from non-zero block/trial");
            SetStep(Step.Start);
            vm.runShaders = true; }
        else
        {
            th.phase = 1; } }
    
    void Awake() {
        videoClips = new VideoClip[]{rightMov, upRightMov, upMov, upLeftMov, leftMov, downLeftMov, downMov, downRightMov}; }

    private void SetupClip()
    {
        videoPlayer.clip = videoClips[th.currentTrial % 8];
        if (videoPlayer.clip.name == rightMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.Right;
        else if (videoPlayer.clip.name == downRightMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.DownRight;
        else if (videoPlayer.clip.name == downMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.Down;
        else if (videoPlayer.clip.name == downLeftMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.DownLeft;
        else if (videoPlayer.clip.name == leftMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.Left;
        else if (videoPlayer.clip.name == upLeftMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.UpLeft;
        else if (videoPlayer.clip.name == upMov.name)
            currentClipDirection = JoystickController.JoyStickDirection.Up;
        //else it's upRightMov
        else
            currentClipDirection = JoystickController.JoyStickDirection.UpRight; 
    }

    private bool IsStep(Step s)
    {
        return s == currentStep;
    }
    private void SetStep(Step s)
    {
        switch (s)
        {
            case Step.Intro1:
                ui.EnableOnly(intro1);
                break;
            case Step.Intro2:
                ui.EnableOnly(intro2);
                break;
            case Step.Practice:
                ui.EnableOnly(practiceStart);
                break;
            case Step.Start:
                ui.EnableOnly(startTrial);
                break;
            case Step.PreExperiment:
                ui.EnableOnly(preExperiment);
                break;
            case Step.PreRaster:
                ui.EnableOnly(preRaster);
                break;
            //if step is None, disable all ui images
            default:
                ui.DisableAllComponentsUI();
                return;
        }
        currentStep = s;
    }
}

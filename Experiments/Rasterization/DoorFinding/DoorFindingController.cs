using BionicVisionVR.Backend.Resources;
using BionicVisionVR.Resources;
using UnityEngine;

public class DoorFindingController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Texture2D makeSelection, startTrial, walkCenter;
    [SerializeField] private int numPracticeTrials = 3;
    [SerializeField] private int numPracticeTrialsBV = 10; 
    
    private int numTrials = 8;
    private int numTargets = 8;
    private bool trialStarted = false;
    private GameObject[] targets = new GameObject[8];
    private bool outOfTime; 
    
    private TaskHandler th;
    private MainController mc; 
    
    private readonly Vector3[] targetLocations = {
        new Vector3(-3.25f, 0, 0f),
        new Vector3(-2.298f, 2.298f, 45f),
        new Vector3(0, 3.25f, 90f),
        new Vector3(2.298f, 2.298f, 135f),
        new Vector3(3.25f, 0, 180f),
        new Vector3(2.298f, -2.298f, 225f),
        new Vector3(0, -3.25f, 270f),
        new Vector3(-2.298f, -2.298f, 315f)}; 
    
    
    
    // Start is called before the first frame update
    void Start() {
        th = TaskHandler.Instance;
        mc = MainController.Instance;
        VariableManagerScript.Instance.invertPreprocessing = true; 

        UI_Handler.Instance.SetPosition(UI_Handler.Position.PartialScreenMiddle1, makeSelection);
        UI_Handler.Instance.SetPosition(UI_Handler.Position.PartialScreenMiddle2,  startTrial);
        UI_Handler.Instance.SetPosition(UI_Handler.Position.FullScreen1, walkCenter); 
        
        th.TaskStart("Raster/Doorfinding", new string[0], mc.sharedSimulationSettings, false, true, numTrials );
        
        th.WriteHeaderToTaggedFile("", "ObjectSelected", false);
        
        for (int i = 0; i < numTargets; i++){  // Creates copies of target items
            targets[i] = i==0 ? target : GameObject.Instantiate(target);
            targets[i].transform.position =
                new Vector3(targetLocations[i][0], target.transform.position.y, targetLocations[i][1]);
            targets[i].transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles.x,
                targetLocations[i][2], target.transform.rotation.eulerAngles.z);
            targets[i].name = "Target" + i; }
        
        InitiateRoom(); }

    
    // Update is called once per frame
    void Update() {
        if (th.RunTask()) {
            if (trialStarted)
            {
                UI_Handler.Instance.textbox1.enabled = true; 
                UI_Handler.Instance.textbox1.text = ((int)(30.0f - (Time.realtimeSinceStartup - th.trialTimer))).ToString(); 
                outOfTime = Time.realtimeSinceStartup - th.trialTimer >= 30.0f; 
                
                if ( outOfTime || (CollisionHandler.Instance.collidedObject != "N/A" &
                                 CollisionHandler.Instance.collidedObject != "RoomMiddle")) 
                {
                    UI_Handler.Instance.EnableOnly(makeSelection);

                    if (th.GetTrigger() || outOfTime) {
                        UI_Handler.Instance.textbox1.text = "Trial: " + th.currentTrial; 
                        if (th.phase > 1) {
                            trialStarted = false;
                            CameraTracker.Instance.PauseRecording();
                            th.WriteToTaggedFile("", CollisionHandler.Instance.collidedObject);
                            th.num_attempted++;
                            th.num_correct += CollisionHandler.Instance.collidedObject == "7" ? 1 : 0;
                            th.lastTwentyCorrect.Enqueue(CollisionHandler.Instance.collidedObject == "7" ? 1 : 0);
                            th.currentTrial++;
                            
                            if (th.phase > 2 & th.currentTrial >= numTrials) {
                                BackendShaderHandler.Instance.RunShaderAfterFrames();
                                th.currentTrial = 0; 
                                if (th.phase == 4) {
                                    th.NextRaster();
                                    th.phase--; }
                                else if (th.phase == 3)
                                    th.phase++;
                            }if ((th.lastTwentyCorrect.Count == 20 && th.currentTrial >= 42) || th.currentTrial > 59){
                                th.phase = 3;
                                th.currentTrial = 0;
                                th.updateDifficulty = false;
                                th.NextRaster(); } }
                        else {
                            if (th.currentTrial > numPracticeTrials)
                                BackendShaderHandler.Instance.RunShaderAfterFrames();
                            if (th.currentTrial > numPracticeTrials + numPracticeTrialsBV) {
                                VariableManagerScript.Instance.runShaders = false;
                                th.currentTrial = 0;
                                th.phase = 2;
                                VariableManagerScript.Instance.predefinedSettings =
                                    PreDefinedSettings.RasterizationDifficulty0; }
                            th.currentTrial++; }

                        trialStarted = false;
                        CameraTracker.Instance.PauseRecording();
                        UI_Handler.Instance.EnableOnly(walkCenter); }
                } else {
                    UI_Handler.Instance.DisableAllComponentsUI(); }
            } else {
                VariableManagerScript.Instance.runShaders = false;
                if (CollisionHandler.Instance.collidedObject == "RoomMiddle") {
                    UI_Handler.Instance.EnableOnly(startTrial);
                    if (th.GetTrigger()) {
                        InitiateRoom();
                        trialStarted = true;
                        th.trialTimer = Time.realtimeSinceStartup; 
                        CameraTracker.Instance.StartRecording(true);
                        if (th.phase > 1 || th.currentTrial >= numPracticeTrials)
                            BackendShaderHandler.Instance.RunShaderAfterFrames();
                        if (th.phase > 2) {
                            BackendShaderHandler.Instance.resetTemporalValues = true; } }
                } else {
                    UI_Handler.Instance.EnableOnly(walkCenter); } } } }

   
    void InitiateRoom() {
        for (int i = 0; i < numTargets; i++) {
            for (int j = 0; j < numTargets; j++) {
                targets[i].transform.Find(j.ToString()).gameObject.SetActive(false); } }
        
        int[] numList = RandomizedList.GetRandomizedList(8); 
        
        for (int i=0; i<numTargets; i++)
            targets[i].transform.Find(numList[i].ToString()).gameObject.SetActive(true); }
}

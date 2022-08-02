using System;
using System.Linq;
using Assets.BionicVisionVR.Coding.Structs;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// This can be used to change between tasks
/// </summary>
public class MainController : MonoBehaviour
{
    public static MainController Instance { get; private set; }
    
    [SerializeField] private InputField SubjectNumberInputField, PhaseNumberInputField, StepNumberInputField, TrialNumberInputField;
    [SerializeField] private Dropdown EyeSelection;
    [SerializeField] public int startingDifficulty;
    [SerializeField] private int startingTrial;
    [SerializeField] private string[] taskList = {"Motion", "DoorFinding", "TumblingE"};
        
    public int numTasks = 3; 
    private int[] taskOrder = { 0, 1, 2 };
    public int subjectNumber;
    private int currentTask = -2;
    public int nextTask = -2;
    private bool checkTrial = true; // Should we check which trial to go to

    private int blackFrames = 5; 

    public PreDefinedSettings[] sharedSimulationSettings = new PreDefinedSettings[]
    {
        PreDefinedSettings.RasterizationDifficulty0,
        PreDefinedSettings.RasterizationDifficulty1,
        PreDefinedSettings.RasterizationDifficulty2,
        PreDefinedSettings.RasterizationDifficulty3,
        PreDefinedSettings.RasterizationDifficulty4,
        PreDefinedSettings.RasterizationDifficulty5,
        PreDefinedSettings.RasterizationDifficulty6,
        PreDefinedSettings.RasterizationDifficulty7,
        PreDefinedSettings.RasterizationDifficulty8,
        PreDefinedSettings.RasterizationDifficulty9,
        PreDefinedSettings.RasterizationDifficulty10
    };
    
    void Update()
    {
        if (currentTask != nextTask) GoToTask();
        
    }

    void GoToTask()
    {
        Random.InitState(subjectNumber + nextTask);
        if (nextTask == -1) {
            int[] randomOrder = new int[taskOrder.Length];
            for (int i = 0; i < taskOrder.Length; i++)
                randomOrder[i] = (int) Random.Range(0, 100f);

            Array.Sort(randomOrder, taskOrder);

            nextTask = taskOrder[0];
            checkTrial = false; }

        VariableManagerScript.Instance.subjectNumber =
            SubjectNumberInputField.text.Equals("") ? 0 : int.Parse(SubjectNumberInputField.text);

        if (PhaseNumberInputField.text != "")
            TaskHandler.Instance.phase = (int.Parse(PhaseNumberInputField.text));

        // if (StepNumberInputField.text != "")
            TaskHandler.Instance.step = StepNumberInputField.text != "" ? (int.Parse(StepNumberInputField.text)) : 0;

        if (TrialNumberInputField.text != "")
            TaskHandler.Instance.currentTrial = (int.Parse(TrialNumberInputField.text));

        Debug.Log(EyeSelection.value); 
        if (EyeSelection.value == 1) {
            UI_Handler.Instance.TheBlackness(true);
            if (blackFrames > 0) {
                blackFrames--;
                return; }
            
            VariableManagerScript.Instance.useLeftEye = true;
            UI_Handler.Instance.TheBlackness(false); }

        if (checkTrial) {
            TaskHandler.Instance.currentBlock =
                PhaseNumberInputField.text != "" ? Int32.Parse(PhaseNumberInputField.text) : 0; 
            TaskHandler.Instance.currentTrial =
                TrialNumberInputField.text != "" ? Int32.Parse(TrialNumberInputField.text) : 0; 
            TaskHandler.Instance.currentDifficulty =
                StepNumberInputField.text != "" ? Int32.Parse(StepNumberInputField.text) : 0; }

        SceneManager.LoadScene(taskList[nextTask]);
        currentTask = nextTask;
        TaskHandler.Instance.SetRasterOrder();
    }
    
    public void SelectTask(int task)
    {
        Debug.Log("Selecting task: " + task);
        nextTask = task; 
    }
    
    private void Awake()
    {
        if ( Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); }
        else
            Destroy(gameObject);
    }
}

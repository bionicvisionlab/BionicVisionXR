using System.IO;
using BionicVisionVR.Coding.Resources;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    public bool checkTrial;
    public bool checkSubject; 
    public int trialInterval;
    public InputField SubjectNumberInputField, BlockNumberInputField, TrialNumberInputField;
    public Dropdown EyeSelection;
    public Canvas MainCanvas;
    public Canvas ErrorWarningCanvas;
    public Canvas TrialWarningCanvas; 
    public Text ErrorWarning;
    public string sceneToLoadNext;
    public string introSceneName; 
    
    public static SceneManagementScript Instance { get; private set; }

    public void SetSceneToObjectName()
    {
        sceneToLoadNext = gameObject.name; 
    }
    
    public void GoToSceneByName(string sceneName)
    {
        if(!sceneName.Equals(""))
            sceneToLoadNext = sceneName;
        if (SubjectNumberInputField.text.Equals(""))
            VariableManagerScript.Instance.subjectNumber = 0; 
        else
            VariableManagerScript.Instance.subjectNumber = int.Parse(SubjectNumberInputField.text);
        
        if (BlockNumberInputField.text != "")
        {
            ExperimentHandler.Instance.currentBlock = (int.Parse(BlockNumberInputField.text) - 1);
        }
        if (TrialNumberInputField.text != "")
        {
            ExperimentHandler.Instance.currentTrial = (int.Parse(TrialNumberInputField.text) - 1);
        }

        if (EyeSelection.value == 1)
        {
            VariableManagerScript.Instance.useLeftEye = true; 
        }

        if(CheckTrial())
            SceneManager.LoadScene(sceneToLoadNext);
    }

    public void CheckSubjectNumber()
    {
        if (checkSubject)
        {
            int subjectNumber = 0;
            while (File.Exists(Application.dataPath + Path.DirectorySeparatorChar + gameObject.name +
                               Path.DirectorySeparatorChar + "Experiment_Data" + Path.DirectorySeparatorChar +
                               subjectNumber + "_" + gameObject.name + ".csv"))
            {
                subjectNumber++;
            }

            if (subjectNumber != int.Parse(SubjectNumberInputField.text))
            {
                MainCanvas.GetComponent<Canvas>().enabled = false;
                ErrorWarningCanvas.GetComponent<Canvas>().enabled = true;
                ErrorWarning.text = "WARNING!!! Subject number does not match automatically parsed subject number (" +
                                    subjectNumber + ")  Continue?";
            }
        }
    }

    public void OverrideEnterScene()
    {
        SceneManager.LoadScene(SceneManagementScript.Instance.sceneToLoadNext); 
    }
    
    public bool CheckTrial()
    {
        if(checkTrial)
            if (TrialNumberInputField.text != "" && int.Parse(TrialNumberInputField.text) % trialInterval == 0)
            {
                TrialWarningCanvas.GetComponent<Canvas>().enabled = true;
                return false; 
            }

        return true; 
    }

    public void ReloadIntroScene()
    {
        SceneManager.LoadScene(introSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadIntroScene();
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
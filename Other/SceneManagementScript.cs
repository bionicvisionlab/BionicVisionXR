using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    public InputField SubjectNumberInputField, BlockNumberInputField, TrialNumberInputField;
    public Dropdown EyeSelection;
    public Canvas MainCanvas;
    public Canvas ErrorWarningCanvas;
    public Canvas TrialWarningCanvas; 
    public Text ErrorWarning; 

    public void GoToTestScene()
    {
        SceneManager.LoadScene("PolyRetina");
    }
    
    public void GoToDoorScene()
    {
        SceneManager.LoadScene("PolyRetina");
    }
    
    public void CheckSubjectLetterRecognitionScene()
    {
        int subjectNumber = 0; 
        while( File.Exists( Application.dataPath + Path.DirectorySeparatorChar + "LetterRecognitionTask" + 
                            Path.DirectorySeparatorChar + "Experiment_Data" + Path.DirectorySeparatorChar +
                            subjectNumber + "_" +
                            "Letter_Recognition_Task.csv"))
        {
            subjectNumber++; 
        }
        if (subjectNumber != int.Parse(SubjectNumberInputField.text))
        {
            MainCanvas.GetComponent<Canvas> ().enabled = false;
            ErrorWarningCanvas.GetComponent<Canvas> ().enabled = true;
            ErrorWarning.text = "WARNING!!! Subject number does not match automatically parsed subject number (" +
                                subjectNumber + ")  Continue?"; 
        }
        else
        {
            GoToLetterRecognitionScene(); 
        }
    }

    public void GoToLetterRecognitionScene()
    {
        VariableManagerScript.Instance.subjectNumber = int.Parse(SubjectNumberInputField.text);
        
        if (BlockNumberInputField.text != "")
        {
            LetterRecognitionExperimentHandler.Instance.currentBlock = (int.Parse(BlockNumberInputField.text) - 1);
        }
        if (TrialNumberInputField.text != "")
        {
            LetterRecognitionExperimentHandler.Instance.currentTrial = (int.Parse(TrialNumberInputField.text) - 1);
        }
        LetterRecognitionExperimentHandler.Instance.ExperimentStartup();

        if (EyeSelection.value == 1)
        {
            VariableManagerScript.Instance.useLeftEye = true; 
        }
        SceneManager.LoadScene("ComputerLetterDetectionTask");
    }
    
    public void CheckSubjectHallwayScene()
    {
        int subjectNumber = 0; 
        while( File.Exists( Application.dataPath + Path.DirectorySeparatorChar + "NavigationHallwayTask" + 
                            Path.DirectorySeparatorChar + "Experiment_Data" + Path.DirectorySeparatorChar +
                            subjectNumber + "_" +
                            "HallwayTask.csv"))
        {
            subjectNumber++; 
        }

        if (TrialNumberInputField.text!="" && int.Parse(TrialNumberInputField.text) % 2 == 0)
        {
            TrialWarningCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (subjectNumber != int.Parse(SubjectNumberInputField.text))
        {
            MainCanvas.GetComponent<Canvas> ().enabled = false;
            ErrorWarningCanvas.GetComponent<Canvas> ().enabled = true;
            ErrorWarning.text = "WARNING!!! Subject number does not match automatically parsed subject number (" +
                                subjectNumber + ")  Continue?"; 
        }
        else
        {
            GoToHallwayNavigationScene(); 
        }
    }
    public void GoToHallwayNavigationScene()
    {
        VariableManagerScript.Instance.subjectNumber = int.Parse(SubjectNumberInputField.text);
        
        if (BlockNumberInputField.text != "")
        {
            HallwayTaskController.Instance.currentBlock = (int.Parse(BlockNumberInputField.text) );
        }
        if (TrialNumberInputField.text != "")
        {
            HallwayTaskController.Instance.currentTrial = (int.Parse(TrialNumberInputField.text) - 1);
        }
        HallwayTaskController.Instance.ExperimentStartup();

        if (EyeSelection.value == 1)
        {
            VariableManagerScript.Instance.useLeftEye = true; 
        }
        SceneManager.LoadScene("HallwayScene");
    }

    public void reloadIntroScene()
    {
        SceneManager.LoadScene("introScene");
    }
    

}
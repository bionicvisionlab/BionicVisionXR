// using System;
// using BionicVisionVR.Coding.Resources;
// using BionicVisionVR.Resources;
// // using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace Experiments.SampleExperiment
// {
//     public class SampleExperimentHandler : MonoBehaviour
//     {
//         public GameObject Cube1;
//         public GameObject Cube2; 
//         
//         private string experimentName = "SampleExperiment";
//         private string[] includedFiles = {"file1", "file2"};
//
//         private PreDefinedSettings[] blockSettings = new PreDefinedSettings[]
//             {PreDefinedSettings.LetterTask1, PreDefinedSettings.LetterTask2};
//
//         private int numberOfTrials = 1; // Number of trials per block
//         private int numberOfPracticeTrials = 1; // Number of trials without SPV
//
//         public bool shouldWriteToFile1 = false;
//         public bool shouldWriteToFile2 = false;
//         
//
//         public void Start()
//         {
//            // TaskHandler.Instance.TaskStart(experimentName, includedFiles, blockSettings, false, numberOfTrials);
//         }
//
//         public void Update()
//         {
//             if (TaskHandler.Instance.GetBlockTrialSingleDimension() > numberOfPracticeTrials)
//                 VariableManagerScript.Instance.runShaders = true; 
//             
//             if (shouldWriteToFile1)
//             {
//                 TaskHandler.Instance.WriteToTaggedFile("file1", "Trial "+ TaskHandler.Instance.currentTrial+ "  complete!");
//                 TaskHandler.Instance.NextTrial();
//                 VariableManagerScript.Instance.runShaders = true; 
//                 shouldWriteToFile1 = false; 
//             }
//             
//             if (shouldWriteToFile2)
//             {
//                 TaskHandler.Instance.WriteToTaggedFile("file1", "Trial "+ TaskHandler.Instance.currentTrial+ "  complete!");
//                 TaskHandler.Instance.currentTrial++;
//                 shouldWriteToFile2 = false; 
//             }
//         }
//     }
// }
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO; 
using BionicVisionVR.Resources;
using UnityEngine;
using Valve.VR;
using Random = UnityEngine.Random;

namespace BionicVisionVR.Coding.Resources
{
    /// <summary>
    /// Handles which block the experiment is on, requires StartExperiment() to be called at the beginning by the
    /// individual experiment handler. This requires the name of your experiment (which should match the folder name),
    /// a list of file tags that should be updated with each new trial, a list of block settings, and whether or not the
    /// first block in your list should be randomized.
    /// </summary>
    public class ExperimentHandler : MonoBehaviour
    {
        public static ExperimentHandler Instance { get; private set; }

        public bool useHMD;
        public bool cameraTracking = true; 
        
        public int currentBlock = 0;
        public int currentTrial = 0;
        private int numberOfTrials = 10;
        public float trialTimer; 
        private FileHandler fileHandler = new FileHandler();

        public string subjectFile; 
        private string[] filesList= new string[]{}; // List of files that should have block/trial information updates

        private PreDefinedBlocks[] blockSettings = new PreDefinedBlocks[]
            {PreDefinedBlocks.LetterTask1, PreDefinedBlocks.LetterTask5, PreDefinedBlocks.LetterTask9};

        // Should be called at the beginning of each experiment
        public void ExperimentStartup(string experimentName, string[] filesList, PreDefinedBlocks[] blockSettings,
            bool randomizeFirstBlock, int numberOfTrials)
        {
            Debug.Log("Experiment start"); 
            string dataDirectory = Application.dataPath + Path.DirectorySeparatorChar + "Experiments" +
                                   Path.DirectorySeparatorChar +
                                   experimentName + Path.DirectorySeparatorChar +
                                   "Experiment_Data" + Path.DirectorySeparatorChar;
            System.IO.Directory.CreateDirectory(dataDirectory);
            subjectFile = dataDirectory + VariableManagerScript.Instance.subjectNumber + ".csv";
            this.filesList = filesList;
            
            WriteToAllTaggedFiles("Start time: " + System.DateTime.Now);

            Random.InitState(VariableManagerScript.Instance.subjectNumber * 10);

            for (int t = 0; t < blockSettings.Length; t++)
            {
                if (t > 0 || randomizeFirstBlock)
                {
                    PreDefinedBlocks tmp = blockSettings[t];

                    int r = Random.Range(t, blockSettings.Length);
                    blockSettings[t] = blockSettings[r];
                    blockSettings[r] = tmp;
                }
            }

            this.blockSettings = blockSettings;
            this.numberOfTrials = numberOfTrials;
            this.useHMD = useHMD; 
        }

        public void WriteToTaggedFile(string fileTag, string toWrite)
        {
            fileHandler.AppendLine(subjectFile.Replace(".csv", "_" + fileTag + ".csv"), toWrite); 
        }

        public void WriteToAllTaggedFiles(string toWrite)
        {
            fileHandler.AppendLine(subjectFile, toWrite);
            foreach (string fileTag in filesList)
            {
                WriteToTaggedFile(fileTag, toWrite);
            }

            if (cameraTracking)
            {
                fileHandler.AppendLine(subjectFile.Replace(".csv", "_head.csv"), toWrite);
                fileHandler.AppendLine(subjectFile.Replace(".csv","_head-position.csv"), toWrite);
                fileHandler.AppendLine(subjectFile.Replace(".csv", "_head-position-easy-paste.csv"), toWrite); 
            }
        }

        public void NextTrial()
        {
            currentTrial++; 
            if (currentTrial == numberOfTrials)
            {
                currentBlock++;
                currentTrial = 0; 
            }
            
            if (VariableManagerScript.Instance.predefinedSettings != blockSettings[currentBlock])
            {
                WriteToAllTaggedFiles(blockSettings[currentBlock].ToString());
                VariableManagerScript.Instance.predefinedSettings = blockSettings[currentBlock];
            }
            WriteToAllTaggedFiles("Trial: " + currentTrial);

            trialTimer = Time.time; 
        }

        private void Awake()
        {
            subjectFile = Application.dataPath + Path.DirectorySeparatorChar + "Experiments" +
                          Path.DirectorySeparatorChar + "Debug" +
                          Path.DirectorySeparatorChar + "subjectFile.csv";
            if (Instance == null)
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
}

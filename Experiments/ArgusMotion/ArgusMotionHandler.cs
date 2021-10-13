using System;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; 

namespace Experiments.ArgusMotion
{
    public class ArgusMotionHandler : MonoBehaviour
    {
        //[SerializeField] private VideoClip[] videoClips = new VideoClip[numberVideos];
        private string experimentName = "ArgusMotion";
        private string[] includedFiles = {"file1", "file2"};

        private PreDefinedBlocks[] blockSettings = new PreDefinedBlocks[]
            {PreDefinedBlocks.LetterTask1, PreDefinedBlocks.LetterTask2};

        private int numberOfTrials = 1; // Number of trials per block
        private int numberOfPracticeTrials = 1; // Number of trials without SPV
        private static int numberVideos = 10;
        [SerializeField] private VideoClip[] videoClips = new VideoClip[numberVideos]; 
        [SerializeField] private bool shouldWriteToFile1 = false;

        

        public void Start()
        {
            ExperimentHandler.Instance.ExperimentStartup(experimentName, includedFiles, blockSettings, false, numberOfTrials);
            var videoPlayer = gameObject.GetComponent<VideoPlayer>();
            videoPlayer.clip = videoClips[ExperimentHandler.Instance.currentTrial]; 

        }

        public void Update()
        {
            if (ExperimentHandler.Instance.GetBlockTrialSingleDimension() > numberOfPracticeTrials)
                VariableManagerScript.Instance.runShaders = true; 
            
            if (shouldWriteToFile1)
            {
                ExperimentHandler.Instance.WriteToTaggedFile("file1", "Trial "+ ExperimentHandler.Instance.currentTrial+ "  complete!");
                ExperimentHandler.Instance.NextTrial();
                VariableManagerScript.Instance.runShaders = true; 
                shouldWriteToFile1 = false; 
            }
            
        }
    }
}
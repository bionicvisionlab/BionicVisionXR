using System;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = System.Random;

namespace Experiments.ArgusMotion
{
    public class ArgusMotionHandler : MonoBehaviour
    {
        [SerializeField] private RawImage requestInput; 
        [SerializeField] private RawImage intro1, intro2, practiceStart, finish, preExperiment, startTrial;

        private string experimentName = "ArgusMotion";
        private string[] includedFiles = {"continuousMovement"};

        private VideoPlayer videoPlayer;
        private double timer;

        private bool waitingForInput; 
        private bool trialStarted;
        private bool intro = true; 
        private bool experimentStarted; // past practice trials

        private PreDefinedBlocks[] blockSettings = new PreDefinedBlocks[]
        {
            PreDefinedBlocks.ArgusMotion1, PreDefinedBlocks.ArgusMotion2, PreDefinedBlocks.ArgusMotion3,
            PreDefinedBlocks.ArgusMotion4, PreDefinedBlocks.ArgusMotion5, PreDefinedBlocks.ArgusMotion6,
            PreDefinedBlocks.ArgusMotion7, PreDefinedBlocks.ArgusMotion8, PreDefinedBlocks.ArgusMotion9
            
        };

        private int numberOfTrials = 8; // Number of trials per block
        private int numberOfPracticeTrials = 3; // Number of trials without SPV
        private static int numberVideos = 8;
        [SerializeField] private VideoClip[] videoClips = new VideoClip[numberVideos];

        private void RandomizeVideos()
        {
            UnityEngine.Random.InitState(VariableManagerScript.Instance.subjectNumber + ExperimentHandler.Instance.currentBlock);
            for (int i = 0; i < numberVideos; i++)
            {
                var tmp = videoClips[i];
                int randomSpot = UnityEngine.Random.Range(i, numberVideos);
                videoClips[i] = videoClips[randomSpot];
                videoClips[randomSpot] = tmp; 
            }
        }

        private void GetFinalInput()
        {
            requestInput.enabled = true;
            waitingForInput = true; 
        }
        
        public void Start()
        {
            timer = Time.realtimeSinceStartupAsDouble; 
            intro1.enabled = true;
            intro2.enabled = false;
            practiceStart.enabled = false;
            finish.enabled = false;
            preExperiment.enabled = false;
            startTrial.enabled = false; 
            
            VariableManagerScript.Instance.runShaders = false; 
            ExperimentHandler.Instance.ExperimentStartup(experimentName, includedFiles, blockSettings, false, numberOfTrials);
            videoPlayer = gameObject.GetComponent<VideoPlayer>();
            videoPlayer.clip = videoClips[ExperimentHandler.Instance.currentTrial]; 

            RandomizeVideos();

            if (ExperimentHandler.Instance.GetBlockTrialSingleDimension() > 0)
            {
                Debug.Log("Starting from non-zero block/trial"); 
                intro = false;
                experimentStarted = true;
                intro1.enabled = false;
                startTrial.enabled = true;
                VariableManagerScript.Instance.predefinedSettings =
                    blockSettings[ExperimentHandler.Instance.currentBlock]; 
                VariableManagerScript.Instance.runShaders = true;
            }
        }

        private bool TimeElapsed()
        {
            return Time.realtimeSinceStartupAsDouble - timer > .25; 
        }

        public void Update()
        {
            if (ExperimentHandler.Instance.GetBlockTrialSingleDimension() >= numberOfPracticeTrials && !experimentStarted)
            {
                
                ExperimentHandler.Instance.currentTrial = 0; 
                RandomizeVideos();
                experimentStarted = true;
                VariableManagerScript.Instance.runShaders = true;
                preExperiment.enabled = true; 
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire1")>0 )
            {
                if (intro)
                {
                    if (intro1.enabled && TimeElapsed()  )
                    {
                        intro1.enabled = false;
                        intro2.enabled = true;
                        timer = Time.realtimeSinceStartupAsDouble;
                    }
                    if (intro2.enabled && TimeElapsed() )
                    {
                        intro2.enabled = false;
                        practiceStart.enabled = true;
                        timer = Time.realtimeSinceStartupAsDouble;
                    }

                    if (practiceStart.enabled && TimeElapsed() )
                    {
                        practiceStart.enabled = false;
                        startTrial.enabled = true; 
                        intro = false; 
                        timer = Time.realtimeSinceStartupAsDouble;
                    }
                }
                else if (preExperiment.enabled && TimeElapsed() )
                {
                    preExperiment.enabled = false;
                    startTrial.enabled = true; 
                    timer = Time.realtimeSinceStartupAsDouble;
                }
                else
                {
                    if (trialStarted)
                    {
                        if (waitingForInput)
                        {
                            if (experimentStarted)
                            {
                                ExperimentHandler.Instance.WriteToSubjectFile(Input.GetAxis("Horizontal") + "," +
                                                                              Input.GetAxis("Vertical"));
                                if (ExperimentHandler.Instance.NextTrialCheckForNewBlock())
                                    RandomizeVideos();
                            }
                            else
                            {
                                ExperimentHandler.Instance.currentTrial++; // Change trial without recording
                            }

                            requestInput.enabled = false;
                            waitingForInput = false;
                            trialStarted = false;
                            if (ExperimentHandler.Instance.GetBlockTrialSingleDimension() ==
                                ExperimentHandler.Instance.ConvertBlockTrialToSingleDimension(blockSettings.Length,
                                    numberOfTrials))
                                finish.enabled = true;
                            else
                                startTrial.enabled = true;

                            timer = Time.realtimeSinceStartupAsDouble; 
                            videoPlayer.clip = videoClips[ExperimentHandler.Instance.currentTrial];
                            ExperimentHandler.Instance.WriteToAllTaggedFiles(videoClips[ExperimentHandler.Instance.currentTrial].name);
                        }
                    }
                    else
                    {
                        if (TimeElapsed())
                        {
                            startTrial.enabled = false;
                            trialStarted = true;
                            videoPlayer.Play();
                            timer = Time.realtimeSinceStartupAsDouble;
                        }
                    }
                }
            }

            if (trialStarted)
            {
                ExperimentHandler.Instance.WriteToTaggedFile("continuousMovement",
                    Input.GetAxis("Horizontal") + "," + Input.GetAxis("Vertical"));
                if (Time.realtimeSinceStartupAsDouble - timer >= videoPlayer.length/2.0)
                {
                    GetFinalInput();
                    videoPlayer.Stop();
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BionicVisionVR.Resources;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LetterSwitching : MonoBehaviour
{
    private float letterLoaded = 0; 
    public AudioSource beepSound;
    private bool[] playedBeeps = new bool[8]{false,false,false,false,false,false,false,false}; // 30 sec, 15 sec, 10 sec, 5 sec, 3/2/1 sec, out of time; 

    private int letterSelectionInt = 13;
    private float letterSelectTimer = 0.0f;
    private Material materialToRevertAfterLetterSelect;
    private bool revertMaterial = false; 

    public RawImage endOfExperiment; 
    private bool letterSelectionMode = false;

    private bool pauseScreen = true;
    private float pauseScreenTimer = 0.0f; 

    private Renderer thisRenderer;

   private FileHandler fileHandler = new FileHandler();
   private bool justDeletedLine = true; 

   public Texture testTexture;
   public RawImage instructions, letterSelected, practiceRound,  clickToSelect, clickAgain, greatJob, tryAgain, startingSim; 
    
    public Material letterA, letterB,letterC, letterD, letterE, letterF, letterG, letterH, letterI, letterJ, letterK, letterL, letterM, letterN, letterO, letterP, letterQ, letterR, letterS, letterT, letterU, letterV, letterW, letterX, letterY, letterZ, bionicVisionEyeball, screenOff; 
    public Texture letterATexture, letterBTexture, letterCTexture, letterDTexture, letterETexture, letterFTexture, letterGTexture, letterHTexture, letterITexture, letterJTexture, letterKTexture, letterLTexture, letterMTexture, letterNTexture, letterOTexture, letterPTexture, letterQTexture, letterRTexture, letterSTexture, letterTTexture, letterUTexture, letterVTexture, letterWTexture, letterXTexture, letterYTexture, letterZTexture;

    private bool practiceI, practiceQ; 
    
    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        bool nextTrial = false;

        if (LetterRecognitionExperimentHandler.Instance.currentBlock >= LetterRecognitionExperimentHandler.Instance.blockSettings.Length)
        {
            thisRenderer.material = screenOff;
            VariableManagerScript.Instance.runShaders = false;
            BackendShaderHandler.Instance.pleaseWait.enabled = false; 
            endOfExperiment.enabled = true;
        }
        else if (LetterRecognitionExperimentHandler.Instance.startRecording || practiceQ || practiceI)
        {
            if (LetterRecognitionExperimentHandler.Instance.startRecording)
            {
                greatJob.enabled = false;
                startingSim.enabled = false;
                clickToSelect.enabled = false;
                clickAgain.enabled = false;
                tryAgain.enabled = false; 
                setMaterial();
            }
            letterSelection(); 
            if(!pauseScreen)
                playBeeps(); 

        }
        else if (Input.GetKeyDown(KeyCode.Escape) )
        {
            instructions.enabled = false;
            practiceRound.enabled = true; 
            practiceQ = true;
            thisRenderer.material = letterQ;
            clickToSelect.enabled = true; 
        }
        
        if (revertMaterial)
        {
            thisRenderer.material = materialToRevertAfterLetterSelect;
            revertMaterial = false; 
        }
    }

    private void letterSelection()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LetterRecognitionExperimentHandler.Instance.startRecording)
        {
            if (pauseScreen)
            {
                letterLoaded = letterLoaded + (Time.time-pauseScreenTimer);
                instructions.enabled = false;
            }
            else
            {
                instructions.enabled = true; 
                pauseScreenTimer = Time.time;
            }

            pauseScreen = !pauseScreen;
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace) && !justDeletedLine && LetterRecognitionExperimentHandler.Instance.currentTrial!=0)
        {
            fileHandler.removeLastLine(LetterRecognitionExperimentHandler.Instance.subjectFile, 2);

            LetterRecognitionExperimentHandler.Instance.currentTrial--;
            setMaterial();
            justDeletedLine = true; 
        }
        
            if (!letterSelectionMode)
            {
                letterSelected.enabled = false; 
                if (/*Input.GetKeyDown(KeyCode.Space) ||*/ Input.GetMouseButtonDown(0))
                {
                    letterSelectionMode = true;
                    letterSelectTimer = Time.time;
                    materialToRevertAfterLetterSelect = thisRenderer.material;
                    thisRenderer.material = screenOff; 
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) //exit letter select mode
                {
                    letterSelectionMode = false;
                    letterLoaded = letterLoaded + (Time.time - letterSelectTimer);
                    revertMaterial = true; 
                }
                
                if (practiceQ || practiceI)
                    clickAgain.enabled = true; 
            
                instructions.enabled = false;
                letterSelected.enabled = true; 
                letterSelectionInt = letterSelectionInt += (int) Input.mouseScrollDelta.y;
                /*if (Input.GetKeyDown(KeyCode.UpArrow))
                    letterSelectionInt++;
                if(Input.GetKeyDown(KeyCode.DownArrow))
                    letterSelectionInt--; 
                  */  
                if (letterSelectionInt > 26)
                {
                    letterSelectionInt = 26;
                }

                if (letterSelectionInt < 1)
                {
                    letterSelectionInt = 1;
                }

                switch (letterSelectionInt)
                {
                    case 1:
                        letterSelected.texture = letterATexture; 
                        break;
                    case 2:
                        letterSelected.texture = letterBTexture;
                        break; 
                    case 3:
                        letterSelected.texture = letterCTexture;
                        break; 
                    case 4:
                        letterSelected.texture = letterDTexture;
                        break; 
                    case 5:
                        letterSelected.texture = letterETexture;
                        break; 
                    case 6:
                        letterSelected.texture = letterFTexture;
                        break; 
                    case 7:
                        letterSelected.texture = letterGTexture;
                        break; 
                    case 8:
                        letterSelected.texture = letterHTexture;
                        break; 
                    case 9:
                        letterSelected.texture = letterITexture;
                        break; 
                    case 10:
                        letterSelected.texture = letterJTexture;
                        break; 
                    case 11:
                        letterSelected.texture = letterKTexture;
                        break; 
                    case 12:
                        letterSelected.texture = letterLTexture;
                        break; 
                    case 13:
                        letterSelected.texture = letterMTexture;
                        break; 
                    case 14:
                        letterSelected.texture = letterNTexture;
                        break; 
                    case 15:
                        letterSelected.texture = letterOTexture;
                        break; 
                    case 16:
                        letterSelected.texture = letterPTexture;
                        break; 
                    case 17:
                        letterSelected.texture = letterQTexture;
                        break; 
                    case 18:
                        letterSelected.texture = letterRTexture;
                        break; 
                    case 19:
                        letterSelected.texture = letterSTexture;
                        break; 
                    case 20:
                        letterSelected.texture = letterTTexture;
                        break; 
                    case 21:
                        letterSelected.texture = letterUTexture;
                        break; 
                    case 22:
                        letterSelected.texture = letterVTexture;
                        break; 
                    case 23:
                        letterSelected.texture = letterWTexture;
                        break; 
                    case 24:
                        letterSelected.texture = letterXTexture;
                        break; 
                    case 25:
                        letterSelected.texture = letterYTexture;
                        break;
                    case 26:
                        letterSelected.texture = letterZTexture;
                        break; 
                }

                if (Input.GetKeyDown(KeyCode.Space) /*|| Input.GetMouseButtonDown(0)*/)
                {
                    justDeletedLine = false;
                    greatJob.enabled = false; 
                    if (LetterRecognitionExperimentHandler.Instance.startRecording)
                    {
                        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile,
                            LetterRecognitionExperimentHandler.Instance.letterArray[
                                LetterRecognitionExperimentHandler.Instance.currentTrial]
                            + ", " + ((char) ('a' + letterSelectionInt - 1)).ToString().ToLowerInvariant() + " - ("  + ") " +
                            (Time.time - letterLoaded) + " seconds");
                        //+LetterRecognitionExperimentHandler.Instance.randomizedArray[LetterRecognitionExperimentHandler.Instance.currentTrial]

                        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile,
                            LetterRecognitionExperimentHandler.Instance
                                .letterArray[LetterRecognitionExperimentHandler.Instance.currentTrial].ToString()
                                .Equals(((char) ('a' + letterSelectionInt - 1)).ToString().ToLowerInvariant())
                                ? "1"
                                : "0");
                    }
                    else
                    {
                        if (practiceQ)
                        {
                            practiceRound.enabled = false; 
                            if (letterSelectionInt == 17)
                            {
                                greatJob.enabled = true;
                                tryAgain.enabled = false; 
                                practiceQ = false;
                                practiceI = true;
                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;
                                VariableManagerScript.Instance.runShaders = true; 
                                thisRenderer.material = letterI;
                                LetterRecognitionExperimentHandler.Instance.lightsOff = true; 
                            }
                            else
                            {
                                tryAgain.enabled = true;
                                clickToSelect.enabled = true;
                            }
                        }
                        else if (practiceI)
                        {
                            if (letterSelectionInt == 9)
                            {
                                practiceI = false; 
                                greatJob.enabled = true;
                                tryAgain.enabled = false; 
                                startingSim.enabled = true;
                                VariableManagerScript.Instance.predefinedSettings = LetterRecognitionExperimentHandler.Instance.blockSettings[LetterRecognitionExperimentHandler.Instance.currentBlock];
                                LetterRecognitionExperimentHandler.Instance.startRecording = true;
                                LetterRecognitionExperimentHandler.Instance.currentTrial = LetterRecognitionExperimentHandler.Instance.currentTrial-1; 
                                letterLoaded = Time.time;
                                playedBeeps = new bool[8] {false, false, false, false, false, false, false, false};
                                pauseScreen = false;
                            }
                            else
                            {
                                tryAgain.enabled = true;
                                clickToSelect.enabled = true;
                            }
                        }
                    }

                    clickAgain.enabled = false; 
                    letterSelectionMode = false;
                    letterSelectionInt = 13; 
                    
                    if(LetterRecognitionExperimentHandler.Instance.startRecording)
                        LetterRecognitionExperimentHandler.Instance.currentTrial++;
                    if (LetterRecognitionExperimentHandler.Instance.currentTrial ==
                        LetterRecognitionExperimentHandler.Instance.letterArray.Length)
                    {
                        BackendShaderHandler.Instance.pleaseWait.enabled = true;
                    }

                    if (LetterRecognitionExperimentHandler.Instance.startRecording)
                    {
                        setMaterial();
                        letterLoaded = Time.time;
                        playedBeeps = new bool[8] {false, false, false, false, false, false, false, false};
                    }

                }
            }
         /*  [Previous block of code for keyboard input selection] 
             else
            {
                foreach (KeyCode kcode in new KeyCode[]
                {
                    KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H,
                    KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P,
                    KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
                    KeyCode.Y, KeyCode.Z
                })
                {
                    if (Input.GetKeyDown(kcode))
                    {
                    
                        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile,
                             LetterRecognitionExperimentHandler.Instance.letterArray[
                                 LetterRecognitionExperimentHandler.Instance.currentTrial]
                             + ", " + kcode.ToString().ToLowerInvariant());

                        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile,
                             LetterRecognitionExperimentHandler.Instance
                                 .letterArray[LetterRecognitionExperimentHandler.Instance.currentTrial].ToString()
                                 .Equals(kcode.ToString().ToLowerInvariant())
                                 ? "1"
                                 : "0");
 
                         LetterRecognitionExperimentHandler.Instance.currentTrial++;
                         setMaterial();
                         letterLoaded = Time.time;
                         playedBeeps = new bool[8]{false,false,false,false,false,false,false,false};

                    }
                }
            }*/
    }

    private void playBeeps()
    {
       // Debug.Log(Time.time - letterLoaded);
        if (Time.time - letterLoaded > 45 && !playedBeeps[1])
        {
            beepSound.Play(); 
            playedBeeps[1] = true; 
        }
        if (Time.time - letterLoaded > 50 && !playedBeeps[2])
        {
            beepSound.Play(); 
            playedBeeps[2] = true; 
        }
        if (Time.time - letterLoaded > 55 && !playedBeeps[3])
        {
            beepSound.Play(); 
            playedBeeps[3] = true; 
        }
        if (Time.time - letterLoaded > 57 && !playedBeeps[4])
        {
            beepSound.Play(); 
            playedBeeps[4] = true; 
        }
        if (Time.time - letterLoaded > 58 && !playedBeeps[5])
        {
            beepSound.Play(); 
            playedBeeps[5] = true; 
        }
        if (Time.time - letterLoaded > 59 && !playedBeeps[6])
        {
            beepSound.Play(); 
            playedBeeps[6] = true; 
        }
        if (Time.time - letterLoaded > 60)
        {
            playedBeeps[7] = true; 
            thisRenderer.material = screenOff;
        }
    }

    private void setMaterial()
    {
        if (LetterRecognitionExperimentHandler.Instance.currentTrial <
            LetterRecognitionExperimentHandler.Instance.letterArray.Length && !playedBeeps[7])
        {
            if (LetterRecognitionExperimentHandler.Instance.debugMode)
            {
                Debug.Log(LetterRecognitionExperimentHandler.Instance.currentTrial);
                Debug.Log(LetterRecognitionExperimentHandler.Instance.letterArray[
                    LetterRecognitionExperimentHandler.Instance.currentTrial]);
            }
            switch (LetterRecognitionExperimentHandler.Instance.letterArray[
                LetterRecognitionExperimentHandler.Instance.currentTrial])
            {
                case 'a':
                    thisRenderer.material = letterA;
                    break;
                case 'b':
                    thisRenderer.material = letterB;
                    break;
                case 'c':
                    thisRenderer.material = letterC;
                    break;
                case 'd':
                    thisRenderer.material = letterD;
                    break;
                case 'e':
                    thisRenderer.material = letterE;
                    break;
                case 'f':
                    thisRenderer.material = letterF;
                    break;
                case 'g':
                    thisRenderer.material = letterG;
                    break;
                case 'h':
                    thisRenderer.material = letterH;
                    break;
                case 'i':
                    thisRenderer.material = letterI;
                    break;
                case 'j':
                    thisRenderer.material = letterJ;
                    break;
                case 'k':
                    thisRenderer.material = letterK;
                    break;
                case 'l':
                    thisRenderer.material = letterL;
                    break;
                case 'm':
                    thisRenderer.material = letterM;
                    break;
                case 'n':
                    thisRenderer.material = letterN;
                    break;
                case 'o':
                    thisRenderer.material = letterO;
                    break;
                case 'p':
                    thisRenderer.material = letterP;
                    break;
                case 'q':
                    thisRenderer.material = letterQ;
                    break;
                case 'r':
                    thisRenderer.material = letterR;
                    break;
                case 's':
                    thisRenderer.material = letterS;
                    break;
                case 't':
                    thisRenderer.material = letterT;
                    break;
                case 'u':
                    thisRenderer.material = letterU;
                    break;
                case 'v':
                    thisRenderer.material = letterV;
                    break;
                case 'w':
                    thisRenderer.material = letterW;
                    break;
                case 'x':
                    thisRenderer.material = letterX;
                    break;
                case 'y':
                    thisRenderer.material = letterY;
                    break;
                case 'z':
                    thisRenderer.material = letterZ;
                    break;
                
            }
        }
    }


}



using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.BionicVisionVR.Coding.Structs;
using BionicVisionVR.Resources;
using UnityEngine;

public class LetterRecognitionExperimentHandler : MonoBehaviour
{
    public static LetterRecognitionExperimentHandler Instance { get; private set; }

    public bool debugMode = false;

    public string subjectFile; 
    
    public int currentTrial = 0;
    public int currentBlock = 0;
    
    /*public float[] rhoSettings = new float[] {50f, 100f}; 
    public float[] lambdaSettings = new float[] {50f, 100f};
    public int[] electrode_spacing_Settings = new int[] {512}; 
    public int[] x_electrode_count_Settings = new int[] {10, 20}; 
    public int[] y_electrode_count_Settings = new int[] {10, 20};*/
    

    public char[] letterArray = new char[0];
    public bool[] randomizedArray = new bool[0]; 
    public PreDefinedBlocks[] blockSettings = new PreDefinedBlocks[] {PreDefinedBlocks.LetterTask1, PreDefinedBlocks.LetterTask5, PreDefinedBlocks.LetterTask9};
    //public BlockSettings[] blockSettings ;

    public bool startRecording = false;
    public bool lightsOff; 
    
    public FileHandler fileHandler = new FileHandler(); 
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ExperimentStartup()
    {
        
         subjectFile = Application.dataPath + Path.DirectorySeparatorChar +"LetterRecognitionTask" + Path.DirectorySeparatorChar
                      + "Experiment_Data" + Path.DirectorySeparatorChar +
                      VariableManagerScript.Instance.subjectNumber + "_" +
                      "Letter_Recognition_Task.csv";
         System.IO.Directory.CreateDirectory(Application.dataPath + Path.DirectorySeparatorChar +
                                             "LetterRecognitionTask"+ Path.DirectorySeparatorChar +
                                             "Experiment_Data" + Path.DirectorySeparatorChar); 
         System.IO.Directory.CreateDirectory(VariableManagerScript.Instance.configurationPath); 
        
        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile, "Start time: "+System.DateTime.Today.ToString()+": "+System.DateTime.Now.ToString());
        
        int blockCount = 0; 
        
        //used to randomize blocks, decided not to
        /*for (int t = 0; t < blockSettings.Length; t++ )
        {
            PreDefinedBlocks tmp = blockSettings[t];
            int r = Random.Range(t, blockSettings.Length);
            blockSettings[t] = blockSettings[r];
            blockSettings[r] = tmp;
        }*/
        
       /* for (int i = 0; i < whatToRun.Length; i++)
        {
            blockSettings[i] = BlockSettings.GetPreDefinedBlockSettings(whatToRun[i]);
        }
        
        blockSettings = new BlockSettings[rhoSettings.Length * lambdaSettings.Length *
                                               x_electrode_count_Settings.Length * electrode_spacing_Settings.Length];
        foreach(float rhoSetting in rhoSettings)
        {
            foreach (float lambdaSetting in lambdaSettings)
            {
                foreach(int electrodeSpacing in electrode_spacing_Settings)
                {
                    for (int i = 0; i < x_electrode_count_Settings.Length; i++)
                    {
                        blockSettings[blockCount] = new BlockSettings(rhoSetting, lambdaSetting,
                            x_electrode_count_Settings[i], y_electrode_count_Settings[i], electrodeSpacing, 0, 0, 45);
                        blockCount++; 
                    }
                }
            }
        }

        for (int t = 0; t < blockSettings.Length; t++ )
        {
            BlockSettings tmp = blockSettings[t];
            int r = Random.Range(t, blockSettings.Length);
            blockSettings[t] = blockSettings[r];
            blockSettings[r] = tmp;
        }*/ 
       
        fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile, blockSettings[currentBlock].ToString());
        VariableManagerScript.Instance.predefinedSettings = blockSettings[currentBlock]; 
        setLetterArray();
    }

    private void setLetterArray()
    {
        Random.seed = 10 * VariableManagerScript.Instance.subjectNumber;
       /* for (int i = 0; i < 26 * 3; i++)
        {
            letterArray[i] = (char)('a'+ (i / 3));
           
        }*/
       letterArray = new char[]
       {
           'c', 'd', 'e', 'f', 'l', 'o', 'p', 't', 'z',
           'c', 'd', 'e', 'f', 'l', 'o', 'p', 't', 'z',
           'c', 'd', 'e', 'f', 'l', 'o', 'p', 't', 'z',
       };
       // randomizedArray = new bool[letterArray.Length];
       // for (int i = 0; i < randomizedArray.Length / 3; i++)
       // {
       //     randomizedArray[i] = true; 
       // }
       
        for (int t = 0; t < letterArray.Length; t++ )
        {
            char tmp =letterArray[t];
            //bool tmpBool = randomizedArray[t];
            int r = Random.Range(t, letterArray.Length);
            letterArray[t] = letterArray[r];
            //randomizedArray[t] = randomizedArray[r];
            letterArray[r] = tmp;
            //randomizedArray[r] = tmpBool; 
        }

        if (debugMode)
        {
            foreach (char letter in letterArray)
            {
                Debug.Log(letter); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startRecording)
        {
            VariableManagerScript.Instance.runShaders = true; 

            if (currentTrial == letterArray.Length)
            {
                Debug.Log("Next Block");
                currentTrial = 0;
                currentBlock++;
                if (currentBlock >= blockSettings.Length)
                {
                    startRecording = false;
                }
                else
                {
                    VariableManagerScript.Instance.predefinedSettings = blockSettings[currentBlock];
                    setLetterArray();
                    fileHandler.AppendLine(LetterRecognitionExperimentHandler.Instance.subjectFile,
                        blockSettings[currentBlock].ToString());
                }
            }
            
           // VariableManagerScript.Instance.randomShuffle = randomizedArray[currentTrial];

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

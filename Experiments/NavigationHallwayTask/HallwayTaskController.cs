using System.Collections;
using System.Collections.Generic;
using System.IO;
using BionicVisionVR.Resources;
using UnityEngine;
using Valve.VR;

public class HallwayTaskController : MonoBehaviour
{
    public int vrVersion = 0; // 1 = VR Version, 0 = Monitor
    private const int NumberBlocks = 28;
    private const int NumberTrials = 6;
    private FileHandler fileHandler = new FileHandler();
    public GameObject parentTransform; 
    public Locations[,] locationList;
    public string subjectFile;
    public int currentBlock, currentTrial;
    public bool roundStarted = false;
    public int numTrials = NumberTrials, numBlocks = NumberBlocks;
    public int collisionCounter=0;
    public float roundTimer; 
    public PreDefinedSettings[] blockSettings = new PreDefinedSettings[]
    {
        PreDefinedSettings.LetterTask10,  // practice rounds are easiest settings
        PreDefinedSettings.LetterTask1, PreDefinedSettings.LetterTask2, PreDefinedSettings.LetterTask3,
        PreDefinedSettings.LetterTask4, PreDefinedSettings.LetterTask5, PreDefinedSettings.LetterTask6, 
        PreDefinedSettings.LetterTask7, PreDefinedSettings.LetterTask8, PreDefinedSettings.LetterTask9,
        PreDefinedSettings.LetterTask10, PreDefinedSettings.LetterTask11,PreDefinedSettings.LetterTask12,
        PreDefinedSettings.LetterTask13, PreDefinedSettings.LetterTask14, PreDefinedSettings.LetterTask15,
        PreDefinedSettings.LetterTask16, PreDefinedSettings.LetterTask17, PreDefinedSettings.LetterTask18,
        PreDefinedSettings.LetterTask19, PreDefinedSettings.LetterTask20, PreDefinedSettings.LetterTask21,
        PreDefinedSettings.LetterTask22, PreDefinedSettings.LetterTask23, PreDefinedSettings.LetterTask24,
        PreDefinedSettings.LetterTask25, PreDefinedSettings.LetterTask26, PreDefinedSettings.LetterTask27
    };
    
    public struct Locations{
        public int depth1, depth2, depth3;
        public bool leftSide1, leftSide2, leftSide3;
    };
    
    private IEnumerator MoveCamera(Transform camera, Vector3 translation)
    {
        camera.position += translation;
        
        yield return null; 
    }
    
    public static HallwayTaskController Instance { get; private set; }

    public void ExperimentStartup(){
        subjectFile = Application.dataPath + Path.DirectorySeparatorChar +"NavigationHallwayTask" + Path.DirectorySeparatorChar
                      + "Experiment_Data" + Path.DirectorySeparatorChar +
                      VariableManagerScript.Instance.subjectNumber + "_" +
                      "HallwayTask.csv";
        System.IO.Directory.CreateDirectory(Application.dataPath + Path.DirectorySeparatorChar +
                                            "NavigationHallwayTask"+ Path.DirectorySeparatorChar +
                                            "Experiment_Data" + Path.DirectorySeparatorChar); 
        System.IO.Directory.CreateDirectory(VariableManagerScript.Instance.configurationPath); 
        
        fileHandler.AppendLine(subjectFile, "Start time: "+System.DateTime.Now.ToString());
        
        Random.InitState(VariableManagerScript.Instance.subjectNumber * 10 + vrVersion);
        locationList = new Locations[NumberBlocks, NumberTrials];
        
        // Sets all locations for each block/trial combo
        for (int i = 0; i < NumberBlocks; i++)
        {
            for (int j = 0; j < NumberTrials; j++)
            {
                int firstLocation = Random.Range(1, 4);
                int secondLocation = Random.Range(1, 4);
                int thirdLocation = Random.Range(1, 4);
                while (firstLocation == secondLocation) secondLocation = Random.Range(1, 4);

                Debug.Log(firstLocation + "," + secondLocation);

                locationList[i, j].depth1 = firstLocation;
                locationList[i, j].leftSide1 = Random.Range(0, 2) == 1 ? true : false;

                locationList[i, j].depth2 = secondLocation;
                locationList[i, j].leftSide2 = Random.Range(0, 2) == 1 ? true : false;

                if (j < NumberTrials / 2) locationList[i, j].depth3 = 0;
                else
                {
                    while (thirdLocation == firstLocation || thirdLocation == secondLocation)
                    {
                        thirdLocation = Random.Range(1, 4);
                    }

                    locationList[i, j].depth3 = thirdLocation;
                    locationList[i, j].leftSide3 = Random.Range(0, 2) == 1 ? true : false;
                }
            }
        }
        
        // Randomizes the order of blocks, first block always easy
        for (int t = 0; t < blockSettings.Length; t++) 
        {
            if (t > 0)
            {
                PreDefinedSettings tmp = blockSettings[t];

                int r = Random.Range(t, blockSettings.Length);
                blockSettings[t] = blockSettings[r];
                blockSettings[r] = tmp;
            }

            for (int u = 0; u < numTrials; u++) //randomizes order of trials with 2/3 obstacles
            {
                Locations temp = locationList[t, u];
                int swapIndex = Random.Range(u, numTrials);
                locationList[t, u] = locationList[t, swapIndex];
                locationList[t, swapIndex] = temp;
            }
        }
        
        Debug.Log(blockSettings[0]);
    }

    // Start is called before the first frame update
    void Start()
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

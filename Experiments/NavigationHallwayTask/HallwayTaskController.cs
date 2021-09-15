using System.Collections;
using System.Collections.Generic;
using System.IO;
using BionicVisionVR.Resources;
using UnityEngine;
using Valve.VR;


public class HallwayTaskController : MonoBehaviour
{
    private const int NumberBlocks = 28;
    private const int NumberTrials = 6;
    private FileHandler fileHandler = new FileHandler();
    
    public int[] blockOrder;
    public GameObject parentTransform; 
    public Locations[,] locationList;
    public string subjectFile;
    public int currentBlock, currentTrial;
    public bool roundStarted = false;
    public int numTrials = NumberTrials, numBlocks = NumberBlocks;
    public int collisionCounter=0;
    public float roundTimer; 
    public PreDefinedBlocks[] blockSettings = new PreDefinedBlocks[]
    {
        PreDefinedBlocks.LetterTask19,  // practice rounds are easiest settings
        PreDefinedBlocks.LetterTask1, PreDefinedBlocks.LetterTask2, PreDefinedBlocks.LetterTask3,
        PreDefinedBlocks.LetterTask4, PreDefinedBlocks.LetterTask5, PreDefinedBlocks.LetterTask6, 
        PreDefinedBlocks.LetterTask7, PreDefinedBlocks.LetterTask8, PreDefinedBlocks.LetterTask9,
        PreDefinedBlocks.LetterTask10, PreDefinedBlocks.LetterTask11,PreDefinedBlocks.LetterTask12,
        PreDefinedBlocks.LetterTask13, PreDefinedBlocks.LetterTask14, PreDefinedBlocks.LetterTask15,
        PreDefinedBlocks.LetterTask16, PreDefinedBlocks.LetterTask17, PreDefinedBlocks.LetterTask18,
        PreDefinedBlocks.LetterTask19, PreDefinedBlocks.LetterTask20, PreDefinedBlocks.LetterTask21,
        PreDefinedBlocks.LetterTask22, PreDefinedBlocks.LetterTask23, PreDefinedBlocks.LetterTask24,
        PreDefinedBlocks.LetterTask25, PreDefinedBlocks.LetterTask26, PreDefinedBlocks.LetterTask27
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
        for (int g = 0; g <= 20; g++)
        {
            for (int i = 0; i < NumberBlocks; i++)
            {
                Debug.Log("Subject: " + i);

                locationList = new Locations[NumberBlocks, NumberTrials];
                blockOrder = new int[NumberBlocks];

                Random.seed = (g * 10);
                // Sets all locations for each block/trail combo
                blockOrder[i] = i;
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

            for (int t = 0; t < blockOrder.Length; t++) // Randomizes the order of blocks, first block always easy
            {
                if (t > 0)
                {
                    int tmp = blockOrder[t];

                    int r = Random.Range(t, blockOrder.Length);
                    blockOrder[t] = blockOrder[r];
                    blockOrder[r] = tmp;
                }

                for (int u = 0; u < numTrials; u++)
                {
                    Locations temp = locationList[t, u];
                    int swapIndex = Random.Range(u, numTrials);
                    locationList[t, u] = locationList[t, swapIndex];
                    locationList[t, swapIndex] = temp;
                }
            }

            for (int i = 1; i <= 20; i++)
            {
                fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_blockMap.csv"), blockSettings[i].ToString());
            }
        }
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

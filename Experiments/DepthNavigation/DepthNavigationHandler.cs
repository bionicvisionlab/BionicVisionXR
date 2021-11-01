using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DepthNavigationHandler : MonoBehaviour
{

    public RawImage navigationArrow;
    public GameObject faceFinish;
    public GameObject playerCamera; 
    public GameObject finish;
    public AudioSource collisionSound;
    public RawImage spaceToContinue;
    public RawImage finishScreen; 

    private string[] fileList = {"collisions"};

    private PreDefinedBlocks[] blocks =
        {PreDefinedBlocks.ArgusMotion1, PreDefinedBlocks.ArgusMotion2, PreDefinedBlocks.ArgusMotion2}; 
    
    private int[] numCollisions = new int[3];
    private bool depthFirst;
    private bool roundComplete;
    private float lastCollision;
    private float roundStart;
    private int collisionCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        ExperimentHandler.Instance.ExperimentStartup("DepthNavigation", fileList, blocks, false,3);
        Random.InitState(VariableManagerScript.Instance.subjectNumber);
        depthFirst = Random.Range(0f, 1f) > .5 ? true : false;
        spaceToContinue.enabled = true; 
    }
    
    //Track collisions
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name); 
        if (other.gameObject.name.Equals("Finish"))
        {
            Debug.Log("Finished!");
            ExperimentHandler.Instance.WriteToAllTaggedFiles("Round " + ExperimentHandler.Instance.currentTrial +
                                                             "finished: " + (Time.time - roundStart) + ", " +
                                                             collisionCounter);
            collisionCounter = 0; 
            
            ExperimentHandler.Instance.currentTrial++;

            if ((ExperimentHandler.Instance.currentTrial == 0 && depthFirst) ||
                (ExperimentHandler.Instance.currentTrial == 1 && !depthFirst))
            {
                VariableManagerScript.Instance.depthDetection = true;
                ExperimentHandler.Instance.WriteToAllTaggedFiles("Depth Mode"); 
            }

            if (ExperimentHandler.Instance.currentTrial == 0)
                SceneManager.LoadScene("DN1"); 
            else if (ExperimentHandler.Instance.currentTrial == 1)
                SceneManager.LoadScene("DN2");
            else
                finishScreen.enabled = true; 
        }
        if (Time.time - lastCollision > 3)
        {
            Debug.Log("COLLISION");
            ExperimentHandler.Instance.WriteToTaggedFile("collisions", "Collision: " + (Time.time - roundStart)); 
            collisionCounter++;
            collisionSound.Play();
            lastCollision = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        faceFinish.transform.LookAt(finish.transform); 
        Vector3 rotation = new Vector3(0f, 0f, faceFinish.transform.eulerAngles.y-playerCamera.transform.eulerAngles.y);
        navigationArrow.transform.eulerAngles = rotation;

        if (spaceToContinue.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceToContinue.enabled = false;
                roundStart = Time.time; 
            }

            if (spaceToContinue.enabled)
                gameObject.GetComponent<FirstPersonAIO>().enabled = false;
            else
                gameObject.GetComponent<FirstPersonAIO>().enabled = true; 
        }
    }
}

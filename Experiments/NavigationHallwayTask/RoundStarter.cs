using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BionicVisionVR.Resources;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Valve.VR;

public class RoundStarter : MonoBehaviour
{
    public GameObject person1, person2, person3;
    private FileHandler fileHandler = new FileHandler(); 
    private IEnumerator MoveCamera(Transform camera, Vector3 translation)
    {
        camera.position += translation;
        
        yield return null; 
    }
    
    public void RoundStart()
    {
        fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_collisions.csv"),  HallwayTaskController.Instance.currentBlock+","+HallwayTaskController.Instance.currentTrial );
        int zInversion = HallwayTaskController.Instance.currentTrial % 2 == 1 ? -1 : 1;

        Transform steamVRCamera = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;
        Vector3 groundPosition = new Vector3(headPosition.x, steamVRCamera.position.y, headPosition.z);
        Vector3 destination = new Vector3(headPosition.x, 0, zInversion*2.8f);
        Vector3 translateVector = destination - groundPosition;
        StartCoroutine(MoveCamera(steamVRCamera, translateVector)); 
        
        if (HallwayTaskController.Instance.currentBlock > 0)
            VariableManagerScript.Instance.runShaders = true; 
        
        Debug.Log("NEW ROUND"); 
        Debug.Log(Time.time - HallwayTaskController.Instance.roundTimer);
        Debug.Log(HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth1);
        Debug.Log(HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth2);
        Debug.Log(HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth3);
        
        switch (HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth1)
        {
            case(0):
                person1.transform.position = new Vector3(-100, -100, -100);
                break; 
            // case(1):
            //     person1.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide1 ? new Vector3(-.4572f, 0, .524f) : new Vector3(.4572f, 0, .524f);
            //     break;
            case(1):
                person1.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide1 ? new Vector3(-.4572f, 0, zInversion * 3.9624f) : new Vector3(.4572f, 0, zInversion * 3.9624f);
                break;
            case(2):
                person1.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide1 ? new Vector3(-.4572f, 0, zInversion * 6.4008f) : new Vector3(.4572f, 0, zInversion * 6.4008f);
                break;
            case(3):
                person1.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide1 ? new Vector3(-.4572f, 0, zInversion * 8.8392f) : new Vector3(.4572f, 0, zInversion * 8.8392f);
                break;
        }
        switch (HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth2)
        {
            case(0):
                person2.transform.position = new Vector3(-100, -100, -100);
                break; 
            // case(1):
            //     person2.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide2 ? new Vector3(-.4572f, 0, .524f) : new Vector3(.4572f, 0, .524f);
            //     break;
            case(1):
                person2.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide2 ? new Vector3(-.4572f, 0, zInversion * 3.9624f) : new Vector3(.4572f, 0, zInversion * 3.9624f);
                break;
            case(2):
                person2.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide2 ? new Vector3(-.4572f, 0, zInversion * 6.4008f) : new Vector3(.4572f, 0, zInversion * 6.4008f);
                break;
            case(3):
                person2.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide2 ? new Vector3(-.4572f, 0,zInversion * 8.8392f) : new Vector3(.4572f, 0, zInversion * 8.8392f);
                break;
        }
        switch (HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].depth3)
        {
            case(0):
                person3.transform.position = new Vector3(-100, -100, -100);
                break; 
            // case(1):
            //     person3.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide3 ? new Vector3(-.4572f, 0, .524f) : new Vector3(.4572f,0, .524f);
            //     break;
            case(1):
                person3.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide3 ? new Vector3(-.4572f, 0, zInversion * 3.9624f) : new Vector3(.4572f, 0, zInversion * 3.9624f);
                break;
            case(2):
                person3.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide3 ? new Vector3(-.4572f, 0, zInversion * 6.4008f) : new Vector3(.4572f, 0, zInversion * 6.4008f);
                break;
            case(3):
                person3.transform.position = HallwayTaskController.Instance.locationList[HallwayTaskController.Instance.currentBlock, HallwayTaskController.Instance.currentTrial].leftSide3 ? new Vector3(-.4572f, 0, zInversion * 8.8392f) : new Vector3(.4572f, 0, zInversion * 8.8392f);
                break;
        }
 
    }
        
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!HallwayTaskController.Instance.roundStarted)
        {
            RoundStart();
            HallwayTaskController.Instance.roundStarted = true;
        }
    }


}


 

using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Resources;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CameraResetVR : MonoBehaviour
{
    public RawImage finishScreen, nextRound;
    public GameObject parentTransform; 
    private FileHandler fileHandler = new FileHandler();
    private Transform steamVRCamera;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time - HallwayTaskController.Instance.roundTimer < .5)
            nextRound.enabled = true;
        else
            nextRound.enabled = false; 
        
        if (Time.time - HallwayTaskController.Instance.roundTimer > 60.5)
        {
            VariableManagerScript.Instance.runShaders = false; 
        }
        
        if ((this.transform.position.z > 9.1 && (HallwayTaskController.Instance.currentTrial%2==0 || HallwayTaskController.Instance.vrVersion==0)) || (this.transform.position.z < -9.1 && HallwayTaskController.Instance.currentTrial%2==1))
        {
            HallwayTaskController.Instance.roundStarted = false;
            if(HallwayTaskController.Instance.vrVersion==1)
                parentTransform.transform.Rotate(new Vector3(0f, 180.0f, 0f));
            else
            {
                this.transform.position = new Vector3(0, 0, -1.5f); 
                this.transform.eulerAngles = new Vector3(0, 180, 0); 
            }
            
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile, HallwayTaskController.Instance.blockSettings[HallwayTaskController.Instance.currentBlock] + ", " + (HallwayTaskController.Instance.currentTrial+1) + ": " + (Time.time - HallwayTaskController.Instance.roundTimer -.5) + ", " + HallwayTaskController.Instance.collisionCounter );
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head.csv"), HallwayTaskController.Instance.blockSettings[HallwayTaskController.Instance.currentBlock] + ", " + (HallwayTaskController.Instance.currentTrial+1) );
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head-position.csv"), HallwayTaskController.Instance.blockSettings[HallwayTaskController.Instance.currentBlock] + ", " + (HallwayTaskController.Instance.currentTrial +1) );
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head-position-easy-paste.csv"),  HallwayTaskController.Instance.blockSettings[HallwayTaskController.Instance.currentBlock] + ", " + (HallwayTaskController.Instance.currentTrial+1) );
            HallwayTaskController.Instance.currentTrial++;
            HallwayTaskController.Instance.collisionCounter = 0;
            if (HallwayTaskController.Instance.currentTrial > 2 && HallwayTaskController.Instance.currentBlock == 0)
            {
                VariableManagerScript.Instance.predefinedSettings = HallwayTaskController.Instance.blockSettings[0]; 
                VariableManagerScript.Instance.runShaders = true; 
            }
            
            if (HallwayTaskController.Instance.currentTrial == HallwayTaskController.Instance.numTrials)
            {
                if (HallwayTaskController.Instance.currentBlock == HallwayTaskController.Instance.numBlocks - 1)
                    finishScreen.enabled = true;
                else
                {
                    HallwayTaskController.Instance.currentBlock++;
                    HallwayTaskController.Instance.currentTrial = 0;
                    VariableManagerScript.Instance.predefinedSettings =
                        HallwayTaskController.Instance.blockSettings[HallwayTaskController.Instance.currentBlock];
                }

            }
                
            Debug.Log("Current block: "+ (HallwayTaskController.Instance.currentBlock+1));
            Debug.Log("Current trial: "+ (HallwayTaskController.Instance.currentTrial+1));
            HallwayTaskController.Instance.roundTimer = Time.time; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Resources;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    private FileHandler fileHandler = new FileHandler(); 
    private float recordHeadTimer = 0.0f;
    public float timeInterval = .5f; //how often to record head position
    public Camera mainCamera; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - recordHeadTimer > timeInterval)
        {
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head.csv"), (Time.time - HallwayTaskController.Instance.roundTimer) + mainCamera.transform.rotation.eulerAngles.ToString() );
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head-position.csv"), (Time.time - HallwayTaskController.Instance.roundTimer) + mainCamera.transform.position.ToString() );
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_head-position-easy-paste.csv"),  mainCamera.transform.position.ToString() );
            recordHeadTimer = Time.time; 
        }
    }
}

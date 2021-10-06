using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using BionicVisionVR.Resources;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    private FileHandler fileHandler = new FileHandler(); 
    private float recordHeadTimer = 0.0f;
    public float timeInterval = .5f; //how often to record head position

    // Update is called once per frame
    void Update()
    {
        if (ExperimentHandler.Instance.cameraTracking && Time.time - recordHeadTimer > timeInterval)
        {
            fileHandler.AppendLine(ExperimentHandler.Instance.subjectFile.Replace(".csv","_head.csv"), (Time.time - ExperimentHandler.Instance.trialTimer) + gameObject.transform.rotation.eulerAngles.ToString() );
            fileHandler.AppendLine(ExperimentHandler.Instance.subjectFile.Replace(".csv","_head-position.csv"), (Time.time - ExperimentHandler.Instance.trialTimer) + gameObject.transform.position.ToString() );
            fileHandler.AppendLine(ExperimentHandler.Instance.subjectFile.Replace(".csv","_head-position-easy-paste.csv"),  gameObject.transform.position.ToString() );
            recordHeadTimer = Time.time; 
        }
    }
}

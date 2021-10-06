using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using Experiments.SampleExperiment;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
            Debug.Log("COLLISION");
            SampleExperimentHandler handler = GetComponentInParent<SampleExperimentHandler>();

            if (ExperimentHandler.Instance.currentBlock == 0)
            {
                handler.shouldWriteToFile1 = true;  // tells the experiment handler this trial is complete
                transform.gameObject.SetActive(false); // deactivates the cube
            }

            else
            {
                handler.shouldWriteToFile2 = true; 
                transform.gameObject.SetActive(false); 
            }

    }
}

using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using UnityEngine;
using Valve.VR;

public class ActivateWithHMD : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        SteamVR_CameraHelper cameraHelper = gameObject.GetComponent<SteamVR_CameraHelper>();
        if (ExperimentHandler.Instance.useHMD)
        {
            cameraHelper.enabled = true; 
        }
        else
        {
            cameraHelper.enabled = false; 
        }
    }
}

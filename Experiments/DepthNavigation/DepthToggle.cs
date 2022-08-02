using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using UnityEngine;

public class DepthToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!VariableManagerScript.Instance.depthDetection)
        {
            gameObject.GetComponent<Camera>().enabled = true; 
            gameObject.GetComponent<CameraRaycast>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

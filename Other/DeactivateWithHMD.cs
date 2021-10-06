using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using UnityEngine;
using Valve.VR;

public class DeactivateWithHMD : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        FirstPersonAIO firstPerson = gameObject.GetComponent<FirstPersonAIO>();
        if (ExperimentHandler.Instance.useHMD)
        {
            firstPerson.enabled = false; 
        }
        else
        {
            firstPerson.enabled = true; 
        }
    }
}

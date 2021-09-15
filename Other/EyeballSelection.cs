using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (VariableManagerScript.Instance.useLeftEye)
        {
            Camera.main.stereoTargetEye = StereoTargetEyeMask.Left; 
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

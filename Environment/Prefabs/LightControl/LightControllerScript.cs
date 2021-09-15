using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightControllerScript : MonoBehaviour
{
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LetterRecognitionExperimentHandler.Instance.lightsOff || Input.GetKeyDown("1"))
        {
            light1.range = 1;
            light2.range = 1;
            light3.range = 1;
            light4.range = 1; 
        }
        
        if (Input.GetKeyDown("2"))
        {
            light1.range = 50;
            light2.range = 50;
            light3.range = 50;
            light4.range = 50; 
        }
        
    }
}

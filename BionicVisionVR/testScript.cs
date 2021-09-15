using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    private BinaryHandler binaryHandler = new BinaryHandler();

    // Start is called before the first frame update
    void Start()
    {
        float[] makeArray = new float[] {3.0f, 2.0f, 1.0f};
        binaryHandler.WriteFloatArray("testArrayBinary", makeArray); 
        
        float[] testArray =  binaryHandler.ReadFloatsFromBinaryFile("testArrayBinary").ToArray();
        
        foreach (float value in testArray)
        {
            Debug.Log(value); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

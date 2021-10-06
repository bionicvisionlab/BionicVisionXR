using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class rollforward_with_headbob : MonoBehaviour
{
    public float speed = 5.0f;

    public bool increaseSpeed = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        Random.InitState(10);  
        // Move the object forward along its z axis 1 unit/seconds
        foreach (int value in Enumerable.Range(1, (int) speed))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }

        if(increaseSpeed && Time.time<6.5)
            speed += .3f;
        
    }
}

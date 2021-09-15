using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelroll : MonoBehaviour
{
    private float speed = 3.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(speed, 0,0, Space.Self);
        speed += .1f; 
    }
}

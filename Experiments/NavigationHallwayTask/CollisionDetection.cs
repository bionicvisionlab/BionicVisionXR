using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Resources;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private FileHandler fileHandler = new FileHandler();
    public Camera mainCamera; 

    public AudioSource collisionSound;
    private float lastCollision = 0f; 
    
    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - HallwayTaskController.Instance.roundTimer < 61 && Time.time - lastCollision > 1)
        {
            Debug.Log("COLLISION");
            fileHandler.AppendLine(HallwayTaskController.Instance.subjectFile.Replace(".csv","_collisions.csv"), (Time.time - HallwayTaskController.Instance.roundTimer) + mainCamera.transform.position.ToString() );
            HallwayTaskController.Instance.collisionCounter++;
            collisionSound.Play();
            lastCollision = Time.time; 
            
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

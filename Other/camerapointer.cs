using UnityEngine;
// This complete script can be attached to a camera to make it
// continuously point at another object.

public class camerapointer : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    public float speed = 1e-10f;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        Debug.Log(Time.time);
        if(Time.time < 7)
            transform.LookAt(target);
        else
        {
            var targetRotation = Quaternion.LookRotation(target2.transform.position - transform.position);
       
        // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            speed += .1f; 
        }
       
        

    }
}
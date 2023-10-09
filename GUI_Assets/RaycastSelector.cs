using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

namespace BionicVisionVR.GUI_Assets
{
    /// <summary>
    /// Draws a line towards the first object hit within a certain distance of the object this is attached to
    /// TODO where to attach
    /// Contains:
    ///     void UpdateLine()
    ///     RaycastHit CreateRaycast(float length)
    /// On Awake:
    ///     Sets lineRenderer to this GameObject's LineRenderer component
    /// On Start:
    ///     N/A
    /// On Update:
    ///     Calls UpdateLine()
    /// </summary>
    public class RaycastSelector : MonoBehaviour
    {
        public float defaultLength = 15.0f;
        public GameObject dot;
        public VRInputModule inputModule; 
        
        private LineRenderer lineRenderer = null;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            UpdateLine(); 
        }
        /// <summary>
        /// Raycasts in forward direction from object and draws a line from transform.position to the hit position
        /// </summary>
        private void UpdateLine()
        {
            float targetLength = defaultLength;
            RaycastHit hit = CreateRaycast(targetLength);

            Vector3 endPosition = transform.position + (transform.forward * targetLength);

            if (hit.collider != null && !hit.collider.gameObject.name.Equals("EndDot"))
            {
                endPosition = hit.point;
                //Debug.Log(hit.collider.gameObject.name);
            }

            dot.transform.position = endPosition;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition); 
        }
        /// <summary>
        /// Creates a raycast of given length in the forward direction from transform.position
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private RaycastHit CreateRaycast(float length)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, defaultLength);

            return hit; 
        }



    }
}
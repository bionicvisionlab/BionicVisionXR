using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

namespace BionicVisionVR.GUI_Assets
{
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

        private RaycastHit CreateRaycast(float length)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, defaultLength);

            return hit; 
        }



    }
}
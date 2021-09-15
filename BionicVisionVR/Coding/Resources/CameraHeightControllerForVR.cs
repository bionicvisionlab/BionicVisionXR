using UnityEngine;
using UnityEngine.XR;
using InputTracking = UnityEngine.XR.InputTracking;

namespace BionicVisionVR.Resources
{
    public class CameraHeightControllerForVR : MonoBehaviour
    {
        [SerializeField] private float defaultHeight = 1.295508f;
        [SerializeField] private Camera camera;
        [SerializeField]private GameObject cameraObject;

        private Vector3 cameraPosition;

        private void Resize()
        {
            
        }

        void Start()
        {
            
        }

        private bool firstRender = true;
        private float heightAdjust = 0; 
        void Update()
        {
            if (firstRender)
            {
                cameraPosition = camera.transform.position; 
                heightAdjust = defaultHeight - cameraPosition.y; 
                cameraObject.transform.Translate(0, heightAdjust, 0);
                firstRender = false;
            }
            
           

        }
    }
}
using UnityEngine;
using UnityEngine.XR;
using InputTracking = UnityEngine.XR.InputTracking;

namespace BionicVisionVR.Resources
{
    /// <summary>
    /// Sets up the camera height and position on the first render frame
    /// TODO where to attach
    /// On Awake:
    ///     N/A
    /// On Start:
    ///     N/A
    /// On Update:
    ///     Sets up the camera height and position on the first render frame
    /// </summary>
    public class CameraHeightControllerForVR : MonoBehaviour
    {
        [SerializeField] private float defaultHeight = 1.295508f;
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
                cameraPosition = GetComponent<Camera>().transform.position; 
                heightAdjust = defaultHeight - cameraPosition.y; 
                cameraObject.transform.Translate(0, heightAdjust, 0);
                firstRender = false;
            }
            
           

        }
    }
}
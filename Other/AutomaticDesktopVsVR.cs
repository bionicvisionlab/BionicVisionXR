using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

/// <summary>
/// Attach this to your camera to automatically switch between
/// keyboard movement and headset
/// </summary>
public class AutomaticDesktopVsVR : MonoBehaviour
{
    void Start()
    {
        SimpleFirstPersonMovement firstPerson = gameObject.GetComponent<SimpleFirstPersonMovement>();
        SteamVR_CameraHelper cameraHelper = gameObject.GetComponent<SteamVR_CameraHelper>();
        
        cameraHelper.enabled = XRSettings.enabled;
        firstPerson.enabled = !XRSettings.enabled;
    }
}

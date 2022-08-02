using UnityEngine;

public class EyeballSelection : MonoBehaviour
{
    void Update() {
        Camera mainCam = gameObject.GetComponentInChildren<Camera>();
        mainCam.stereoTargetEye = VariableManagerScript.Instance.useLeftEye ? StereoTargetEyeMask.Left : StereoTargetEyeMask.Right; } 
}

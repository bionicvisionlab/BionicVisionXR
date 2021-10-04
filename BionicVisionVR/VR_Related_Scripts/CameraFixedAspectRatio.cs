// CameraLetterBox.cs
using UnityEngine;
 
public class CameraFixedAspectRatio : MonoBehaviour
{
    private Camera mainCam = new Camera();

    public float targetAspectRatio = 1f;
 
    private void Start()
    {
        mainCam = BackendShaderHandler.Instance.mainCamera;
    }
    
    private void Update()
    {
        float w = Screen.width;
        float h = Screen.height;
        float a = w / h;
        Rect r;
        if (a > targetAspectRatio)
        {
            float tw = h * targetAspectRatio;
            float o = (w - tw) * 0.5f;
            r = new Rect(o,0,tw,h);
        }
        else
        {
            float th = w / targetAspectRatio;
            float o = (h - th) * 0.5f;
            r = new Rect(0, o, w, th);
        }
        mainCam.pixelRect = r;

    }
}
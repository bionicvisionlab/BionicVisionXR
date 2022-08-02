using System;
using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using Tobii.XR;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private bool recordCamera = false, recordGaze=false;

    public static CameraTracker Instance; 
    private float recordHeadTimer = 0.0f;
    public float timeInterval = .2f; //how often to record head position

    // public Material pointShader;
    private Camera vrCamera;
    public float smoothMove = 80;
    private Vector3 _lastGazeDirection;

    public void StartRecording(bool recordGaze)
    {
        this.recordGaze = recordGaze;
        recordCamera = true; 
    }

    public void PauseRecording()
    {
        recordGaze = false;
        recordCamera = false; 
    }
    
    private void Start()
    {
        vrCamera = gameObject.GetComponent<Camera>();
        
    }

    private void Update()
    {
        if (Time.time - recordHeadTimer > timeInterval & recordCamera)
        {
            if (recordGaze)
            {
                var provider = TobiiXR.Internal.Provider;
                var eyeTrackingData = new TobiiXR_EyeTrackingData();
                provider.GetEyeTrackingDataLocal(eyeTrackingData);
                var smoothMoveSpeed = true;

                var interpolatedGazeDirection = Vector3.Lerp(_lastGazeDirection, eyeTrackingData.GazeRay.Direction,
                    (smoothMove) * Time.unscaledDeltaTime);
                var usedDirection = smoothMoveSpeed
                    ? interpolatedGazeDirection.normalized
                    : eyeTrackingData.GazeRay.Direction.normalized;
                _lastGazeDirection = usedDirection;

                var screenPos =
                    vrCamera.WorldToScreenPoint(vrCamera.transform.position +
                                                vrCamera.transform.rotation * usedDirection);

                float gazeX = screenPos.x / BackendShaderHandler.Instance.xResolution;
                float gazeY = screenPos.y / BackendShaderHandler.Instance.yResolution;

                TaskHandler.Instance.WriteToTaggedFile("_camera_tracker",
                    gameObject.transform.rotation.eulerAngles.x.ToString() + "," +
                    gameObject.transform.rotation.eulerAngles.y.ToString() + "," + 
                    gameObject.transform.rotation.eulerAngles.z.ToString() + "," +
                    gameObject.transform.position.x.ToString() + "," +
                    gameObject.transform.position.y.ToString() + "," + 
                    gameObject.transform.position.z.ToString() + "," +
                    gazeX + "," + gazeY + "," + usedDirection.x + "," + usedDirection.y + "," + usedDirection.z, false);
                recordHeadTimer = Time.time;
            }
            else
            {
                TaskHandler.Instance.WriteToTaggedFile("_camera_tracker",
                    gameObject.transform.rotation.eulerAngles.x.ToString() + "," +
                    gameObject.transform.rotation.eulerAngles.y.ToString() + "," + 
                    gameObject.transform.rotation.eulerAngles.z.ToString() +
                    gameObject.transform.position.x.ToString() + "," +
                    gameObject.transform.position.y.ToString() + "," + 
                    gameObject.transform.position.z.ToString() , false);
                recordHeadTimer = Time.time;
            }

        }
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

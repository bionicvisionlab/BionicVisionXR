using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR;
using UnityEngine;

public class RunShift : MonoBehaviour
{
    [SerializeField] private Material shiftShaderMaterial;
    [SerializeField] private bool _smoothMove = true;
    [SerializeField] [Range(1, 30)] private int _smoothMoveSpeed = 7;
    private Vector3 _lastGazeDirection;
    
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var provider = TobiiXR.Internal.Provider;
        var eyeTrackingData = new TobiiXR_EyeTrackingData();
        provider.GetEyeTrackingDataLocal(eyeTrackingData);

        var interpolatedGazeDirection = Vector3.Lerp(_lastGazeDirection, eyeTrackingData.GazeRay.Direction, 
            _smoothMoveSpeed * Time.unscaledDeltaTime);
        var usedDirection = _smoothMove ? interpolatedGazeDirection.normalized : eyeTrackingData.GazeRay.Direction.normalized;
        _lastGazeDirection = usedDirection; 
        
        float aspectRatio = (float) src.height /  src.width;
        float convertToUnitSphere = (float) Math.Sqrt(1.0f / eyeTrackingData.GazeRay.Direction.z);
        
        shiftShaderMaterial.SetFloat("gazeY", (usedDirection.y * convertToUnitSphere) +0.5f);
        shiftShaderMaterial.SetFloat("gazeX", (usedDirection.x * convertToUnitSphere * aspectRatio) +  0.5f);
        shiftShaderMaterial.SetFloat("aspectRatio", aspectRatio); 
        
        RenderTexture temp = src; 
        Graphics.Blit(src, dest, shiftShaderMaterial);
        //Graphics.Blit(temp, dest); 
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Container class for adding shaders to the camera's output
/// Attach to a camera
/// Contains:
///     OnRenderImage(RenderTexture src, RenderTexture dest)
/// On Awake:
///     N/A
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class RunShader : MonoBehaviour
{
    public bool debugDisplay = false; 
    public float tau_ca = 0.2f; // Charge Dissipation Rate (slow decay)
    public float tau_bp = 0.15f;  // Brightness Fading Rate (fast decay)
    public float ca_scale = 0.25f;
    public float amplitude = 0.5f; 
    
    public Material lumShader;
    public Material temporalShader;

    public struct TemporalVariables
    {
        public float ca ;
        public float bp ;

        public TemporalVariables(float ca, float bp)
        {
            this.ca = ca;
            this.bp = bp;
        }
    }

    private bool firstFrame = true;
    private TemporalVariables[] temporalVariableArray; 
    private ComputeBuffer temporalVariableBuffer;
    private int frameCount = 0;
    private int debugFrequency = 10; 
    /// <summary>
    /// Called on every frame of the camera
    /// Blits lumShader and then temporalShader onto src and sends it to dest
    /// </summary>
    /// <param name="src">A RenderTexture containing the source image</param>
    /// <param name="dest">The RenderTexture to update with the modified image</param>
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (firstFrame)
        {
            temporalVariableArray = new TemporalVariables[src.width*src.height];
            for (int i = 0; i < src.width*src.height; i++)
                temporalVariableArray[i] = new TemporalVariables(0f, 0f);
            temporalVariableBuffer = new ComputeBuffer(src.width * src.height,
                System.Runtime.InteropServices.Marshal.SizeOf(typeof(TemporalVariables)), ComputeBufferType.Default);
            Graphics.SetRandomWriteTarget(1, temporalVariableBuffer, true);
            temporalVariableBuffer.SetData(temporalVariableArray);

            temporalShader.SetBuffer("tempVariables", temporalVariableBuffer);
            temporalShader.SetFloat("xResolution", src.width);
            temporalShader.SetFloat("yResolution", src.height);
            temporalShader.SetFloat("tau_ca", tau_ca);
            temporalShader.SetFloat("tau_bp", tau_bp); 
            temporalShader.SetFloat("ca_scale", ca_scale); 
            temporalShader.SetFloat("amplitude", amplitude);
            firstFrame = false; 
        }

        temporalShader.SetFloat("dt", Time.deltaTime);
        
        RenderTexture tmp = RenderTexture.GetTemporary(src.width, src.height, 0); 
        Graphics.Blit(src, tmp, lumShader);
        Graphics.Blit(tmp, dest, temporalShader);

        if (frameCount % debugFrequency == 0 & debugDisplay)
        {
            temporalVariableBuffer.GetData(temporalVariableArray);
            Debug.Log("CA: " + temporalVariableArray[0].ca);
            Debug.Log("BP: " + temporalVariableArray[0].bp);
        }

        frameCount++; 
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

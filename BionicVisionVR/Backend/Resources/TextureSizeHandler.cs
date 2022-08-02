using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// Currently does nothing
/// Attach to camera
/// Contains: 
///     void OnRenderImage(RenderTexture source, RenderTexture destination)
/// On Awake:
///     N/A
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class TextureSizeHandler : MonoBehaviour
{

    /// <summary>
    /// Currently does nothing
    /// </summary>
    /// <param name="source">A RenderTexture containing the source image</param>
    /// <param name="destination">The RenderTexture to update with the modified image</param>
    /// <returns></returns>
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RawImage[] textures= gameObject.GetComponentsInChildren<RawImage>();
        for (int i=0; i<textures.Length; i++)
        {
            // var newText = new Texture2D(source.width, source.height);
            // newText. = (Texture2D) textures[i].texture; 
            // textures[i].texture = newText; 
            //
        }
    }
}

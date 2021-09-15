using System;
using UnityEngine;

/// <summary>
/// Degrees and microns are listed with negative/positive values assuming (0,0) is at the center of the screen
/// Screen position is solely positive and spans 0:1 (with .5 being at the center of the screen)
///
/// When converting from screen position to other coordinates (or vise versa), the y-axis needs inverted.
/// In screen position, y=0 is the top of the screen and y=1 is the bottom
/// 
/// </summary>
public class UnitConverter
{

    public int flatten2DElectrodePosition(int row, int col)
    {
        return row + VariableManagerScript.Instance.numberYelectrodes * col;
    }
    
    public int flatten2DPixel(int x, int y)
    {
        return x + BackendShaderHandler.Instance.xResolution * y; 
    }
    
    public int[] unflatten2DPixel(int flattened)
    {
        int x = flattened / BackendShaderHandler.Instance.xResolution;
        int y = flattened % BackendShaderHandler.Instance.xResolution;
        return new int[]{x,y}; 
    }

    public float degreeToScreenPos(float degree)
    {
        return ((VariableManagerScript.Instance.headset_fov / 2.0f) + degree) /
               VariableManagerScript.Instance.headset_fov;
    }
    
    public float degreeToScreenPos(float degree, bool invert)
    {
        return ((VariableManagerScript.Instance.headset_fov / 2.0f) - degree) /
               VariableManagerScript.Instance.headset_fov;
    }


    /// <summary>
    ///   This function converts an eccentricity measurement on the retinal
    /// surface(in micrometers), measured from the optic axis, into degrees
    /// of visual angle using Eq. A6 in [Watson2014]
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public float degreeToMicron(float degree)
    {
       
        float sign = degree >= 0 ? 1.0f : -1.0f;
        degree = Math.Abs(degree); 
        float micron = 0.268f * degree + 3.427e-4f * (float) Math.Pow(degree,2) - 8.3309e-6f * (float) Math.Pow(degree,3);
        micron = 1e3f * micron; 

        return micron * sign;
    }
    
    /// <summary>
    /// Converts retinal distances (um) to visual angles (deg)
    /// This function converts an eccentricity measurement on the retinal
    /// surface(in micrometers), measured from the optic axis, into degrees
    /// of visual angle using Eq. A6 in [Watson2014]
    /// </summary>
    /// <param name="micron"></param>
    /// <returns></returns>
    public float micronToDegree(float micron)
    {
        float sign = micron >= 0 ? 1.0f : -1.0f;
        float micronMM = 1e-3f * Math.Abs(micron);
        float degree = (3.556f * micronMM) + (0.05993f * (float) Math.Pow(micronMM,2)) - (0.007358f * (float) Math.Pow(micronMM,3));
        degree += 3.027e-4f * (float) Math.Pow(micronMM, 4);
        return sign * degree; 

    }
    
    public float pixelToDegree(int pixel)
    {
        return pixel * (VariableManagerScript.Instance.headset_fov / BackendShaderHandler.Instance.xResolution) - VariableManagerScript.Instance.headset_fov/2.0f; 
    }

    public float pixelToMicron(int pixel)
    {
        return degreeToMicron(pixelToDegree(pixel)); 
    }

    public float micronToScreenPos(float micron)
    {
        return degreeToScreenPos(micronToDegree(micron)); 
    }
    
    public float micronToScreenPos(float micron, bool invert)
    {
        return degreeToScreenPos(micronToDegree(micron), invert); 
    }

    public float screenPosToDegree(float screenPos)
    {
        return (screenPos - 0.5f) * VariableManagerScript.Instance.headset_fov; 
    }

    public float screenPosToDegree(float screenPos, bool invert)
    {
        return invert? screenPosToDegree(1 - screenPos) : screenPosToDegree(screenPos);
    }

    public float screenPosToMicron(float screenPos)
    {
        return degreeToMicron(screenPosToDegree(screenPos)); 
    }

    public float screenPosToMicron(float screenPos, bool invert)
    {
        return degreeToMicron(screenPosToDegree(screenPos, invert));
    }

}

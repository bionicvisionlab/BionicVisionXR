using System;
using UnityEngine;

/// <summary>
/// Degrees and microns are listed with negative/positive values assuming (0,0) is at the center of the screen
/// Screen position is solely positive and spans 0:1 (with .5 being at the center of the screen)
///
/// When converting from screen position to other coordinates (or vise versa), the y-axis needs inverted.
/// In screen position, y=0 is the top of the screen and y=1 is the bottom
///
/// Uses VariableManagerScript
/// Uses BackendShaderHandler
///
/// Contains:
///     int flatten2DElectrodePosition(int row, int col)
///     int flatten2DPixel(int x, int y)
///     int[] unflatten2DPixel(int flattened)
///     float degreeToScreenPos(float degree)
///     float degreeToScreenPos(float degree, bool invert)
///     float degreeToMicron(float degree)
///     float micronToDegree(float micron)
///     float pixelToDegree(int pixel)
///     float pixelToMicron(int pixel)
///     float micronToScreenPos(float micron)
///     float micronToScreenPos(float micron, bool invert)
///     float screenPosToDegree(float screenPos)
///     float screenPosToDegree(float screenPos, bool invert)
///     float screenPosToMicron(float screenPos)
///     float screenPosToMicron(float screenPos, bool invert)
/// </summary>
public class UnitConverter
{
    /// <summary>
    /// Converts a [row, col] index in a 2D array into the corresponding index in a 1D array
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public int flatten2DElectrodePosition(int row, int col)
    {
        return row + VariableManagerScript.Instance.numberYelectrodes * col;
    }
    
    /// <summary>
    /// Converts a [x, y] position in a 2D array into the corresponding index in a 1D array
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int flatten2DPixel(int x, int y)
    {
        return x + BackendShaderHandler.Instance.xResolution * y; 
    }
    
    /// <summary>
    /// Converts an index in a 1D array into an [x, y] position in the BackendShaderHandler's resolution
    /// </summary>
    /// <param name="flattened"></param>
    /// <returns></returns>
    public int[] unflatten2DPixel(int flattened)
    {
        int x = flattened / BackendShaderHandler.Instance.xResolution;
        int y = flattened % BackendShaderHandler.Instance.xResolution;
        return new int[]{x,y}; 
    }
    /// <summary>
    /// Converts an electrode's degrees position to the position on the screen
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public float degreeToScreenPos(float degree)
    {
        return ((VariableManagerScript.Instance.headset_fov / 2.0f) + degree) /
               VariableManagerScript.Instance.headset_fov;
    }
    
    /// <summary>
    /// Converts an electrode's inverted degrees position to the position on the screen
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Converts pixel value to degree value
    /// </summary>
    /// <param name="pixel"></param>
    /// <returns></returns>
    public float pixelToDegree(int pixel)
    {
        return pixel * (VariableManagerScript.Instance.headset_fov / BackendShaderHandler.Instance.xResolution) - VariableManagerScript.Instance.headset_fov/2.0f; 
    }
    /// <summary>
    /// Converts pixel value to micron value
    /// </summary>
    /// <param name="pixel"></param>
    /// <returns></returns>
    public float pixelToMicron(int pixel)
    {
        return degreeToMicron(pixelToDegree(pixel)); 
    }
    /// <summary>
    /// Converts micron value to screen position
    /// </summary>
    /// <param name="micron"></param>
    /// <returns></returns>
    public float micronToScreenPos(float micron)
    {
        return degreeToScreenPos(micronToDegree(micron)); 
    }
    /// <summary>
    /// Converts micron value with inverted degrees to screen position
    /// </summary>
    /// <param name="micron"></param>
    /// <param name="invert"></param>
    /// <returns></returns>
    public float micronToScreenPos(float micron, bool invert)
    {
        return degreeToScreenPos(micronToDegree(micron), invert); 
    }
    /// <summary>
    /// Converts screen position to degree value
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    public float screenPosToDegree(float screenPos)
    {
        return (screenPos - 0.5f) * VariableManagerScript.Instance.headset_fov; 
    }
    /// <summary>
    /// If inverted, converts (1-screenPos) to degree value
    /// Else, converts screenPos to degree value
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="invert"></param>
    /// <returns></returns>
    public float screenPosToDegree(float screenPos, bool invert)
    {
        return invert? screenPosToDegree(1 - screenPos) : screenPosToDegree(screenPos);
    }
    /// <summary>
    /// Converts screen position to micron value
    /// </summary>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    public float screenPosToMicron(float screenPos)
    {
        return degreeToMicron(screenPosToDegree(screenPos)); 
    }
    /// <summary>
    /// Converts screen position to micron value
    /// Uses (1-screenPos) to convert if invert is true
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="invert"></param>
    /// <returns></returns>
    public float screenPosToMicron(float screenPos, bool invert)
    {
        return degreeToMicron(screenPosToDegree(screenPos, invert));
    }

}

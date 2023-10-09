public struct AxonSegment
{
    public float xPosition;
    public float yPosition;
    public float brightnessContribution;

    /// <summary>
    /// Each pixel has associated AxonSegments that contribute to the brightness of the pixel
    /// The AxonSegments are generated with AxonMapHandler -> SetAxonMap()  [This will store an array of AxonSegment in BackendResourceManager]
    /// 
    /// xPosition and yPosition have two possible coordinates: screen position (0 to 1 texture coordinates) and retinal position (microns)
    /// Should initially be declared using retinal positions to avoid multiple conversions prior to running the shader
    ///
    /// In shaders, AxonSegments and Electrodes should use screen coordinates to avoid real-time micron->screenPos calculations 
    /// AxonMapHandler -> AxonSegmentsToScreenPosCoords()
    /// ElectrodesHandler -> ElectrodeGridToScreenPosCoords() 
    /// 
    /// </summary>
    /// <param name="_xPosition"></param> location of x (should initially be declared in microns, convert to screen position before shaders)
    /// <param name="_yPosition"></param> location of y (should initially be declared in microns, convert to screen position before shaders)
    /// <param name="_contribution"></param> The amount this axon segment contributes to the given pixel 
    public AxonSegment(float _xPosition, float _yPosition, float _contribution)
    {
        xPosition = _xPosition;
        yPosition = _yPosition;
        brightnessContribution = _contribution;
    }
    
    /// <summary>
    /// Each pixel has associated AxonSegments that contribute to the brightness of the pixel
    /// The AxonSegments are generated with AxonMapHandler -> SetAxonMap()  [This will store an array of AxonSegment in BackendResourceManager]
    /// 
    /// xPosition and yPosition have two possible coordinates: screen position (0 to 1 texture coordinates) and retinal position (microns)
    /// Should initially be declared using retinal positions to avoid multiple conversions prior to running the shader
    ///
    /// In shaders, AxonSegments and Electrodes should use screen coordinates to avoid real-time micron->screenPos calculations 
    /// AxonMapHandler -> AxonSegmentsToScreenPosCoords()
    /// ElectrodesHandler -> ElectrodeGridToScreenPosCoords() 
    /// 
    /// </summary>
    /// <param name="variableVector[0]"></param> location of x (should initially be declared in microns, convert to screen position before shaders)
    /// <param name="variableVector[1]"></param> location of y (should initially be declared in microns, convert to screen position before shaders)
    /// <param name="variableVector[2]"></param> The amount this axon segment contributes to the given pixel 
    public AxonSegment(float[] variableVector)
    {
        if (variableVector.Length == 3)
        {
            xPosition = variableVector[0];
            yPosition = variableVector[1];

            brightnessContribution = variableVector[2];
        }
        else
        {
            xPosition = variableVector[0];
            yPosition = variableVector[1];

            brightnessContribution = variableVector[3];
        }
    }
    
    public override string ToString()
    {
        return xPosition + ", " + yPosition + ", " + ":  " + brightnessContribution; 
    }
}
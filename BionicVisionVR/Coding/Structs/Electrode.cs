namespace BionicVisionVR.Structs
{
    public struct Electrode
    {
        public float xPosition;
        public float yPosition; 
        public float current;
        
        /// <summary>
        /// Stores location and electrical current 
        /// 
        /// xPosition and yPosition have two possible coordinates: screen position (0 to 1 texture coordinates) and retinal position (microns)
        /// Should initially be declared using retinal positions to avoid multiple conversions prior to running the shader
        ///
        ///  In shaders, AxonSegments and Electrodes should use screen coordinates to avoid real-time micron->screenPos calculations 
        /// AxonMapHandler -> AxonSegmentsToScreenPosCoords()
        /// ElectrodesHandler -> ElectrodeGridToScreenPosCoords() 
        /// 
        /// </summary>
        /// <param name="_xPosition"></param> location of x (should initially be declared in microns, convert to screen position before shaders)
        /// <param name="_yPosition"></param> location of y (should initially be declared in microns, convert to screen position before shaders)
        /// <param name="_current"></param> current of electrode

       public Electrode(float _xPosition, float _yPosition, float _current)
        {
            xPosition = _xPosition;
            yPosition = _yPosition; 
            current = _current; 
        }
    }
}
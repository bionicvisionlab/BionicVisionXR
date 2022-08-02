namespace BionicVisionVR.Structs
{
    public struct Electrode
    {
        public int electrodeNumber;
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

       public Electrode(int electrodeNumber, float xPosition, float yPosition, float current )
        {
            this.electrodeNumber = electrodeNumber;
            this.xPosition = xPosition;
            this.yPosition = yPosition; 
            this.current = current;
        }

        public override string ToString()
        {
            return "Electrode " + electrodeNumber + ": (" + xPosition + ", " + yPosition + ") -" + current; 
        }
    }
}
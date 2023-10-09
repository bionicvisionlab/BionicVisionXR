namespace BionicVisionVR.Coding.Structs
{
    public struct AxonMap
    {
        /// <summary>
        /// Stores information about individual pixels' associated AxonSegment
        ///
        /// axonSegmentContributions stores every simulated AxonSegment (the axon segment location and how much it contributes to the current pixel)
        ///
        /// axonIdxStart and axonIdxEnd determine the starting and ending position in axonSegmentContributions for the current pixel.
        ///
        /// For example, if pixel #104 had the following values:
        /// axonIdxStart[104]  = 203
        /// axonIdxEnd[104] = 207
        ///
        /// This would mean axonSegments 203, 204, 205, and 206 of axonSegmentContributions would contribute to pixel #104
        /// The amount each axon segment contributes is stored in each AxonSegment's "brightnessContribution"
        /// 
        /// </summary>
       
            public int[] axonIdxStart;
            public int[] axonIdxEnd; 
            public AxonSegment[] axonSegmentContributions;
    
            public AxonMap(int[] _axonIdxStart, int[] _axonIdxEnd, AxonSegment[] _axonSegmentContributions)
            {
                axonIdxStart = _axonIdxStart;
                axonIdxEnd = _axonIdxEnd;
                axonSegmentContributions = _axonSegmentContributions;
            }
    
        }

}
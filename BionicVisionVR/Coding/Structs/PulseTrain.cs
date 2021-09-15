public struct PulseTrain
{
    public uint positiveSignalStart; // timestep where positive signal starts
    public uint positiveSignalEnd; // timestep where positive signal ends
    public uint negativeSignalStart; // timestep where negative signal starts
    public uint negativeSignalEnd; // timestep where negative signal ends
    public uint numberTimeSteps; //number of timesteps between pulse starts
    public uint timeStepsPerFrame; //time steps that fit in one frame

    public PulseTrain(uint _positiveSignalStart, uint _positiveSignalEnd,
        uint _negativeSignalStart, uint _negativeSignalEnd, uint _numberTimeSteps,
        uint _timeStepsPerFrame)
    {
        positiveSignalStart = _positiveSignalStart;
        positiveSignalEnd = _positiveSignalEnd;
        negativeSignalStart = _negativeSignalStart;
        negativeSignalEnd = _negativeSignalEnd;
        numberTimeSteps = _numberTimeSteps;
        timeStepsPerFrame = _timeStepsPerFrame;
    }
}
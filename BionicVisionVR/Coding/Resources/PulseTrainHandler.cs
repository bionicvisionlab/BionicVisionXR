namespace BionicVisionVR.Resources
{
    public class PulseTrainHandler
    {
        public void SetPulseTrain()
        {
            // find all of this based off freq/pulse duration/ etc and update
            uint returnPositiveSignalStart = 0;
            uint returnPositiveSignalEnd = (uint)(VariableManagerScript.Instance.interphasePulseDuration/VariableManagerScript.Instance.simulationTimeStep);
            uint returnNegativeSignalStart = (uint)(VariableManagerScript.Instance.interphasePulseDuration/VariableManagerScript.Instance.simulationTimeStep);
            uint returnNegativeSignalEnd = (uint)(2*VariableManagerScript.Instance.interphasePulseDuration/VariableManagerScript.Instance.simulationTimeStep);
            uint returnNumberTimeSteps = (uint)(1/(VariableManagerScript.Instance.pulseFrequency*VariableManagerScript.Instance.simulationTimeStep));
            uint returnTimeStepsPerFrame = (uint)(VariableManagerScript.Instance.frameDuration/VariableManagerScript.Instance.simulationTimeStep);
        
            BackendShaderHandler.Instance.pulseTrain = new PulseTrain(returnPositiveSignalStart, returnPositiveSignalEnd, returnNegativeSignalStart, returnNegativeSignalEnd, returnNumberTimeSteps, returnTimeStepsPerFrame);
        }
    }
}
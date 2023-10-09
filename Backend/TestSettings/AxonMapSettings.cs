
using System;

public class AxonMapSettings
{
    public float rho;
    public float lambda; 
    public int xElectrodeCount;
    public int yElectrodeCount;
    public int electrodeSpacing;
    public float xPosition;
    public float yPosition;
    public float rotation; 

    public AxonMapSettings(float _rho, float _lambda, int _xElectrodeCount, int _yElectrodeCount, int _electrodeSpacing, float _xPosition, float _yPosition, float _rotation) {
        rho = _rho;
        lambda = _lambda;
        xElectrodeCount = _xElectrodeCount;
        yElectrodeCount = _yElectrodeCount;
        electrodeSpacing = _electrodeSpacing;
        xPosition = _xPosition;
        yPosition = _yPosition;
        rotation = _rotation; }
    
    public void UpdateSettings() {
        VariableManagerScript vm = VariableManagerScript.Instance;
        vm.rho = rho;
        vm.lambda = lambda;
        vm.numberXelectrodes = xElectrodeCount;
        vm.numberYelectrodes = yElectrodeCount;
        vm.electrodeSpacing = electrodeSpacing;
        vm.xPosition = xPosition;
        vm.yPosition = yPosition;
        vm.rotation = rotation; }

    public static AxonMapSettings GetPredefinedSettings(Enum settings) {
        AxonMapSettings returnBlock;
        
        switch (settings) {
            case AxonMapSettings_Enum.BuildingMode:
                returnBlock = new AxonMapSettings(200, 50, 6, 6, 800, 0, 0, 0);
                break; 
            case AxonMapSettings_Enum.DebugMode:
                returnBlock = new AxonMapSettings(100, 50, 15, 15, 400, 0, 0, 0);
                break;     
            
            
            case AxonMapSettings_Enum.SharedDifficulty0:
                returnBlock = new AxonMapSettings(150, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty1:
                returnBlock = new AxonMapSettings(200, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty2:
                returnBlock = new AxonMapSettings(250, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty3:
                returnBlock = new AxonMapSettings(300, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty4:
                returnBlock = new AxonMapSettings(350, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty5:
                returnBlock = new AxonMapSettings(400, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty6:
                returnBlock = new AxonMapSettings(450, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty7:
                returnBlock = new AxonMapSettings(500, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty8:
                returnBlock = new AxonMapSettings(600, 18, 10, 10, 400, 0, 0, 0); 
                break;
            case AxonMapSettings_Enum.SharedDifficulty9:
                returnBlock = new AxonMapSettings(800, 18, 10, 10, 400, 0, 0, 0); 
                break;
            default:
                returnBlock = new AxonMapSettings(100, 20, 10, 10, 200, 0, 0, 0 ); 
                break; } 
        return returnBlock; }

    public string PrintTestSettings(Enum settings) {
        return rho + "," + lambda + "," + xElectrodeCount + "," + yElectrodeCount + "," + xPosition + "," +
               yPosition + "," + electrodeSpacing + "," + rotation; }
}

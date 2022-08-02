using BionicVisionVR.Resources;

namespace Assets.BionicVisionVR.Coding.Structs
{
    public struct SimulationSettings
    {
        public float rho;
        public float lambda; 
        public int xElectrodeCount;
        public int yElectrodeCount;
        public int electrodeSpacing;
        public float xPosition;
        public float yPosition;
        public float rotation; 

        public SimulationSettings(float _rho, float _lambda, int _xElectrodeCount, int _yElectrodeCount, int _electrodeSpacing, float _xPosition, float _yPosition, float _rotation)
        {
            rho = _rho;
            lambda = _lambda;
            xElectrodeCount = _xElectrodeCount;
            yElectrodeCount = _yElectrodeCount;
            electrodeSpacing = _electrodeSpacing;
            xPosition = _xPosition;
            yPosition = _yPosition;
            rotation = _rotation; 
        }

        public string printSettings(bool header)
        {
            if (header)
                return
                    "Rho, Lambda, X_Electrodes, Y_Electrodes, X_Position_Device, Y_Position_Device, Electrode_Spacing, Device_Rot";
            return rho + "," + lambda + "," + xElectrodeCount + "," + yElectrodeCount + "," + xPosition + "," +
                   yPosition + "," + electrodeSpacing + "," + rotation; 
        }public string printSettings(){return printSettings(false);}

        public void UpdateSettings()
        {
            VariableManagerScript vm = VariableManagerScript.Instance;
            vm.rho = rho;
            vm.lambda = lambda;
            vm.numberXelectrodes = xElectrodeCount;
            vm.numberYelectrodes = yElectrodeCount;
            vm.electrodeSpacing = electrodeSpacing;
            vm.xPosition = xPosition;
            vm.yPosition = yPosition;
            vm.rotation = rotation;
        }

        public static SimulationSettings GetPreDefinedSimulationSettings(PreDefinedSettings setting)
        {
            SimulationSettings returnBlock;
            switch (setting)
            {
                case PreDefinedSettings.BuildingMode:
                    returnBlock = new SimulationSettings(200, 50, 6, 6, 800, 0, 0, 0);
                    break; 
                case PreDefinedSettings.DebugMode:
                    returnBlock = new SimulationSettings(100, 50, 15, 15, 400, 0, 0, 0);
                    break;     
                
                
                case PreDefinedSettings.RasterizationDifficulty0:
                    returnBlock = new SimulationSettings(150, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty1:
                    returnBlock = new SimulationSettings(200, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty2:
                    returnBlock = new SimulationSettings(250, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty3:
                    returnBlock = new SimulationSettings(300, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty4:
                    returnBlock = new SimulationSettings(350, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty5:
                    returnBlock = new SimulationSettings(400, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty6:
                    returnBlock = new SimulationSettings(450, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty7:
                    returnBlock = new SimulationSettings(500, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty8:
                    returnBlock = new SimulationSettings(600, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty9:
                    returnBlock = new SimulationSettings(800, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                case PreDefinedSettings.RasterizationDifficulty10:
                    returnBlock = new SimulationSettings(1000, 18, 10, 10, 400, 0, 0, 0); 
                    break;
                
               case PreDefinedSettings.ArgusMotion1: // Axon Map
                    returnBlock = new SimulationSettings(300, 1000, 10, 6, 300, 0, 0, 45); 
                    break;
				case PreDefinedSettings.ArgusMotion2:
                    returnBlock = new SimulationSettings(300, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
               case PreDefinedSettings.ArgusMotion3:
                    returnBlock = new SimulationSettings(300, 1000, 10, 6, 575, 0, 0, 90); 
                    break;
				case PreDefinedSettings.ArgusMotion4: // Scoreboard
                    returnBlock = new SimulationSettings(100, 50, 10, 6, 575, 0, 0, 0); 
                    break;
               case PreDefinedSettings.ArgusMotion5: 
                    returnBlock = new SimulationSettings(100, 50, 10, 6, 575, 0, 0, 45); 
                    break;
 				case PreDefinedSettings.ArgusMotion6: 
                    returnBlock = new SimulationSettings(100, 50, 10, 6, 575, 0, 0, 90);
                    break;
               case PreDefinedSettings.ArgusMotion7: // Patient 1 
                    returnBlock = new SimulationSettings(156, 621, 10, 6, 575, -1896, -542,-44); 
                    break;
               case PreDefinedSettings.ArgusMotion8: // Patient 2
                    returnBlock = new SimulationSettings(50, 1150, 10, 6, 575, -1203, 280, -35); 
                    break;
				case PreDefinedSettings.ArgusMotion9: // Patient 3
                    returnBlock = new SimulationSettings(199, 1983, 10, 6, 575, -1945, 469, -34); 
                    break;
                case PreDefinedSettings.ArgusMotion10: // Extended Axon Map
                    returnBlock = new SimulationSettings(300, 1000, 12, 20, 575, 0, 0, 0);
                    break;
                case PreDefinedSettings.ArgusMotion11:
                    returnBlock = new SimulationSettings(300, 1000, 12, 20, 575, 0, 0, 45);
                    break;
                case PreDefinedSettings.ArgusMotion12:
                    returnBlock = new SimulationSettings(300, 1000, 12, 20, 575, 0, 0, 90);
                    break;
               case PreDefinedSettings.ArgusMotion13: // Extended Scoreboard
                    returnBlock = new SimulationSettings(100, 50, 12, 20, 575, 0, 0, 0);
                    break;
                case PreDefinedSettings.ArgusMotion14:
                    returnBlock = new SimulationSettings(100, 50, 12, 20, 575, 0, 0, 45);
                    break;
                case PreDefinedSettings.ArgusMotion15:
                    returnBlock = new SimulationSettings(100, 50, 12, 20, 575, 0, 0, 90);
                    break;
                case PreDefinedSettings.ArgusMotion16: // Patient 1 Extended
                    returnBlock = new SimulationSettings(156, 621, 12, 20, 575, 0, 0, -44);
                    break;
                case PreDefinedSettings.ArgusMotion17: // Patient 2 Extended 
                    returnBlock = new SimulationSettings(50, 1150, 12, 20, 575, 0, 0, -35);
                    break;
                case PreDefinedSettings.ArgusMotion18: // Patient 3 Extended
                    returnBlock = new SimulationSettings(199, 1983, 12, 20, 575, 0, 0, -34);
                    break;
                
               case PreDefinedSettings.LetterTask1: 
                    returnBlock = new SimulationSettings(100, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask2:
                    returnBlock = new SimulationSettings(100, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask3:
                    returnBlock = new SimulationSettings(100, 5000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask4:
                    returnBlock = new SimulationSettings(300, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask5:
                    returnBlock = new SimulationSettings(300, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask6:
                    returnBlock = new SimulationSettings(300, 5000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask7:
                    returnBlock = new SimulationSettings(500, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask8:
                    returnBlock = new SimulationSettings(500, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedSettings.LetterTask9:
                    returnBlock = new SimulationSettings(500, 5000, 10, 6, 575, 0, 0, 45); 
                    break;                
                case PreDefinedSettings.LetterTask10: // Easiest settings
                    returnBlock = new SimulationSettings(100, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask11:
                    returnBlock = new SimulationSettings(100, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask12:
                    returnBlock = new SimulationSettings(100, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask13:
                    returnBlock = new SimulationSettings(300, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask14:
                    returnBlock = new SimulationSettings(300, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask15:
                    returnBlock = new SimulationSettings(300, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask16:
                    returnBlock = new SimulationSettings(500, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask17:
                    returnBlock = new SimulationSettings(500, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask18:
                    returnBlock = new SimulationSettings(500, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask19:
                    returnBlock = new SimulationSettings(100, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask20:
                    returnBlock = new SimulationSettings(100, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask21:
                    returnBlock = new SimulationSettings(100, 5000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask22:
                    returnBlock = new SimulationSettings(300, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask23:
                    returnBlock = new SimulationSettings(300, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask24:
                    returnBlock = new SimulationSettings(300, 5000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask25:
                    returnBlock = new SimulationSettings(500, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask26:
                    returnBlock = new SimulationSettings(500, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedSettings.LetterTask27:
                    returnBlock = new SimulationSettings(500, 5000, 16, 10, 575, 0, 0, 0); 
                    break;

               default:
                    returnBlock = new SimulationSettings(); 
                    break; 
            }

            return returnBlock; 
        }
    }
}
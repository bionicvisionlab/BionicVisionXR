using BionicVisionVR.Resources;

namespace Assets.BionicVisionVR.Coding.Structs
{
    public struct BlockSettings
    {
        public float rho;
        public float lambda; 
        public int xElectrodeCount;
        public int yElectrodeCount;
        public int electrodeSpacing;
        public float xPosition;
        public float yPosition;
        public float rotation; 

        public BlockSettings(float _rho, float _lambda, int _xElectrodeCount, int _yElectrodeCount, int _electrodeSpacing, float _xPosition, float _yPosition, float _rotation)
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

        public static BlockSettings GetPreDefinedBlockSettings(PreDefinedBlocks block)
        {
            BlockSettings returnBlock;
            switch (block)
            {
               case PreDefinedBlocks.ArgusMotion1: // Axon Map
                    returnBlock = new BlockSettings(300, 1000, 10, 6, 575, 0, 0, 0); 
                    break;
				case PreDefinedBlocks.ArgusMotion2:
                    returnBlock = new BlockSettings(300, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
               case PreDefinedBlocks.ArgusMotion3:
                    returnBlock = new BlockSettings(300, 1000, 10, 6, 575, 0, 0, 90); 
                    break;
				case PreDefinedBlocks.ArgusMotion4: // Scoreboard
                    returnBlock = new BlockSettings(100, 50, 10, 6, 575, 0, 0, 0); 
                    break;
               case PreDefinedBlocks.ArgusMotion5: 
                    returnBlock = new BlockSettings(100, 50, 10, 6, 575, 0, 0, 45); 
                    break;
 				case PreDefinedBlocks.ArgusMotion6: 
                    returnBlock = new BlockSettings(100, 50, 10, 6, 575, 0, 0, 90); 
                    break;
				case PreDefinedBlocks.ArgusMotion7: // Patient 1 
                    returnBlock = new BlockSettings(315, 500, 10, 6, 575, -1896, -542,-44); 
                    break;
               case PreDefinedBlocks.ArgusMotion8: // Patient 2
                    returnBlock = new BlockSettings(144, 1414, 10, 6, 575, -1203, 280, -35); 
                    break;
				case PreDefinedBlocks.ArgusMotion9: // Patient 3
                    returnBlock = new BlockSettings(437, 1420, 10, 6, 575, -1945, 469, -34); 
                    break;

                case PreDefinedBlocks.LetterTask1: 
                    returnBlock = new BlockSettings(100, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask2:
                    returnBlock = new BlockSettings(100, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask3:
                    returnBlock = new BlockSettings(100, 5000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask4:
                    returnBlock = new BlockSettings(300, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask5:
                    returnBlock = new BlockSettings(300, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask6:
                    returnBlock = new BlockSettings(300, 5000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask7:
                    returnBlock = new BlockSettings(500, 50, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask8:
                    returnBlock = new BlockSettings(500, 1000, 10, 6, 575, 0, 0, 45); 
                    break;
                case PreDefinedBlocks.LetterTask9:
                    returnBlock = new BlockSettings(500, 5000, 10, 6, 575, 0, 0, 45); 
                    break;                
                case PreDefinedBlocks.LetterTask10: // Easiest settings
                    returnBlock = new BlockSettings(100, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask11:
                    returnBlock = new BlockSettings(100, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask12:
                    returnBlock = new BlockSettings(100, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask13:
                    returnBlock = new BlockSettings(300, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask14:
                    returnBlock = new BlockSettings(300, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask15:
                    returnBlock = new BlockSettings(300, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask16:
                    returnBlock = new BlockSettings(500, 50, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask17:
                    returnBlock = new BlockSettings(500, 1000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask18:
                    returnBlock = new BlockSettings(500, 5000, 30, 19, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask19:
                    returnBlock = new BlockSettings(100, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask20:
                    returnBlock = new BlockSettings(100, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask21:
                    returnBlock = new BlockSettings(100, 5000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask22:
                    returnBlock = new BlockSettings(300, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask23:
                    returnBlock = new BlockSettings(300, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask24:
                    returnBlock = new BlockSettings(300, 5000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask25:
                    returnBlock = new BlockSettings(500, 50, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask26:
                    returnBlock = new BlockSettings(500, 1000, 16, 10, 575, 0, 0, 0); 
                    break;
                case PreDefinedBlocks.LetterTask27:
                    returnBlock = new BlockSettings(500, 5000, 16, 10, 575, 0, 0, 0); 
                    break;
                
                
                
                default:
                    returnBlock = new BlockSettings(); 
                    break; 
            }

            return returnBlock; 
        }
    }
}
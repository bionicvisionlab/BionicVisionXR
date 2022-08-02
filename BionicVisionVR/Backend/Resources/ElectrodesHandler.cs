using System;
using BionicVisionVR.Structs;

namespace BionicVisionVR.Resources
{
    /// <summary>
    /// Container class of functions relating to electrode grids
    /// Uses VariableManagerScript and BackendShaderHandler
    /// Contains:
    ///     void SetRectangularGrid()
    ///     void ElectrodeGridToScreenPosCoords()
    /// </summary>
    public class  ElectrodesHandler
    { 
        UnitConverter unitConverter = new UnitConverter(); 
        private float deg2rad = 0.01745329251f;
        
        /// <summary>
        /// Creates an evenly spaced rectangular electrode grid 
        /// Uses VariableManagerScript: "numberXelectrodes", "numberYelectrodes", "xPosition", "yPosition", "rotation", and "electrodeSpacing"
        /// </summary>
        public void SetRectangularGrid()
        {
            Electrode[] electrodes = new Electrode[VariableManagerScript.Instance.numberYelectrodes * VariableManagerScript.Instance.numberXelectrodes];
            float x_pos;
            float y_pos;
            float x_temp;
            float y_temp;

            for (int row = 0; row < VariableManagerScript.Instance.numberYelectrodes; row++)
            {
                for (int col = 0; col < VariableManagerScript.Instance.numberXelectrodes; col++)
                {
                    // Space out electrodes
                    x_pos = (col - VariableManagerScript.Instance.numberXelectrodes / 2.0f + 0.5f) * VariableManagerScript.Instance.electrodeSpacing;
                    y_pos = (row - VariableManagerScript.Instance.numberYelectrodes / 2.0f + 0.5f) * VariableManagerScript.Instance.electrodeSpacing;

                    // Rotate electrodes 
                    x_temp = (float) (x_pos * Math.Cos(deg2rad*VariableManagerScript.Instance.rotation) - y_pos * Math.Sin(deg2rad*VariableManagerScript.Instance.rotation));
                    y_temp = (float) (x_pos * Math.Sin(deg2rad*VariableManagerScript.Instance.rotation) + y_pos * Math.Cos(deg2rad*VariableManagerScript.Instance.rotation));

                    // Shift electrodes
                    x_pos = x_temp + VariableManagerScript.Instance.xPosition; 
                    y_pos = y_temp + VariableManagerScript.Instance.yPosition;

                    int elecNumber = unitConverter.flatten2DElectrodePosition(row, col); 
                    electrodes[elecNumber] = new Electrode(elecNumber, x_pos, 1 - y_pos , 0.0f);
                }
            }
            
            BackendShaderHandler.Instance.electrodes = electrodes;
                            
            if (VariableManagerScript.Instance.savePremadeConfiguration)
            {
                VariableManagerScript.Instance.updateConfigurationPath();
                
                BinaryHandler binaryHandler = new BinaryHandler();
                binaryHandler.WriteElectrodeLocations(VariableManagerScript.Instance.configurationPath, electrodes);
            }
        }
        
        /// <summary>
         /// Converts all Electrode in electrodes of BackendResourceManager to screen positions,
         /// should be ran prior to using Electrode locations inside a shader.  
         /// </summary>
         public void ElectrodeGridToScreenPosCoords()
         {
             UnitConverter unitConverter = new UnitConverter();
                     
             for (int i = 0; i < BackendShaderHandler.Instance.electrodes.Length; i++)
             {
                 BackendShaderHandler.Instance.electrodes[i].xPosition =
                     unitConverter.micronToScreenPos(BackendShaderHandler.Instance.electrodes[i].xPosition);
                         
                 BackendShaderHandler.Instance.electrodes[i].yPosition =
                     unitConverter.micronToScreenPos(BackendShaderHandler.Instance.electrodes[i].yPosition);
             }
        }
        
    }
}
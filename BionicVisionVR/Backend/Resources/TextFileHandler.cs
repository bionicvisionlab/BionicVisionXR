using System.IO;
using UnityEngine;

namespace BionicVisionVR.Resources
{
    /// <summary>
    /// Writes and reads information from files about p2p variables
    /// Uses VariableManagerScript
    /// Contains:
    ///     void WriteP2PVariables()
    ///     void ReadString()
    ///     void ClearP2PFile()
    /// </summary>
    public class TextFileHandler
    {
        string path = "Assets/Scripts/p2pVariables";
        /// <summary>
        /// Write VariableManagerScript's xPosition, yPosition, and implant_fov to "Assets/Scripts/p2pVariables"
        /// </summary>
        public void WriteP2PVariables()
        {
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(VariableManagerScript.Instance.xPosition.ToString());
            writer.WriteLine(VariableManagerScript.Instance.yPosition.ToString());
            writer.WriteLine(VariableManagerScript.Instance.implant_fov.ToString());
            writer.Close();
        
        }
        
        /// <summary>
        /// Debug.logs text from "Assets/Resources/test.txt"
        /// </summary>
        public void ReadString()
        {
            string path = "Assets/Resources/test.txt";

            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path); 
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }
        
        /// <summary>
        /// Deletes the file at "Assets/Scripts/p2pVariables"
        /// </summary>
        public void ClearP2PFile()
        {
            File.Delete(path); 
        }

    }
}
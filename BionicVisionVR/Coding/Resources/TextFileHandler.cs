using System.IO;
using UnityEngine;

namespace BionicVisionVR.Resources
{
    public class TextFileHandler
    {
        string path = "Assets/Scripts/p2pVariables";
    
        public void WriteP2PVariables()
        {
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(VariableManagerScript.Instance.xPosition.ToString());
            writer.WriteLine(VariableManagerScript.Instance.yPosition.ToString());
            writer.WriteLine(VariableManagerScript.Instance.implant_fov.ToString());
            writer.Close();
        
        }
    
        public void ReadString()
        {
            string path = "Assets/Resources/test.txt";

            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path); 
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }

        public void ClearP2PFile()
        {
            File.Delete(path); 
        }

    }
}
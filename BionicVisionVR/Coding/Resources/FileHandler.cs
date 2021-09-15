using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BionicVisionVR.Resources
 {
     public class FileHandler
     {
         /// <summary>
         /// Creates file if it doesn't exist
         /// Then appends line 
         /// </summary>
         public void AppendLine(string path, string newLine)
         {
             if (!File.Exists(path))
             {
                 using (StreamWriter file = File.CreateText(path))
                 {
                     file.WriteLine(newLine);
                 }
             }

             else
             {
                 using (System.IO.StreamWriter file =
                     new System.IO.StreamWriter(@path, true))
                 {
                     file.WriteLine(newLine);
                 }
             }
         }

         public void removeLastLine(string path, int numLines)
         {
             List<string> lineList = new List<string>(File.ReadAllLines(path));
             lineList.RemoveAt(lineList.Count - numLines);
             File.WriteAllLines(path, lineList.ToArray());
         }

         public void removeLastLine(string path)
         {
             removeLastLine(path, 1);
         }
     }
 }


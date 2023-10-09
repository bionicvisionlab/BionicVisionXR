using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Used to handle writing to files.  
/// </summary>
public class FileHandler
{
    /// <summary>
    /// Creates file (or directory) if it doesn't exist
    /// Then appends line 
    /// </summary>
    public void AppendLine(string path, string newLine)
    {            
        CheckPath(path);
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
    /// <summary>
    /// Removes the last line of a file
    /// </summary>
    /// <param name="path">Path of file</param>
    /// <param name="numLines">Number of lines in file</param>
    public void RemoveLastLine(string path, int numLines)
    {
        List<string> lineList = new List<string>(File.ReadAllLines(path));
        lineList.RemoveAt(lineList.Count - numLines);
        File.WriteAllLines(path, lineList.ToArray());
    }
    /// <summary>
    /// Checks that the given path exists
    /// </summary>
    /// <param name="path">Path to check</param>
    /// <returns>True if the given path exists : false</returns>
    public bool CheckPath(string path)
    {
        string directory = Path.GetDirectoryName(path); 
        if(directory!=null & !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        return Directory.Exists(directory);
    }
}

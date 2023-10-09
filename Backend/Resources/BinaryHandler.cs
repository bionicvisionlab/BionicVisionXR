using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BionicVisionVR.Coding.Structs;
using BionicVisionVR.Structs;
/// <summary>
/// Handles writing types to binary files, and reading types from binary files
///     Types Read: int, float, float[,], AxonSegment, AxonMap, Electrode
///     Types Written: float[], AxonSegment, AxonMap, Electrode
/// Contains:
///     List<int> ReadFromBinaryFile(string path)
///     List<float> ReadFloatsFromBinaryFile(string path)
///     float[,] Read2DArray_float32(int row, int col, string path)
///     AxonSegment[] ReadAxonSegments(string path)
///     void WriteAxonSegments(string path, AxonSegment[] axonSegments)
///     void WriteFloatArray(string path, float[] floatArray)
///     void WriteAxonMap(string path, AxonMap axonMap)
///     AxonMap ReadAxonMap(string path)
///     void WriteElectrodeLocations(string path, Electrode[] electrodes)
///     Electrode[] ReadElectrodeLocations(string path)
///     bool CheckPath(string path)
/// </summary>
public class BinaryHandler
{
    /// <summary>
    /// Reads ints from a given binary file and returns the list of ints.
    /// </summary>
    /// <param name="path">Path of the binary file to read from</param>
    /// <returns>List of ints read from given file</returns>
    public List<int> ReadFromBinaryFile(string path)
    {
        // Approach one
        using (var filestream = File.Open(path, FileMode.Open))
        using (var binaryStream = new BinaryReader(filestream))
        {
            var pos = 0;
            List<int> result = new List<int>();
            var length = (float) binaryStream.BaseStream.Length;
            while (pos < length)
            {
                int element = binaryStream.ReadInt32();
                result.Add(element);
                pos += sizeof(int);
            }

            return result;
        }
    }
    
    /// <summary>
    /// Reads floats from a given binary file and returns the list of floats.
    /// </summary>
    /// <param name="path">Path of the binary file to read from</param>
    /// <returns>List of floats read from given file</returns>
    public List<float> ReadFloatsFromBinaryFile(string path)
    {
        // Approach one
        using (var filestream = File.Open(path, FileMode.Open))
        using (var binaryStream = new BinaryReader(filestream))
        {
            var pos = 0;
            List<float> result = new List<float>();
            var length = (float) binaryStream.BaseStream.Length;
            while (pos < length)
            {
                float element = binaryStream.ReadSingle();
                result.Add(element);
                pos += sizeof(float);
            }

            return result;
        }
    }
    /// <summary>
    /// Reads floats from the given binary file and places them in order into a 2D array of the given size
    /// </summary>
    /// <param name="row">Number of rows</param>
    /// <param name="col">Number of columns</param>
    /// <param name="path">Path of the binary file to read from</param>
    /// <returns>2D array of floats read from given file</returns>
    public float[,] Read2DArray_float32(int row, int col, string path)
    {
        float[,] rate_buff = new float[row, col];

        // open the file
        using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
        {
            for (int i = 0; i < row; i++)
            {
                // read the doubles out of the byte buffer into the two dimensional array
                // note this assumes machine-endian byte order
                for (int j = 0; j < col; j++)
                {
                    float temp = reader.ReadSingle();
                    rate_buff[i, j] = temp;
                }
            }

            //return rate_buff;
        }

        return rate_buff;
    }
    /// <summary>
    /// Reads info from the given binary file and translates that into AxonSegments
    /// </summary>
    /// <param name="path">Path of the binary file to read from</param>
    /// <returns>Array of AxonSegments read from given file</returns>
    public AxonSegment[] ReadAxonSegments(string path)
    {
        CheckPath(path);
        using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
        {
            AxonSegment[] axon_buff = new AxonSegment[reader.BaseStream.Length/3/sizeof(float)];

            for (int i = 0; i < axon_buff.Length; i++)
            {
                axon_buff[i] = new AxonSegment(new float[] {reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()});
            }
            return axon_buff;
        }
    }
    /// <summary>
    /// Writes the given array of AxonSegments to the given binary file
    /// </summary>
    /// <param name="path">Path of the binary file to write to</param>
    /// <param name="axonSegments">Array of AxonSegments to write to file</param>
    public void WriteAxonSegments(string path, AxonSegment[] axonSegments)
    {
        CheckPath(path);
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (AxonSegment item in axonSegments)
            {
                writer.Write(item.xPosition);
                writer.Write(item.yPosition); 
                writer.Write(item.brightnessContribution);
            }
        }
    }
    /// <summary>
    /// Writes the given array of floats to the given binary file
    /// </summary>
    /// <param name="path">Path of the binary file to write to</param>
    /// <param name="axonSegments">Array of floats to write to file</param>
    public void WriteFloatArray(string path, float[] floatArray)
    {
        CheckPath(path);
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (float item in floatArray)
            {
                writer.Write(item);
            }
        }
    }
    /// <summary>
    /// Writes all AxonMap.axonIdxStart values to path+"_axonIdxStart"
    /// Writes all AxonMap.axonIdxEnd values to path+"_axonIdxEnd"
    /// Writes AxonMap.axonSegmentContributions segments to path+"_axonSegments"
    /// </summary>
    /// <param name="path">Path of the binary file to write to</param>
    /// <param name="axonMap">AxonMap to write to file</param>
    public void WriteAxonMap(string path, AxonMap axonMap)
    {
        CheckPath(path);
        using (var stream = new FileStream(path+"_axonIdxStart", FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (int item in axonMap.axonIdxStart)
            {
                writer.Write(item);
            }
        }
        using (var stream = new FileStream(path+"_axonIdxEnd", FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (int item in axonMap.axonIdxEnd)
            {
                writer.Write(item);
            }
        }

        WriteAxonSegments(path + "_axonSegments", axonMap.axonSegmentContributions);
    }
    /// <summary>
    /// Creates an AxonMap based off of values from binary files using the given path
    /// </summary>
    /// <param name="path">Path to read from</param>
    /// <returns>AxonMap created from values found in given file</returns>
    public AxonMap ReadAxonMap(string path)
    {
        return new AxonMap(ReadFromBinaryFile(path + "_axonIdxStart").ToArray(),
            ReadFromBinaryFile(path + "_axonIdxEnd").ToArray(), ReadAxonSegments(path + "_axonSegments")); 
    }
    /// <summary>
    /// Writes X positions of given array of electrodes to path+"_electrodePositionsX"
    /// Writes Y positions of given array of electrodes to path+"_electrodePositionsY"
    /// </summary>
    /// <param name="path">Path to write to</param>
    /// <param name="electrodes">Array of electrodes to write</param>
    public void WriteElectrodeLocations(string path, Electrode[] electrodes)
    {
        CheckPath(path);
        using (var stream = new FileStream(path+"_electrodePositionsX", FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (Electrode item in electrodes)
            {
                writer.Write(item.xPosition);
            }
        }
        using (var stream = new FileStream(path+"_electrodePositionsY", FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            foreach (Electrode item in electrodes)
            {
                writer.Write(item.yPosition); 
            }
        }
    }
    /// <summary>
    /// Creates an Electrode array based off of values from binary files using the given path
    /// </summary>
    /// <param name="path">Path to read from</param>
    /// <returns>Electrode array created from values found in given file</returns>
    public Electrode[] ReadElectrodeLocations(string path)
    {
        CheckPath(path);
        float[] electrodePositionsX = ReadFloatsFromBinaryFile(path + "_electrodePositionsX").ToArray();
        float[] electrodePositionsY = ReadFloatsFromBinaryFile(path + "_electrodePositionsY").ToArray();
        Electrode[] electrodes = new Electrode[electrodePositionsX.Length];

        for (int i = 0; i < electrodes.Length; i++)
        {
            electrodes[i] = new Electrode(i, electrodePositionsX[i], electrodePositionsY[i], 0);
        }

        return electrodes; 
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BionicVisionVR.Structs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BionicVisionVR.Resources
{
    /// <summary>
    /// Handles creation of BackendResourceManager's "axonMap"
    /// Requires pulse2percept - https://pulse2percept.readthedocs.io/en/stable/install.html
    /// </summary>
    public class AxonMapHandler
    {
        public ComputeShader computeShader; 
        
        private BinaryHandler binaryHandler = new BinaryHandler();
        private UnitConverter unitConverter = new UnitConverter();
        
        /// <summary>
        /// Creates BackendResourceManager's axonMap
        /// Uses BackendResourceManager: "simulation_Xmin",  "simulation_ymin",  "simulation_Xmax",  "simulation_Ymax", simulation_xyStep
        /// Uses VariableManagerScript: 
        /// </summary>
        public void SetAxonMap()
        {
            String pythonPath = Application.dataPath + Path.DirectorySeparatorChar + "BionicVisionVR" +
                                Path.DirectorySeparatorChar
                                + "Coding" + Path.DirectorySeparatorChar + "python" + Path.DirectorySeparatorChar;

            System.IO.Directory.CreateDirectory(pythonPath);
            
            Debug.Log("Waiting for pulse2percept Axon Map calculations...");
            Debug.Log(pythonPath);

            if (VariableManagerScript.Instance.calculateAxonMap)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "open";
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardOutput = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                processStartInfo.CreateNoWindow = false;
                
                string pulse2perceptCmdCall = "python " + pythonPath+"build-p2p.py " +
                                  pythonPath
                                  + " " + BackendShaderHandler.Instance.simulation_xMin + " " + BackendShaderHandler.Instance.simulation_xMax + " " + BackendShaderHandler.Instance.simulation_yMin + " " +
                                  BackendShaderHandler.Instance.simulation_yMax + " " + BackendShaderHandler.Instance.simulation_xyStep + " " + VariableManagerScript.Instance.rho + " " + VariableManagerScript.Instance.lambda + 
                                  " " + VariableManagerScript.Instance.axonContributionThreshold + " " + VariableManagerScript.Instance.number_axons + " " + VariableManagerScript.Instance.number_axon_segments + " " + VariableManagerScript.Instance.useLeftEye;
                UnityEngine.Debug.Log(pulse2perceptCmdCall);
                processStartInfo.Arguments = pulse2perceptCmdCall;
                
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
                UnityEngine.Debug.Log("Done waiting");
                
            }
            
            List<int> axon_contrib_shape =
                binaryHandler.ReadFromBinaryFile(Application.dataPath + "/BionicVisionVR/Coding/python/axon_contrib_length.dat");

            BackendShaderHandler.Instance.axonMap.axonIdxStart = binaryHandler.ReadFromBinaryFile(pythonPath + "axon_idx_start.dat")
                .ToArray();
            BackendShaderHandler.Instance.axonMap.axonIdxEnd = binaryHandler.ReadFromBinaryFile(pythonPath + "axon_idx_end.dat")
                .ToArray();
            
            BackendShaderHandler.Instance.axonMap.axonSegmentContributions =
                binaryHandler.ReadAxonSegments(pythonPath + "axon_contrib.dat");

            if (VariableManagerScript.Instance.savePremadeConfiguration)
            {
                binaryHandler.WriteAxonMap(VariableManagerScript.Instance.configurationPath, BackendShaderHandler.Instance.axonMap);
            }
        }

      /// <summary>
        /// Pre calculates how much current from each Electrode in BackendResourceManager -> "electrodes" reaches each AxonSegment in
        /// BackendResourceManager -> "axonMap.axonSegmentContributions"
        /// </summary>
        public void SetElectrodeToAxonSegmentGauss()
        {
            //TODO
            // 1) Implement multi-threading - (Java sample in BrainPoke (Main line #323 addSynapses(), find C# equivalent of taskExecutor) - Done!
            // 2) Find axonContributionThresholding value that ensures 
            
            Timer time = new Timer("Calc Time");
            time.start();
            int numberElectrodes = VariableManagerScript.Instance.numberYelectrodes *
                                   VariableManagerScript.Instance.numberXelectrodes;

            //Memory Limit reached -- Find what's taking memory prior to this as calculations show this shouldn't be happening: 

            computeShader = UnityEngine.Resources.Load<ComputeShader>("AxonElectrodeGauss");
            Debug.Log(BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length + "*" + numberElectrodes); 
            int kernel = VariableManagerScript.Instance.debugMode ? computeShader.FindKernel("calculateGaussDebug") : computeShader.FindKernel("calculateGauss");
            if ((long) BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * (long) numberElectrodes >
                Int32.MaxValue || BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes == 0)
            {
                Debug.Log(
                    "Not computationally feasible.  Please raise downscaling factor or lower number of electrodes");
            }
            else
            {

                BackendShaderHandler.Instance.axonSegmentGaussToElectrodes =
                    new ComputeBuffer(
                        BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes,
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof(float)), ComputeBufferType.Default);
                //Graphics.SetRandomWriteTarget(4, axonSegmentGaussToElectrodesBuffer, true);

                ComputeBuffer electrodesBuffer =
                    new ComputeBuffer(BackendShaderHandler.Instance.electrodes.Length,
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof(Electrode)), ComputeBufferType.Default);
                electrodesBuffer.SetData(BackendShaderHandler.Instance.electrodes);

                ComputeBuffer axonsBuffer =
                    new ComputeBuffer(BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length,
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof(AxonSegment)), ComputeBufferType.Default);
                axonsBuffer.SetData(BackendShaderHandler.Instance.axonMap.axonSegmentContributions);

                ComputeBuffer debugInfoBuf = null;
                if (VariableManagerScript.Instance.debugMode)
                {
                    debugInfoBuf = new ComputeBuffer(
                        BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes,
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)), ComputeBufferType.Default);
                    computeShader.SetBuffer(kernel, "DebugInfo", debugInfoBuf);
                }

                computeShader.SetBuffer(kernel, "Electrodes", electrodesBuffer);
                computeShader.SetBuffer(kernel, "Axons", axonsBuffer);
                computeShader.SetBuffer(kernel, "AxonSegmentGauss",
                    BackendShaderHandler.Instance.axonSegmentGaussToElectrodes);

                computeShader.SetInt("ElectrodeCount", numberElectrodes);
                computeShader.SetFloat("rho", VariableManagerScript.Instance.rho);
                computeShader.Dispatch(kernel,
                    BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length / 1024 + 1, 1, 1);
                electrodesBuffer.Release();
                axonsBuffer.Release();

                if (VariableManagerScript.Instance.debugMode)
                {
                    Vector3[] debugInfo =
                        new Vector3[BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length *
                                    numberElectrodes];
                    debugInfoBuf.GetData(debugInfo);

                    int numberAxons = BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length;
                    int arrayIndex = 0;
                    for (int currentAxon = 0; currentAxon < numberAxons; currentAxon++)
                    {
                        for (int currentElectrode = 0; currentElectrode < numberElectrodes; currentElectrode++)
                        {
                            if (currentAxon ==
                                BackendShaderHandler.Instance.axonMap.axonIdxStart[
                                    VariableManagerScript.Instance.specificPixelToDebug] && currentElectrode == 0)
                                Debug.Log("Pixel " + VariableManagerScript.Instance.specificPixelToDebug + ": "
                                          + BackendShaderHandler.Instance.axonMap.axonIdxStart[
                                              VariableManagerScript.Instance.specificPixelToDebug]
                                          + "," + BackendShaderHandler.Instance.axonMap.axonIdxEnd[
                                              VariableManagerScript.Instance.specificPixelToDebug]);

                            if (currentAxon >
                                BackendShaderHandler.Instance.axonMap.axonIdxStart[
                                    VariableManagerScript.Instance.specificPixelToDebug] &&
                                currentAxon <
                                BackendShaderHandler.Instance.axonMap.axonIdxEnd[
                                    VariableManagerScript.Instance.specificPixelToDebug])
                            {
                                Debug.Log("Electrode screen pos: " +
                                          unitConverter.micronToScreenPos(BackendShaderHandler.Instance
                                              .electrodes[currentElectrode].xPosition) + "," +
                                          unitConverter.micronToScreenPos(BackendShaderHandler.Instance
                                              .electrodes[currentElectrode].yPosition));
                                Debug.Log("Electrode degree pos: " +
                                          unitConverter.micronToDegree(BackendShaderHandler.Instance
                                              .electrodes[currentElectrode].xPosition) + "," +
                                          unitConverter.micronToDegree(BackendShaderHandler.Instance
                                              .electrodes[currentElectrode].yPosition));
                                Debug.Log("Electrode micron pos: " +
                                          BackendShaderHandler.Instance.electrodes[currentElectrode].xPosition + "," +
                                          BackendShaderHandler.Instance.electrodes[currentElectrode].yPosition);

                                Debug.Log("Axon segment screen pos: " +
                                          unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap
                                              .axonSegmentContributions[currentAxon].xPosition) +
                                          "," + unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap
                                              .axonSegmentContributions[currentAxon].yPosition));
                                Debug.Log("Axon segment degree pos: " +
                                          unitConverter.micronToDegree(BackendShaderHandler.Instance.axonMap
                                              .axonSegmentContributions[currentAxon].xPosition) + "," +
                                          unitConverter.micronToDegree(BackendShaderHandler.Instance.axonMap
                                              .axonSegmentContributions[currentAxon].yPosition));
                                Debug.Log("Axon segment micron pos: " +
                                          BackendShaderHandler.Instance.axonMap.axonSegmentContributions[currentAxon]
                                              .xPosition + "," +
                                          BackendShaderHandler.Instance.axonMap.axonSegmentContributions[currentAxon]
                                              .yPosition);

                                Debug.Log("Distance^2: " + debugInfo[arrayIndex].x);
                                Debug.Log("Exp inner: " + debugInfo[arrayIndex].y);
                                Debug.Log("Electrode gauss: " + debugInfo[arrayIndex].z);
                                arrayIndex++;
                            }
                        }
                    }
                }

                if (VariableManagerScript.Instance.savePremadeConfiguration)
                {
                    
                    if ((long) BackendShaderHandler.Instance.electrodes.Length * BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length >
                        Int32.MaxValue || BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes == 0)
                    {
                        Debug.Log(
                            "Not computationally feasible.  Please raise downscaling factor or lower number of electrodes");
                        
                    }
                    else
                    {
                        
                        float[] electrodeGauss = new float[BackendShaderHandler.Instance.electrodes.Length *
                                                           BackendShaderHandler.Instance.axonMap
                                                               .axonSegmentContributions
                                                               .Length];
                        BackendShaderHandler.Instance.axonSegmentGaussToElectrodes.GetData(electrodeGauss);
                        binaryHandler.WriteFloatArray(
                            VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss", electrodeGauss
                        );
                    }
                }

                Debug.Log("Finished Calculating Electrode Distances");
                time.stopAndLog();
            }
        }
        /// <summary>
        /// Converts all AxonSegment in axonMap of BackendResourceManager to screen positions,
        /// should be ran prior to using AxonSegment locations inside a shader.  
        /// </summary>
        public void AxonSegmentsToScreenPosCoords()
        {
            UnitConverter unitConverter = new UnitConverter();
            
            for (int i = 0; i < BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length; i++)
            {
                BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i].xPosition =
                    unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i]
                        .xPosition);
                
                BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i].yPosition =
                    unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i]
                        .yPosition);
            }
        }
    }
}
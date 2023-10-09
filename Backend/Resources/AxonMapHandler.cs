using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BionicVisionVR.Backend.Resources;
using BionicVisionVR.Structs;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Handles creation of BackendResourceManager's "axonMap"
/// Requires pulse2percept - https://pulse2percept.readthedocs.io/en/stable/install.html
/// Uses VariableManagerScript and BackendShaderHandler
/// </summary>
public class AxonMapHandler
{
    private ComputeShader computeShader;
    private BinaryHandler binaryHandler = new BinaryHandler();
    private UnitConverter unitConverter = new UnitConverter();
    
    public void SetAxonsAndElectrodesGauss() {
        SetAxonMap();
        SetElectrodeToAxonSegmentGauss(); }
    
    /// <summary>
    /// Creates BackendShaderHandler's axonMap
    /// Uses BackendShaderHandler: "simulation_Xmin",  "simulation_ymin",  "simulation_Xmax",  "simulation_Ymax", simulation_xyStep
    /// Uses VariableManagerScript: 
    /// </summary>
    public void SetAxonMap() {
        VariableManagerScript.Instance.updateConfigurationPath();
        String pythonPath = VariableManagerScript.Instance.backendPath  + "python" + Path.DirectorySeparatorChar;

        System.IO.Directory.CreateDirectory(pythonPath);
        
        ErrorDebug.Log("Waiting for pulse2percept Axon Map calculations...");
        ErrorDebug.Log(pythonPath);

        if (VariableManagerScript.Instance.calculateAxonMap) {
            //File.Delete("axon_contrib.dat");
            //File.Delete("axon_contrib_length.dat");
            //File.Delete("axon_idx_end.dat");
            //File.Delete("axon_idx_start.dat");
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "python";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = false;
            processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            processStartInfo.CreateNoWindow = false;
            
            string pulse2perceptCmdCall = pythonPath+"build-p2p.py " +
                              pythonPath
                              + " " + BackendShaderHandler.Instance.simulation_xMin + " " + BackendShaderHandler.Instance.simulation_xMax + " " + BackendShaderHandler.Instance.simulation_yMin + " " +
                              BackendShaderHandler.Instance.simulation_yMax + " " + BackendShaderHandler.Instance.simulation_xyStep + " " + VariableManagerScript.Instance.rho + " " + VariableManagerScript.Instance.lambda + 
                              " " + VariableManagerScript.Instance.axonContributionThreshold + " " + VariableManagerScript.Instance.number_axons + " " + VariableManagerScript.Instance.number_axon_segments + " " + VariableManagerScript.Instance.useLeftEye;
            
            Debug.Log(pulse2perceptCmdCall);
            processStartInfo.Arguments = pulse2perceptCmdCall;
            
            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
            ErrorDebug.Log("Done waiting"); 
        }
        
        VariableManagerScript.Instance.updateConfigurationPath();
        
        List<int> axon_contrib_shape =
            binaryHandler.ReadFromBinaryFile(VariableManagerScript.Instance.backendPath + "python/axon_contrib_length.dat");

        BackendShaderHandler.Instance.axonMap.axonIdxStart = binaryHandler.ReadFromBinaryFile(pythonPath + "axon_idx_start.dat")
            .ToArray();
        BackendShaderHandler.Instance.axonMap.axonIdxEnd = binaryHandler.ReadFromBinaryFile(pythonPath + "axon_idx_end.dat")
            .ToArray();
        
        BackendShaderHandler.Instance.axonMap.axonSegmentContributions =
            binaryHandler.ReadAxonSegments(pythonPath + "axon_contrib.dat");

        if (VariableManagerScript.Instance.savePremadeConfiguration) {
            VariableManagerScript.Instance.updateConfigurationPath();
            binaryHandler.WriteAxonMap(VariableManagerScript.Instance.configurationPath, BackendShaderHandler.Instance.axonMap); }
    }

  /// <summary>
    /// Pre calculates how much current from each Electrode in BackendResourceManager -> "electrodes" reaches each AxonSegment in
    /// BackendResourceManager -> "axonMap.axonSegmentContributions"
    /// </summary>
    public void SetElectrodeToAxonSegmentGauss()
    {
        int numberElectrodes = VariableManagerScript.Instance.numberYelectrodes *
                               VariableManagerScript.Instance.numberXelectrodes;

        long numberElectrodesToSegments =
            (long) BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * (long) numberElectrodes;

        computeShader = UnityEngine.Resources.Load<ComputeShader>("ComputeShaders"+Path.DirectorySeparatorChar+"AxonElectrodeGauss");
        
        ErrorDebug.Log(BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length + "*" + numberElectrodes); 
        int kernel = VariableManagerScript.Instance.debugMode ? computeShader.FindKernel("calculateGaussDebug") : computeShader.FindKernel("calculateGauss");

        if (numberElectrodesToSegments == 0 || numberElectrodesToSegments * 4 > 2147483648){
            ErrorDebug.Log(
                "Not computationally feasible.  Please raise downscaling factor or lower number of electrodes");
            VariableManagerScript.Instance.runShaders = false;
        } else {
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
            if (VariableManagerScript.Instance.debugMode) {
                debugInfoBuf = new ComputeBuffer(
                    BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes,
                    System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)), ComputeBufferType.Default);
                computeShader.SetBuffer(kernel, "DebugInfo", debugInfoBuf); }

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

            if (VariableManagerScript.Instance.savePremadeConfiguration) {
                if ((long) BackendShaderHandler.Instance.electrodes.Length * BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length >
                    Int32.MaxValue || BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length * numberElectrodes == 0)
                {
                    ErrorDebug.Log(
                        "Not computationally feasible.  Please raise downscaling factor or lower number of electrodes"); }
                else {
                    float[] electrodeGauss = new float[BackendShaderHandler.Instance.electrodes.Length *
                                                       BackendShaderHandler.Instance.axonMap
                                                           .axonSegmentContributions
                                                           .Length];
                    BackendShaderHandler.Instance.axonSegmentGaussToElectrodes.GetData(electrodeGauss);
                    
                    VariableManagerScript.Instance.updateConfigurationPath();
                    binaryHandler.WriteFloatArray(
                        VariableManagerScript.Instance.configurationPath + "_axonElectrodeGauss", electrodeGauss
                    ); } }

            ErrorDebug.Log("Finished Calculating Electrode Distances"); } }
  
  
    /// <summary>
    /// Converts all AxonSegment in axonMap of BackendResourceManager to screen positions,
    /// should be ran prior to using AxonSegment locations inside a shader.  
    /// </summary>
    public void AxonSegmentsToScreenPosCoords() {
        UnitConverter unitConverter = new UnitConverter();
        
        for (int i = 0; i < BackendShaderHandler.Instance.axonMap.axonSegmentContributions.Length; i++) {
            BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i].xPosition =
                unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i]
                    .xPosition);
            
            BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i].yPosition =
                unitConverter.micronToScreenPos(BackendShaderHandler.Instance.axonMap.axonSegmentContributions[i]
                    .yPosition); } }
}
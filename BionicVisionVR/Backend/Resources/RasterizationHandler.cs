using System;
using BionicVisionVR.Backend.Resources;
using BionicVisionVR.Structs;
using UnityEngine;

/// <summary>
/// [Singleton] Handles electrode grouping for rasterized electrode activation
/// Attach to an object that holds settings
/// Uses BackendShaderHandler 'bs'
/// Uses VariableManagerScript 'vm';
/// Contains:
///     (SetRasterType(RasterType)
///     SetLineGroups()
///     SetRadialGroups()
///     SetRandomGroups()
///     SetCheckerGroups()
///     PrintRasterGroups())
/// On Awake:
///     (Initializes this [Singleton] or destroys new Instance)
/// On Start:
///     (Initializes Abbreviated Singletons
///     Initializes RasterGroups to avoid null reference)
/// On Update:
///     (N/A)
/// </summary>
public class RasterizationHandler : MonoBehaviour
{
    public enum RasterType
    {
        VerticalLineRaster,
        HorizontalLineRaster,
        RandomRaster,
        CheckerboardRaster,
        RadialRaster,
        None
    }

    [Header("Settings")]
    public RasterType currentRasterType = RasterType.None; 
    public int rasterizeGroups = 6;
    public int rasterTiming = 6;

    [Header("Public Access Variables (Do not change)")]
    public Electrode[][] rasterizedGroups;
    
    private BackendShaderHandler bs;
    private VariableManagerScript vm;

    /// <summary>
    /// Sets the raster type and calls SetRasterGroups if the given RasterType is different from the old one
    /// </summary>
    /// <param name="newType">RasterType to change to</param>
    public void SetRasterType(RasterType newType) {
        if (currentRasterType != newType) {
            currentRasterType = newType;
            SetRasterGroups(); } }
    
    /// <summary>
    /// Calls the correct Set[RasterType]Group() function based off of what the currentRasterType is
    /// </summary>
    public void SetRasterGroups() {
        if (!(bs.electrodes==null) && bs.electrodes.Length % rasterizeGroups != 0) {
            Debug.Log("*** ERROR, ELECTRODES MUST DIVIDE EVENLY INTO NUMBER OF RASTER GROUPS");
            currentRasterType = RasterType.None; }
        else {
            Debug.Log("Rasterize setup");
            if (currentRasterType == RasterType.VerticalLineRaster || currentRasterType == RasterType.HorizontalLineRaster)
                SetLineGroups();
            else if (currentRasterType == RasterType.RandomRaster)
                SetRandomGroups();
            else if (currentRasterType == RasterType.RadialRaster)
                SetRadialGroups();
            else if (currentRasterType == RasterType.CheckerboardRaster)
                SetCheckerGroups(); } }

    /// <summary>
    /// If the current RasterType == VerticalLineRaster
    ///     - Fills rasterizedGroups in order of right to left, then top to bottom
	///			with the values from bs.electrodes
    /// If the current RasterType == HorizeontalLineRaster
    ///     - Fills rasterizedGroups in order of top to bottom, then right to left
	///			with the values from bs.electrodes
    /// </summary>
    private void SetLineGroups() {
        Electrode[] electrodes = bs.electrodes; 

        if (currentRasterType == RasterType.VerticalLineRaster) { 
            ErrorDebug.Log("Creating vertical raster groups");
            for (int i = 0; i < rasterizeGroups; i++) {
                rasterizedGroups[i] = new Electrode[electrodes.Length / rasterizeGroups];
                Electrode[] subset = new Electrode[electrodes.Length / rasterizeGroups];
                Array.Copy(electrodes, i * electrodes.Length / rasterizeGroups,
                    subset, 0, electrodes.Length / rasterizeGroups);
                rasterizedGroups[i] = (Electrode[]) subset.Clone();
                ErrorDebug.Log("Group #" + i);
                foreach (var e in rasterizedGroups[i]) {
                    ErrorDebug.Log(e.ToString()); } } }
        
        else if (currentRasterType == RasterType.HorizontalLineRaster) {
            Debug.Log("Creating horizontal raster groups");
            for (int j = 0; j < rasterizeGroups; j++) 
            {
                ErrorDebug.Log("***  " + j);
                Electrode[] subset = new Electrode[electrodes.Length / rasterizeGroups]; 
                for (int i = 0; i < subset.Length; i++)
                {
                    subset[i] = electrodes[i * rasterizeGroups + j];
                    ErrorDebug.Log(electrodes[i * rasterizeGroups + j]); }

                rasterizedGroups[j] = (Electrode[]) subset.Clone();; } } }
    
    /// <summary>
    /// - Randomly places electrodes from bs.electrodes into rasterizedGroups
    /// </summary>
    private void SetRandomGroups(){
        Debug.Log("Creating randomized raster groups");
        
        int[] randomOrder = new int[bs.electrodes.Length];
        for (int i = 0; i < bs.electrodes.Length; i++)
            randomOrder[i] = (int)UnityEngine.Random.Range(0, 100f);
        
        Electrode[] tempArray = bs.electrodes;
        Array.Sort(randomOrder, tempArray);
        
        for (int i = 0; i < rasterizeGroups; i++) {
            Electrode[] subset = new Electrode[tempArray.Length / rasterizeGroups];
            for (int j = 0; j < subset.Length; j++) {
                subset[j] = tempArray[i * rasterizeGroups + j]; }

            rasterizedGroups[i] = (Electrode[]) subset.Clone(); } }

    /// <summary>
	/// TODO figure out what this does
    /// </summary>
    private void SetRadialGroups() {
        Electrode[] electrodes = bs.electrodes;
        int currentElectrode = electrodes.Length % 2 == 0 ?
            (electrodes.Length + VariableManagerScript.Instance.numberXelectrodes) / 2 
            : (electrodes.Length-1)/2;
        
        int currentGroup = 0;
        int currentStep = 1;
        Electrode[] subset = new Electrode[electrodes.Length / rasterizeGroups];
        subset[0] = electrodes[currentElectrode]; 
        int subset_pos = 1;
        int direction = -1; 
        while (currentGroup < rasterizeGroups) {
            for (int i = 0; i < currentStep; i++) {

                currentElectrode += direction;
                if (currentElectrode >= 0 & currentElectrode < electrodes.Length) {
                    subset[subset_pos] = electrodes[currentElectrode];
                    subset_pos++; }

                if (subset_pos == subset.Length) {
                    rasterizedGroups[currentGroup] = (Electrode[]) subset.Clone(); 
                    subset_pos = 0;
                    currentGroup++; } }
            
            if (currentGroup > rasterizeGroups)
                break; 

            for (int i = 0; i < currentStep; i++) {
                currentElectrode += direction * VariableManagerScript.Instance.numberXelectrodes;

                if (currentElectrode >= 0 & currentElectrode < electrodes.Length) {
                    subset[subset_pos] = electrodes[currentElectrode];
                    subset_pos++; }

                if (subset_pos == subset.Length) {
                    rasterizedGroups[currentGroup] = (Electrode[]) subset.Clone();
                    subset_pos = 0;
                    currentGroup++; } }
            
            direction = -1 * direction;
            currentStep++; }

        PrintRasterGroups(); }
    
    /// <summary>
    /// TODO
    /// </summary>
    private void SetCheckerGroups() {
        if (vm.numberXelectrodes % rasterizeGroups != 0) {
            Debug.Log("**** ERROR - Checkboard requires each row to be divisible by number of raster groups");
            currentRasterType = RasterType.None; }
        else {
            Debug.Log("Setting Checkerboard Raster Groups");
            for (int i = 0; i < rasterizeGroups; i++) {
                int currElectrode = 0;
                Electrode[] electrodes = bs.electrodes;
                Electrode[] subset = new Electrode[electrodes.Length / rasterizeGroups];
                int row = 0;
                int count = 0;
                
                while (row < vm.numberYelectrodes) {
                    for(int j=0; j<(vm.numberXelectrodes / rasterizeGroups); j++) {
                        currElectrode = (row * vm.numberXelectrodes) + i + (rasterizeGroups * j);
                        
                        if (row % 2 == 0) {
                            subset[count] = electrodes[currElectrode];
                            count += 1; }
                        else{

                            currElectrode = currElectrode + (int) Math.Ceiling(rasterizeGroups / 2.0);

                            currElectrode = currElectrode >= ((row+1)* vm.numberXelectrodes)?
                                 currElectrode - vm.numberXelectrodes : currElectrode; 
                            
                            subset[count] = electrodes[currElectrode];
                            count += 1; } 
                    }
                    row++; }
                rasterizedGroups[i] = (Electrode[]) subset.Clone(); } } }

    /// <summary>
    /// Prints the electrodes in the rasterizedGroups
    /// </summary>
    /// <returns></returns>
    public string PrintRasterGroups() {
        string all_groups = currentRasterType.ToString(); 
        
        foreach (var grp in rasterizedGroups) {
            string elecs = "";
            foreach (var elec in grp) 
                elecs += elec.electrodeNumber.ToString() + ", ";
            
            all_groups += "\n" + elecs; }
        
        if (vm.debugMode)
            print(all_groups);

        return all_groups; }

    
    /// *** Singleton information updated at startup ***
    private void Start() { bs = BackendShaderHandler.Instance; vm = VariableManagerScript.Instance; rasterizedGroups = new Electrode[rasterizeGroups][]; }
    public static RasterizationHandler Instance { get; private set; }
    private void Awake() {  if ( Instance == null) {Instance = this; DontDestroyOnLoad(gameObject);} else Destroy(gameObject);  }
}

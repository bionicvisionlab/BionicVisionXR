using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BionicVisionVR.Backend.Resources;
using UnityEditor.UIElements;

/// <summary>
/// [Singleton] Keeps track of collisions, follows VR camera but is unaffected by rotations
/// Attach to an object with a rigid body collider
/// Contains: 
///     SetIgnoreList(string[], bool)
/// On Awake:
///     Initializes this [Singleton] or destroys new Instance
/// On Start:
///     N/A
/// On Update:
///     Moves to VR camera location
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    public List<String> collidedTags;
    [SerializeField] private String[] tagsToIgnore;
    private string[] ignoreList = new string[]{}; 

    /// <summary>
    /// Sets a list of objects to ignore when detecting collisions
    /// </summary>
    /// <param name="ignoreList">Contains the names of all ignored objects</param> 
    /// <param name="overwriteOld">? Overwrites old list  :  Appends to List</param> 
    public void SetIgnore(string[] ignoreList, bool overwriteOld=true) {
        if (overwriteOld)
            this.ignoreList = ignoreList;
        else {
            string[] tempList = new string[this.ignoreList.Length + ignoreList.Length]; 
            this.ignoreList.CopyTo(tempList, 0);
            ignoreList.CopyTo(tempList, this.ignoreList.Length);
            this.ignoreList = (string[]) tempList.Clone(); } }
    
    void OnTriggerEnter(Collider other) {
        if(!ignoreList.Contains(other.name)){
            SoundHandler.Instance.Beep();
            ErrorDebug.Log("** " + other.name);
            collidedTags.Add(other.gameObject.tag);
        } }

    void OnTriggerExit(Collider other) {
        ErrorDebug.Log("** " + other.name);
        collidedTags.Remove(other.gameObject.tag);
    }

    void Update() {
        transform.position = (BackendShaderHandler.Instance.vrCamera.transform.position); }
    
    /// *** Singleton information updated at startup ***
    public static CollisionHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        else 
            Destroy(gameObject);
        collidedTags = new List<string>();
    } 
}

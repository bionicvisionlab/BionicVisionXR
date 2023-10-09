using UnityEngine;
using UnityEngine.UI;

namespace BionicVisionVR.Backend.Resources
{
    /// <summary>
    /// Logs an object to console if debugMode is true
    /// TODO attach location
    /// Contains: 
    ///     Log<T>(T toLog)
    /// On Awake:
    ///     N/A
    /// On Start:
    ///     N/A
    /// On Update:
    ///     N/A
    /// </summary>
    public class ErrorDebug: MonoBehaviour {
        
        /// <summary>
        /// Debug.Logs the .ToString() value of the given object
        /// </summary>
        /// <param name="toLog">Object to log</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void Log<T>(T toLog)
        {
            if(VariableManagerScript.Instance.debugMode)
                Debug.Log(toLog.ToString()); 
        }
    }
}
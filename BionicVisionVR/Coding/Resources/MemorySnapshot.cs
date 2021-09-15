using System.IO;
using UnityEngine; 
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Profiling.Memory.Experimental;
namespace BionicVisionVR.Resources
{
    public class MemorySnapshot
    {
        public void TakeSnapshot(string name = "snapshot")
        {
            string path = Application.dataPath + Path.DirectorySeparatorChar +  "MemorySnapshots" + Path.DirectorySeparatorChar ;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); 
            }
            path = Path.Combine(path, name + ".snap"); 
            CaptureFlags captureFlags = CaptureFlags.ManagedObjects
                                        | CaptureFlags.NativeObjects
                                        | CaptureFlags.NativeAllocations
                                        | CaptureFlags.NativeAllocationSites
                                        | CaptureFlags.NativeStackTraces;
            MemoryProfiler.TakeSnapshot(path, (string s, bool b) =>{}, captureFlags);
            Debug.LogFormat("Taking a memory Snapshot and storing it locally at {0}", path);
        }
    }
}
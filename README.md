# BVL-Bionic_vision_VR

The 2022 version has been drastically simplified to allow for non-experts to utilize the simulation software. 
When developing a Unity project, download the contents of this github into the Assets folder. 

The project requires [SteamVR](https://valvesoftware.github.io/steamvr_unity_plugin/articles/Quickstart.html))

If using Tobii eyetracking (HTC Vive Pro Eye), you will need to request and download the Unity SDK through [Tobii](https://developer.tobii.com/pc-gaming/unity-sdk/getting-started/)

To use the bionic vision simulation, simply replace the normal camera with the SPV_prefab.prefab located in the 'BionicVisionVR' folder. Settings can be changed in Unity's inspector under the different scripts attached to the prefab's nested objects. Sometimes Unity fails to import the scripts correctly and will display an error in the console stating "Game Object XXXXX attached script not found". Double clicking the error message will pull the object up in the inspector and tell you the name of the missing script. Search the folders for the missing scripts. Most of the files needed are in BionicVisionVR -> Backend -> Resources

The general organization of the project:

BionicVisionVR: For everything related to the bionic vision simulation

Environment: Physical assets used across all experiments in the environment

Experiments: Contains code to handle block randomization, camera tracking, subject file generations, etc.  See the sample experiment for the latest example. 

Other: Packages that help run the simulation.  For example, there is a first person movement script

![alt text](https://github.com/bionicvisionlab/BionicVisionXR/flowchart.jpeg)

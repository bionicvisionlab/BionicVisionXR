# BVL-Bionic_vision_VR

The 2022 version has been drastically simplified to allow for non-experts to utilize the simulation software. 
When developing a Unity project, download the contents of this github into the Assets folder. 

The project requires [SteamVR](https://valvesoftware.github.io/steamvr_unity_plugin/articles/Quickstart.html))

If using Tobii eyetracking (HTC Vive Pro Eye), you will need to request and download the Unity SDK through [Tobii](https://developer.tobii.com/pc-gaming/unity-sdk/getting-started/)

To use the bionic vision simulation, simply replace the normal camera with the SPV_prefab.prefab located in the 'BionicVisionVR' folder. Settings can be changed in Unity's inspector under the different scripts attached to the prefab's nested objects. Sometimes Unity fails to import the scripts correctly and will display an error in the console stating "Game Object XXXXX attached script not found". Double clicking the error message will pull the object up in the inspector and tell you the name of the missing script. Search the folders for the missing scripts. Most of the files needed are in BionicVisionVR -> Backend -> Resources

##The general organization of the project:

BionicVisionVR: For everything related to the bionic vision simulation

Environment: Physical assets used across all experiments in the environment

Experiments: Contains code to handle block randomization, camera tracking, subject file generations, etc.  See the sample experiment for the latest example. 

Other: Packages that help run the simulation.  For example, there is a first person movement script

##Backend
![Simulation Flowchart](https://github.com/bionicvisionlab/BionicVisionXR/blob/master/Flowchart.jpeg)
The majority of the simulation is handled by shaders which are called in the 'BackendHandler.cs' file.  This file is attached to the SPV_Camera and shader's are attached to the script as materials of the .shader file. 

The simulation works by precomputing every electrode->axon segment interaction and computing how much each axon segment contributes to each pixel. Every axon segment that contributes above a certain threshold for a certain pixel is stored in the axonSegmentContributionBuffer (Axon Segment/Pixel Buffer). The start and end locations of this list are stored in separate buffers. For example, pixel #0 might have 10 axon segments that contribute more than the threshold value. In this scenario AxonSegmentStartBuffer[0]=0 and AxonSegmentEndBuffer[0]=9. When calculating the brightness at pixel #0, the shader would go through the first ten locations (0-9) and would find how much each electrode contributes to that specific pixel. This setup allows for extremely fast real time processing as most of the calculations are performed at the start of the simulation. The axon map shader is easily replaced with any model that has a mapping of each cell's (or segment's) contribution to a specific pixel, and each electrode's contribution to each cell.  

The simulation also has built in logic to handle temporal effects (such as phosphene persistence and fading), along with eye tracking. The eye tracking supports the virtual camera updating the field of view to where the person is looking while also updating their perceived field of view (as in Pixium Prima, Polyretina, etc), or can be set to update where the gaze is being perceived, but not what the camera is looking at (as in any head mounted camera device).  
The entire simulation is a work in progress, please feel free to email me at justin_kasowski@ucsb.edu, or submit a bug report through github. 

##Citations 
To cite the simulation, please use [doi.org/10.1145/3519391.3522752](https://doi.org/10.1145/3519391.3522752)
To cite the axon model, please use [doi.org/10.1038/s41598-019-45416-4](https://doi.org/10.1038/s41598-019-45416-4)

##Acknowledgements (In chronological order)
Michael Beyeler (mentorship in software design and the original axon model equations)
Nathan Wu (helping implement the initial shader logic and providing initial support with real time computer vision algorithms)
Ethan Gao (converting the axon map and electrode gaussian equations to compute shaders)
Versha Rohatgi (work on determining VR FOV and allowing for any pulse2percept device to be simulated) 
Rucha Kolhatkar (work on the initial demo's GUI)
Robert Gee (creating docstrings and continuing to develop real time computer vision algorithms)
Anand Giduthuri (converting 3d models and continuing to develop real time computer vision algorithms)

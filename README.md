# BVL-Bionic_vision_VR

Before downloading this repository, start a new Unity project with Unity version "2021.1.12f1". It is recommended to use Unity Hub (https://unity3d.com/get-unity/download) to manage your Unity projects.  To get version "2021.1.12f1", click on the "Installs" tab of Unity Hub, click "Add", and add version "2021.1.12f1". On the following window, add Android, Mac, and Linux support options. Once this is installed (~5-10 minutes), navigate back to the "Projects" tab of Unity Hub and click "New".  In the following window, click the "3D" template, choose a project name and a project directory.  Click create. 

For ease of editting/version control, it is recommended to use Jetbrain's Rider (https://www.jetbrains.com/rider/) integration. After installing Rider, click "Get from Version Control".  In the URL field, copy and paste this Github Repository's address (https://github.com/bionicvisionlab/BionicVisionXR) and for the directory, select the "Assets" folder of the new Unity project created previously. This will clone the repository into the Unity Project's Assets where it will be ready to use, while simultaneously being ready to pull/push from Github using Rider.  To directly link the Rider IDE to your Unity project, follow the guide at "https://www.jetbrains.com/help/rider/Unity.html". This allows for easier debugging and automatically opening files in Rider.  

Alternatively, the repository can be downloaded straight from Github and moved into the Unity project's Assets folder to be used without IDE integration. Once you've loaded the project, look for any compile errors.  Most likely these will be from missing packages that will need downloaded onto your computer. Most of these assets can be located by searching the Asset Store for the name of the asset.   

*** When committing changes, DO NOT add any unversioned files without getting permission first ***


The general project organization is as follows:

BionicVisionVR: For everything related to the bionic vision simulation.  The main requirements of the simulation are the VariableManagerScript and the BackendShaderHandler.  The VariableManagerScript is a singleton that persists between scenes and keeps track of various simulation parameters. 

Environment: Physical assets used across all experiments in the environment

Experiments: Contains code to handle block randomization, camera tracking, subject file generations, etc.  See the sample experiment for the latest example. 

Other: Packages required to run the simulation.  For example, the first person movement script or VR integration packages. 

Every individual experiment should have its own folder that contains everything required for that experiment. If there are multiple experiments that use the same script (for example, the script that turns the lights on/off), that script should be moved to the "Other" folder.  If there are multiple experiments using the same physical asset, it should be moved to the Environment folder. 

introScene is a GUI scene that can be used to enter into the different experiments.  Each experiment should have its own scene located in its experiment folder. The "SceneManagement" script (located in the "Other" folder) can be used to load other scenes.  Add a button to the canvas, under the Inspector there is an "On Click" parameter that can be set to SceneManagementScript.GoToNewScene (this function will need to be created for your particular experiment, see the examples in SceneManagementScript). introScene also has a "ScriptManager".  This is where you can put Singletons scripts (like VariableManagerScript) that can be used to set values and carry them into the subsequent scenes.  




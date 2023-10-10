# BionicVisionXR

BionicVisionXR is a virtual and augmented reality toolbox for simulated prosthetic vision (SPV).

The software provides a framework for integrating different computational models with simulations of current visual prostheses to produce simulations of prosthetic vision that respect:
- clinical reports about phosphene appearance (i.e., shape = f(stimulus params), persistence/fading)
- eye movements (i.e., gaze-congruent vs. gaze-incongruent viewing)
- practical limitations (i.e., rastering, as not all electrodes can be turned on at the same time)

The package is most powerful in combination with [pulse2percept](https://github.com/pulse2percept/pulse2percept), the lab's Python-based bionic vision simulator.
The ideal workflow is as follows:

![Figure 1](/bionicvisionxr.png)

To simplify the implementation of research tasks in immersive virtual environments, the package relies on [SimpleXR](https://github.com/simpleOmnia/sXR), 
which allows for simple handling of object interactions, collision detection, keyboard controls, switching between desktop mode and HMD mode, and much more.

If you use this simulator in a publication, please cite:

> J Kasowski & M Beyeler (2022). Immersive Virtual Reality Simulations of Bionic Vision. *AHs '22: Proceedings of the Augmented Humans International Conference 2022*
> 82–93, doi:[10.1145/3519391.3522752](https://doi.org/10.1145/3519391.3522752).


## Getting Started

If you are a bionic vision researcher who is interested in using this project, but would benefit from support, please contact bionicvisionlab@gmail.com.

For the more experienced user:

- The project requires [SteamVR](https://valvesoftware.github.io/steamvr_unity_plugin/articles/Quickstart.html)
- When developing a Unity project, download the contents of this repo into the Assets folder. 
- To use the bionic vision simulation, replace the normal camera with the `SPV_prefab.prefab` located in the "BionicVisionVR" folder.
  The settings can be changed in Unity's inspector under the different scripts attached to the prefab's nested objects.
- The default phosphene model is the axon map model [Beyeler et al., (2019](https://doi.org/10.1038/s41598-019-45416-4).
  The axon map model requires [pulse2percept](https://github.com/pulse2percept/pulse2percept) to be [installed]((https://pulse2percept.readthedocs.io/en/stable/install.html) and working from the command line.
- We recommend [SimpleXR](https://github.com/simpleOmnia/sXR) for eye tracking. Once imported into your project, update the `GetGazeScreenPos()` function in `BackendShaderHandler.cs` (uncomment the code). 


## Troubleshooting

This project should be easy to set up. If it is not, please contact bionicvisionlab@gmail.com.

- **Python/pulse2percept errors**:
  It is recommended to install python and all pip libraries through the command line (Windows) or terminal (Linux/Mac).
  In Windows, this can be done by simply typing `python` in the command line.
  This will automatically open the Windows store with the option to install Python 3.
   Once python is installed, use `python -m pip install XXXXXXX` to install all the required libraries.
  Installing this way will ensure pulse2percept can be called from within Unity.  

- **Import Error**:
  Sometimes Unity fails to import the scripts correctly and will display an error in the console stating "Game Object XXXXX attached script not found".
  Double clicking the error message will pull the object up in the inspector and tell you the name of the missing script. 
  Search the folders for the missing scripts. Most of the files needed are in "BionicVisionVR -> Backend -> Resources"


## How it works

All software was developed using the Unity development platform, consisting of a combination of C\# code processed by the CPU and fragment/compute shaders processed by the GPU.

The general workflow is as follows:

1. **Image acquisition:** Unity's virtual camera captures a 60-degree field of view at 90 frames per second. 
2. **Image processing:** The image is typically downscaled, converted to greyscale, preprocessed, and blurred with a Gaussian kernel.
   Preprocessing includes things like depth or edge detection, contrast enhancement, etc. 
3. **Electrode activation:** Electrode activation is derived directly from the closest pixel to each electrode's location in the visual field.
   The previous blurring is in place to avoid misrepresenting crisp edges, where moving one pixel could result in an entirely different activation value.
   Activation values are only collected for electrodes that are currently active.
4. **Spatial effects:** The electrode activation values are used with a psychophysically validated phosphene model
   to determine the brightness value for each pixel in the current frame.
5. **Temporal effects:** Previous work has demonstrated phosphene fading and persistence across prosthetic technologies.
   Additionally, previous simulations have eluded to the importance of temporal properties in electrode stimulation strategies.
   To simulate these effects, we implemented a charge accumulation and decay model with parameters matching previously reported temporal properties in real devices.
   Information from previous frames is used to adjust the brightness of subsequent frames.
6. **Gaze-contingent rendering:** Gaze contingency (when what you're being shown is congruent with your gaze) has been shown to improve performance on various tasks using real devices.
   The package has the option to access an HMD's eye tracker and present the stimulus as either gaze-congruent or gaze-incongruent.

The majority of the simulation is handled by shaders which are called in the `BackendHandler.cs` file.
This file is attached to the `SPV_Camera`, and shader's are attached to the script as materials of the `.shader` file. 


## Acknowledgments

We thank our research assistants who have contributed to this code base.
In chronological order:
* Nathan Wu (helping implement the initial shader logic and providing initial support with real time computer vision algorithms)
* Ethan Gao (converting the axon map and electrode gaussian equations to compute shaders)
* Versha Rohatgi (work on determining VR FOV and allowing for any pulse2percept device to be simulated) 
* Rucha Kolhatkar (work on the initial demo's GUI)
* Robert Gee (creating docstrings and continuing to develop real time computer vision algorithms)
* Anand Giduthuri (converting 3d models and continuing to develop real time computer vision algorithms)

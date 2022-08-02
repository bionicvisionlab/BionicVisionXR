using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
/// <summary>
/// Manages devices, interactable GUI objects and user input
/// TODO where to attach
/// Uses VariableManagerScript and BackendShaderHandler
/// Contains:
///     void SelectSlider()
///     bool TimeElapsed()
///     void UpdateVariableManager()
///     void GenerateAllFiles()  
/// On Awake:
///     N/A
/// On Start:
///    Initializes values
/// On Update:
///     Manages user input
/// </summary>
public class  NewGUI_Controller : MonoBehaviour
{
    public GameObject gui; 
    
    public Slider device;
    public Slider x_pos;
    public Slider y_pos;
    public Slider rotation;
    public Slider rho;
    public Slider lambda;

    public Slider gazeLock;
    public Slider temporalMode;
    public Slider blurIntensity;
    public Slider zoom;
    public Slider preProcessor;
    public Slider amplitude; 
    

    public Button SPV_active;

    private int[] possibleRho =  {50, 150, 300};
    private int[] possibleLambda = {50, 1500, 3000};
    
    private int currentGuiTargetRight, currentGuiTargetLeft;
    private static int numSlidersRight = 6; 
    private Slider[] GuiTargetsRight = new Slider[numSlidersRight];
    private float[] sliderIntervalsRight = new float[numSlidersRight];  
    private static int numSlidersLeft = 6; 
    private Slider[] GuiTargetsLeft = new Slider[numSlidersLeft];
    private float[] sliderIntervalsLeft = new float[numSlidersLeft];  

    private double timer=0;
    private float interval = 0.25f; 

    // Start is called before the first frame update
    void Start()
    {
        GuiTargetsRight[0] = device;
        sliderIntervalsRight[0] = 1f; 
        
        GuiTargetsRight[1] = x_pos;
        sliderIntervalsRight[1] = 600f; 
        
        GuiTargetsRight[2] = y_pos;
        sliderIntervalsRight[2] = 600f;

        GuiTargetsRight[3] = rotation; 
        sliderIntervalsRight[3] = 45f;
        
        GuiTargetsRight[4] = rho;
        sliderIntervalsRight[4] = 1f; 
        
        GuiTargetsRight[5] = lambda;
        sliderIntervalsRight[5] = 1f; 
        
        GuiTargetsLeft[0] = gazeLock;
        sliderIntervalsLeft[0] = 1f; 
        
        GuiTargetsLeft[1] = temporalMode;
        sliderIntervalsLeft[1] = 1f; 
        
        GuiTargetsLeft[2] = blurIntensity;
        sliderIntervalsLeft[2] = 2f;

        GuiTargetsLeft[3] = zoom; 
        sliderIntervalsLeft[3] = 10f;
        
        GuiTargetsLeft[4] = preProcessor;
        sliderIntervalsLeft[4] = 1f; 
        
        GuiTargetsLeft[5] = amplitude;
        sliderIntervalsLeft[5] = 0.1f; 

        //GenerateAllFiles(); 
        
        UpdateVariableManager();
        SelectSlider(); 
    }
    /// <summary>
    /// Set the correct left and right gui target to interactable, and all others to not interactable
    /// </summary>
    void SelectSlider()
    {
        for (int i=0; i<GuiTargetsRight.Length; i++)
        {
            if (currentGuiTargetRight != i)
                GuiTargetsRight[i].interactable = false;
            else
                GuiTargetsRight[i].interactable = true;
        }
        
        for (int i=0; i<GuiTargetsLeft.Length; i++)
        {
            if (currentGuiTargetLeft != i)
                GuiTargetsLeft[i].interactable = false;
            else
                GuiTargetsLeft[i].interactable = true;
        }
    }
    /// <summary>
    /// Returns true if [interval] time has elapsed
    /// </summary>
    /// <returns></returns>
    bool TimeElapsed()
    {
        //Debug.Log(Time.realtimeSinceStartupAsDouble - timer + ""); 
        if (Time.realtimeSinceStartupAsDouble - timer > interval)
        {
            //Debug.Log("True"); 
            timer = Time.realtimeSinceStartupAsDouble;
            return true; 
        }
        return false; 
    }
    /// <summary>
    /// Updates values in VariableManagerScript based off of the current device
    /// </summary>
    void UpdateVariableManager()
    {
        if (device.value == 0)
        {
            VariableManagerScript.Instance.numberXelectrodes = 10;
            VariableManagerScript.Instance.numberYelectrodes = 6;
            VariableManagerScript.Instance.electrodeSpacing = 575; 
        }
        else if (device.value == 1)
        {
            VariableManagerScript.Instance.numberXelectrodes = 15;
            VariableManagerScript.Instance.numberYelectrodes = 15;
            VariableManagerScript.Instance.electrodeSpacing = 575; 
        }
        else if (device.value == 2)
        {
            VariableManagerScript.Instance.numberXelectrodes = 20;
            VariableManagerScript.Instance.numberYelectrodes = 20;
            VariableManagerScript.Instance.electrodeSpacing = 350; 
        }

        VariableManagerScript.Instance.xPosition = x_pos.value;
        VariableManagerScript.Instance.yPosition = y_pos.value;
        VariableManagerScript.Instance.rotation = rotation.value;
        VariableManagerScript.Instance.rho = possibleRho[(int) rho.value]; 
        VariableManagerScript.Instance.lambda = possibleLambda[(int) lambda.value];
        
        BackendShaderHandler.Instance.UpdateAfterFrames();

        gui.SetActive(false); 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) /*|| SteamVR_Actions._default.TeleportBack.GetStateDown(SteamVR_Input_Sources.RightHand)) */ && currentGuiTargetRight != GuiTargetsRight.Length-1 && TimeElapsed())
            currentGuiTargetRight++;

        if ((Input.GetKeyDown(KeyCode.UpArrow)|| SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.RightHand)) && currentGuiTargetRight != 0 && TimeElapsed())
            currentGuiTargetRight--; 
        
        
        if (Input.GetKeyDown(KeyCode.S) /*|| SteamVR_Actions._default.TeleportBack.GetStateDown(SteamVR_Input_Sources.LeftHand)) */&& currentGuiTargetLeft != GuiTargetsLeft.Length-1 && TimeElapsed())
            currentGuiTargetLeft++;

        if ((Input.GetKeyDown(KeyCode.W)|| SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand)) && currentGuiTargetLeft != 0  && TimeElapsed())
            currentGuiTargetLeft--; 
        
        SelectSlider();

        if ((Input.GetKeyDown(KeyCode.Delete) || SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))  && TimeElapsed())
        {
            SPV_active.interactable = !SPV_active.interactable; 
            SPV_active.GetComponentInChildren<Text>().text = SPV_active.interactable ? "SPV active" : "SPV Inactive";
            VariableManagerScript.Instance.useBionicVisionShader = SPV_active.interactable;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || SteamVR_Actions._default.SnapTurnRight.GetStateDown(SteamVR_Input_Sources.RightHand)) && TimeElapsed())
            GuiTargetsRight[currentGuiTargetRight].value += sliderIntervalsRight[currentGuiTargetRight];

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || SteamVR_Actions._default.SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.RightHand)) && TimeElapsed())
            GuiTargetsRight[currentGuiTargetRight].value -= sliderIntervalsRight[currentGuiTargetRight];
        
        if ((Input.GetKeyDown(KeyCode.D) || SteamVR_Actions._default.SnapTurnRight.GetStateDown(SteamVR_Input_Sources.LeftHand)) && TimeElapsed())
            GuiTargetsLeft[currentGuiTargetLeft].value += sliderIntervalsLeft[currentGuiTargetLeft];

        if ((Input.GetKeyDown(KeyCode.A) || SteamVR_Actions._default.SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.LeftHand)) && TimeElapsed())
            GuiTargetsLeft[currentGuiTargetLeft].value -= sliderIntervalsLeft[currentGuiTargetLeft];

        if ((Input.GetKeyDown(KeyCode.KeypadEnter) ||
            SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand)) && TimeElapsed())
        {
            if(gui.activeSelf)
                UpdateVariableManager();
            else
                gui.SetActive(true);
        }

        if ((Input.GetKeyDown(KeyCode.KeypadPlus) ||
            SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand)) && TimeElapsed())
        {
            if (gui.activeSelf)
                gui.SetActive(false);
            else
                gui.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            VariableManagerScript.Instance.runShaders = !VariableManagerScript.Instance.runShaders; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            SceneManager.LoadScene("ClassicTasks");
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            SceneManager.LoadScene("VideoPlayer"); 
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            SceneManager.LoadScene("ARpassthrough"); 

        VariableManagerScript.Instance.gazeLock = (int) gazeLock.value == 1 ? true : false; 
        VariableManagerScript.Instance.useTemporal = temporalMode.value == 1 ? true : false; 
        VariableManagerScript.Instance.blurIntensity = (int) blurIntensity.value; 
        VariableManagerScript.Instance.zoom = (int) zoom.value; 
        VariableManagerScript.Instance.whichPreprocessor = (int) preProcessor.value; 
        VariableManagerScript.Instance.amplitude = amplitude.value; 
    }
    /// <summary>
    /// Updates GuiTargetsRight values and calls UpdateVariableManager
    /// </summary>
    private void GenerateAllFiles()
    {
        int[] three = {0, 1, 2};
        int[] rots = {-90, -45, 0, 45, 90};
        for(int eye = 0; eye<2; eye++){ //Both eyes
            foreach (var i in three) //devices
            {
                foreach (var j in three) //xPos
                {
                    foreach (var k in three) //yPos
                    {
                        foreach (var r in rots)
                        {
                            foreach (var rh in possibleRho)
                            {
                                foreach (var la in possibleLambda)
                                {
                                    GuiTargetsRight[0].value = i;
                                    GuiTargetsRight[1].value = j;
                                    GuiTargetsRight[2].value = k;
                                    GuiTargetsRight[3].value = r;
                                    GuiTargetsRight[4].value = rh;
                                    GuiTargetsRight[5].value = la;
                                    UpdateVariableManager();
                                }
                            }
                        }
                    }
                }
            }

            VariableManagerScript.Instance.useLeftEye = !VariableManagerScript.Instance.useLeftEye;
        }
    }
}

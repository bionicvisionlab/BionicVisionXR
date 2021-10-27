using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class NewGUI_Controller : MonoBehaviour
{
    public Slider device;
    public Slider x_pos;
    public Slider y_pos;
    public Slider rotation;
    public Slider rho;
    public Slider lambda;
    public Slider amplitude;

    public Button SPV_active;

    private int[] possibleRho =  {50, 100, 300};
    private int[] possibleLambda = {50, 1000, 5000}; 

private int currentGuiTarget;
    private static int numSliders = 7; 
    private Slider[] GuiTargets = new Slider[numSliders];
    private float[] sliderIntervals = new float[numSliders];  

    private double timer;
    private float interval = .25f; 

    // Start is called before the first frame update
    void Start()
    {
        GuiTargets[0] = device;
        sliderIntervals[0] = 1f; 
        
        GuiTargets[1] = x_pos;
        sliderIntervals[1] = 300f; 
        
        GuiTargets[2] = y_pos;
        sliderIntervals[2] = 300f;

        GuiTargets[3] = rotation; 
        sliderIntervals[3] = 45f;
        
        GuiTargets[4] = rho;
        sliderIntervals[4] = 1f; 
        
        GuiTargets[5] = lambda;
        sliderIntervals[5] = 1f; 
        
        GuiTargets[6] = amplitude;
        sliderIntervals[6] = 0.1f;

        UpdateVariableManager();
        SelectSlider(); 
    }

    void SelectSlider()
    {
        for (int i=0; i<GuiTargets.Length; i++)
        {
            if (currentGuiTarget != i)
                GuiTargets[i].interactable = false;
            else
                GuiTargets[i].interactable = true;
        }
    }

    bool TimeElapsed()
    {
        if (Time.realtimeSinceStartupAsDouble - timer > interval)
        {
            timer = Time.realtimeSinceStartupAsDouble;
            return true; 
        }
        return false; 
    }

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
            VariableManagerScript.Instance.electrodeSpacing = 280; 
        }

        VariableManagerScript.Instance.xPosition = x_pos.value;
        VariableManagerScript.Instance.yPosition = y_pos.value;
        VariableManagerScript.Instance.rotation = rotation.value;
        VariableManagerScript.Instance.rho = possibleRho[(int) rho.value]; 
        VariableManagerScript.Instance.lambda = possibleLambda[(int) lambda.value];
        
        BackendShaderHandler.Instance.UpdateConfiguration();
        VariableManagerScript.Instance.amplitude = amplitude.value; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) && TimeElapsed() && currentGuiTarget != GuiTargets.Length-1)
            currentGuiTarget++;

        if (Input.GetKeyDown(KeyCode.UpArrow) && TimeElapsed()  && currentGuiTarget != 0)
            currentGuiTarget--; 
        
        SelectSlider();
        
        if (Input.GetKeyDown(KeyCode.Space) || SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (SPV_active.interactable)
            {
                SPV_active.interactable = false;
                SPV_active.GetComponentInChildren<Text>().text = "SPV Inactive";
            }
            else
            {
                SPV_active.interactable = true;
                SPV_active.GetComponentInChildren<Text>().text = "SPV Active";
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
            GuiTargets[currentGuiTarget].value += sliderIntervals[currentGuiTarget];

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GuiTargets[currentGuiTarget].value -= sliderIntervals[currentGuiTarget];

        if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
            SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            UpdateVariableManager();

    }
}

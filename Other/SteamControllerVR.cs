using System;
using System.Collections;
using System.Collections.Generic;
using BionicVisionVR.Coding.Resources;
using UnityEngine;
using Valve.VR;

public class SteamControllerVR : MonoBehaviour
{
    public SteamVR_Action_Boolean ampAdjustUp, ampAdjustDown;
    public SteamVR_Action_Boolean TriggerOnOff;
    public SteamVR_Action_Boolean sideButton;
    public SteamVR_Input_Sources handType;

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        TaskHandler.Instance.triggerPressed = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("“Trigger is down”");
        TaskHandler.Instance.triggerPressed = true;
    }

    public void TrackPadUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (TaskHandler.Instance.phase < 4)
            VariableManagerScript.Instance.amplitude += .1f;
    }

    public void TrackPadDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (TaskHandler.Instance.phase < 4)
            VariableManagerScript.Instance.amplitude -= .1f;
    }

    public void TemporalReset(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        BackendShaderHandler.Instance.resetTemporalValues = true; }

    void Update() {
        if (TaskHandler.Instance.phase < 4) {
            if (ampAdjustDown.GetState(SteamVR_Input_Sources.Any))
                VariableManagerScript.Instance.amplitude -= .1f;
            else if (ampAdjustUp.GetState(SteamVR_Input_Sources.Any))
                VariableManagerScript.Instance.amplitude += .1f; } } 
    void Start() {
        TriggerOnOff.AddOnStateDownListener(TriggerDown, handType);
        TriggerOnOff.AddOnStateUpListener(TriggerUp, handType);
        ampAdjustUp.AddOnStateDownListener(TrackPadUp, handType);
        ampAdjustDown.AddOnStateDownListener(TrackPadDown, handType); 
        sideButton.AddOnStateDownListener(TemporalReset, handType);
    }
    
    public static SteamControllerVR Instance { get; private set; }
    private void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }else { Destroy(gameObject); } }
}
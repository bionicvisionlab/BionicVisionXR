using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class NewGUI_Controller : MonoBehaviour
{
    
    public Button SPV_active; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public InputField numberXelectrodes;
    public InputField numberYelectrodes;
    public InputField fov;
    public InputField xPos;
    public InputField yPos;
    public InputField rotation; 

    public void setget()
    {
        VariableManagerScript.Instance.numberYelectrodes = int.Parse(numberXelectrodes.text);
        VariableManagerScript.Instance.numberXelectrodes = int.Parse(numberYelectrodes.text);
        VariableManagerScript.Instance.implant_fov = int.Parse(fov.text);
        VariableManagerScript.Instance.xPosition = int.Parse(xPos.text);
        VariableManagerScript.Instance.yPosition = int.Parse(yPos.text);
        VariableManagerScript.Instance.rotation = int.Parse(rotation.text);

    }
}

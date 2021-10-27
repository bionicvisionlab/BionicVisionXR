using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BionicVisionVR.Resources;
using BionicVisionVR.Structs;

public class GUIManager : MonoBehaviour
{
    // public GameObject SingletonManager;

    private enum DeviceEnum
    {
        DEFAULT,
        PRIMA,
        ARGUS_2,
        ALPHA_AMS,
        SQUARE
    }

    private DeviceEnum deviceName;

    public Text xElectrodeCount;
    public Text yElectrodeCount;
    public Text electrodeSpacingValue;
    public Text amplitudeValue;

    public GameObject calculateButton;

    public GameObject[] uiComponents;
    public GameObject[] defaultUIComponents;
    public GameObject[] squareUIComponents;

    private int[] rhoValues = { 100, 350 };
    private int[] lambdaValues = { 50, 300, 1000 };
    private int[] positionX = { 0, -300, -300, 300, 300 };
    private int[] positionY = { 0, 300, -300, -300, 300 };
    private int[] rotationValues = { 0, 20, 45, 60 };

    private int[] electrodesArray = { 10, 20, 30 };
    private int[] spacingArray = { 250, 350 };

    private int rhoIndex = 0;
    private int lambdaIndex = 0;
    private int positionIndex = 0;
    private int rotationIndex = 0;

    private int electrodeIndex = 0;
    private int spacingIndex = 0;

    private void Start()
    {
        ChangeDevice(0);
    }

    private void ChangeUIVisibility(bool isVisible, GameObject[] components)
    {
        if (isVisible)
        {
            foreach (GameObject i in components)
            {
                i.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject i in components)
            {
                i.SetActive(false);
            }
        }
    }

    public void ChangeDevice(int device)
    {
        if (device == 0)
        {
            deviceName = DeviceEnum.DEFAULT;
            ChangeConfiguration(DeviceEnum.DEFAULT);
        }
        else if (device == 1)
        {
            deviceName = DeviceEnum.PRIMA;
            ChangeConfiguration(DeviceEnum.PRIMA);
        }
        else if (device == 2)
        {
            deviceName = DeviceEnum.ARGUS_2;
            ChangeConfiguration(DeviceEnum.ARGUS_2);
        }
        else if (device == 3)
        {
            deviceName = DeviceEnum.ALPHA_AMS;
            ChangeConfiguration(DeviceEnum.ALPHA_AMS);
        }
        else
        {
            deviceName = DeviceEnum.SQUARE;
            ChangeConfiguration(DeviceEnum.SQUARE);
        }
    }

    private void ChangeConfiguration(DeviceEnum deviceChoice)
    {
        Debug.Log("Device: " + deviceChoice + 
            ", Rho: " + rhoValues[rhoIndex] +
            ", Lambda: " + lambdaValues[lambdaIndex] +
            ", Position: (" + positionX[positionIndex] + ", " + positionY[positionIndex] +
            "), Rotation: " + rotationValues[rotationIndex]);

        // Set to LetterTask1 Configuration for each setting
        if (rhoIndex == 0)
        {
            if (lambdaIndex == 0)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch(deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;
                                
                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
            else if (lambdaIndex == 1)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
            else if (lambdaIndex == 2)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;


                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
        }
        else if (rhoIndex == 1)
        {
            if (lambdaIndex == 0)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
            else if (lambdaIndex == 1)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
            else if (lambdaIndex == 2)
            {
                if (positionIndex == 0)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 1)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 2)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;


                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 3)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
                else if (positionIndex == 4)
                {
                    if (rotationIndex == 0)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 1)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 2)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                    else if (rotationIndex == 3)
                    {
                        switch (deviceChoice)
                        {
                            case DeviceEnum.PRIMA:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask1;

                                break;
                            case DeviceEnum.ARGUS_2:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask2;

                                break;
                            case DeviceEnum.ALPHA_AMS:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(false, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask3;

                                break;
                            case DeviceEnum.SQUARE:
                                ChangeUIVisibility(false, defaultUIComponents);
                                ChangeUIVisibility(true, squareUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask4;

                                break;
                            default:
                                ChangeUIVisibility(false, squareUIComponents);
                                ChangeUIVisibility(true, defaultUIComponents);

                                VariableManagerScript.Instance.predefinedSettings = PreDefinedBlocks.LetterTask5;

                                break;
                        }
                    }
                }
            }
        }

        if (deviceChoice == DeviceEnum.DEFAULT)
        {
            calculateButton.SetActive(true);
        }
        else
        {
            calculateButton.SetActive(false);
        }
    }

    public void ChangePosition(bool isRight)
    {
        if (isRight)
        {
            if (positionIndex == 4) positionIndex = 0;
            else positionIndex++;
        }
        else
        {
            if (positionIndex == 0) positionIndex = 4;
            else positionIndex--;
        }

        // Text to change is at index 2, 5, 8, 11 in uiComponents
        Debug.Log("Change Position");
        uiComponents[2].GetComponent<Text>().text = "Position: (" + positionX[positionIndex] + ", " + positionY[positionIndex] + ")";
        Debug.Log(uiComponents[2].GetComponent<Text>().text );
        VariableManagerScript.Instance.xPosition = positionX[positionIndex];
        VariableManagerScript.Instance.yPosition = positionY[positionIndex];
        ChangeConfiguration(deviceName);
    }

    public void ChangeRotation(bool isRight)
    {
        if (isRight)
        {
            if (rotationIndex == 3) rotationIndex = 0;
            else rotationIndex++;
        }
        else
        {
            if (rotationIndex == 0) rotationIndex = 3;
            else rotationIndex--;
        }

        // Text to change is at index 2, 5, 8, 11 in uiComponents
        uiComponents[8].GetComponent<Text>().text = "Rotation: " + rotationValues[rotationIndex];
        VariableManagerScript.Instance.rotation = rotationValues[rotationIndex];
        ChangeConfiguration(deviceName);
    }

    public void ChangeRho(bool isRight)
    {
        if (isRight)
        {
            if (rhoIndex == 1) rhoIndex = 0;
            else rhoIndex++;
        }
        else
        {
            if (rhoIndex == 0) rhoIndex = 1;
            else rhoIndex--;
        }

        // Text to change is at index 2, 5, 8, 11 in uiComponents
        uiComponents[5].GetComponent<Text>().text = "Rho: " + rhoValues[rhoIndex];
        VariableManagerScript.Instance.rho = rhoValues[rhoIndex];
        ChangeConfiguration(deviceName);
    }

    public void ChangeLambda(bool isRight)
    {
        if (isRight)
        {
            if (lambdaIndex == 2) lambdaIndex = 0;
            else lambdaIndex++;
        }
        else
        {
            if (lambdaIndex == 0) lambdaIndex = 2;
            else lambdaIndex--;
        }

        // Text to change is at index 2, 5, 8, 11 in uiComponents
        uiComponents[11].GetComponent<Text>().text = "Lambda: " + lambdaValues[lambdaIndex];
        VariableManagerScript.Instance.lambda = lambdaValues[lambdaIndex];
        ChangeConfiguration(deviceName);
    }

    public void ChangeNumElectrodes(bool isRight)
    {
        if (isRight)
        {
            if (electrodeIndex == 2) electrodeIndex = 0;
            else electrodeIndex++;
        }
        else
        {
            if (electrodeIndex == 0) electrodeIndex = 2;
            else electrodeIndex--;
        }

        squareUIComponents[2].GetComponent<Text>().text = "Dimensions: " + electrodesArray[electrodeIndex] + "x" + electrodesArray[electrodeIndex];
        VariableManagerScript.Instance.numberXelectrodes = electrodesArray[electrodeIndex];
        VariableManagerScript.Instance.numberYelectrodes = electrodesArray[electrodeIndex];
    }

    public void ChangeElectrodeSpacing(bool isRight)
    {
        if (isRight)
        {
            if (spacingIndex == 1) spacingIndex = 0;
            else spacingIndex++;
        }
        else
        {
            if (spacingIndex == 0) spacingIndex = 1;
            else spacingIndex--;
        }

        squareUIComponents[5].GetComponent<Text>().text = "Spacing: " + spacingArray[spacingIndex];
        VariableManagerScript.Instance.electrodeSpacing = spacingArray[spacingIndex];
    }

    public void XElectrodeCountChange(Slider slider)
    {
        xElectrodeCount.text = "# Xelectrodes: " + (int)slider.value;
        VariableManagerScript.Instance.numberXelectrodes = (int)slider.value;
        Debug.Log(slider.value);
    }

    public void YElectrodeCountChange(Slider slider)
    {
        yElectrodeCount.text = "# Yelectrodes: " + (int)slider.value;
        VariableManagerScript.Instance.numberYelectrodes = (int)slider.value;
        Debug.Log(slider.value);
    }

    public void ElectrodeSpacingChange(Slider slider)
    {
        electrodeSpacingValue.text = "Electrode Spacing: " + (int)slider.value;
        VariableManagerScript.Instance.electrodeSpacing = (int)slider.value;
        Debug.Log(slider.value);
    }

    public void AmplitudeChange(Slider slider)
    {
        amplitudeValue.text = "Amplitude: " + (int)slider.value;
        VariableManagerScript.Instance.amplitude = (int)slider.value;
        Debug.Log(slider.value);
    }

    public void CalculateAll()
    {
        VariableManagerScript.Instance.xPosition = positionX[positionIndex];
        VariableManagerScript.Instance.yPosition = positionY[positionIndex];

        VariableManagerScript.Instance.rotation = rotationValues[rotationIndex];

        VariableManagerScript.Instance.rho = rhoValues[rhoIndex];

        VariableManagerScript.Instance.lambda = lambdaValues[lambdaIndex];

        VariableManagerScript.Instance.electrodeSpacing = spacingArray[spacingIndex];
    }
}
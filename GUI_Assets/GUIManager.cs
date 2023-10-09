// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using BionicVisionVR.Resources;
// using BionicVisionVR.Structs;
// /// <summary>
// /// Handles GUI objects' visibility and location on the screen based off of
// /// the device being used and position
// /// Uses VariableManagerScript
// /// TODO where to attach
// /// Contains:
// ///     void ChangeUIVisibility(bool isVisible, GameObject[] components)
// ///     void ChangeDevice(int device)
// ///     void ChangeConfiguration(DeviceEnum deviceChoice)
// ///     void ChangePosition(bool isRight)
// ///     void ChangeRotation(bool isRight)
// ///     void ChangeRho(bool isRight)
// ///     void ChangeLambda(bool isRight)
// ///     void ChangeNumElectrodes(bool isRight)
// ///     void ChangeElectrodeSpacing(bool isRight)
// ///     void XElectrodeCountChange(Slider slider)
// ///     void YElectrodeCountChange(Slider slider)
// ///     void ElectrodeSpacingChange(Slider slider)
// ///     void AmplitudeChange(Slider slider)
// ///     void CalculateAll()
// /// On Awake:
// ///     N/A
// /// On Start:
// ///    Set device being used to DEFAULT
// /// On Update:
// ///     N/A
// /// </summary>
// public class GUIManager : MonoBehaviour
// {
//     // public GameObject SingletonManager;
//
//     private enum DeviceEnum
//     {
//         DEFAULT,
//         PRIMA,
//         ARGUS_2,
//         ALPHA_AMS,
//         SQUARE
//     }
//
//     private DeviceEnum deviceName;
//
//     public Text xElectrodeCount;
//     public Text yElectrodeCount;
//     public Text electrodeSpacingValue;
//     public Text amplitudeValue;
//
//     public GameObject calculateButton;
//
//     public GameObject[] uiComponents;
//     public GameObject[] defaultUIComponents;
//     public GameObject[] squareUIComponents;
//
//     private int[] rhoValues = { 100, 350 };
//     private int[] lambdaValues = { 50, 300, 1000 };
//     private int[] positionX = { 0, -300, -300, 300, 300 };
//     private int[] positionY = { 0, 300, -300, -300, 300 };
//     private int[] rotationValues = { 0, 20, 45, 60 };
//
//     private int[] electrodesArray = { 10, 20, 30 };
//     private int[] spacingArray = { 250, 350 };
//
//     private int rhoIndex = 0;
//     private int lambdaIndex = 0;
//     private int positionIndex = 0;
//     private int rotationIndex = 0;
//
//     private int electrodeIndex = 0;
//     private int spacingIndex = 0;
//     private void Start()
//     {
//         ChangeDevice(0);
//     }
//     /// <summary>
//     /// Calls SetActive(isVisible) on all objects in components
//     /// </summary>
//     /// <param name="isVisible">Value to change visibility to</param>
//     /// <param name="components">Array of components to change visibility of</param>
//     private void ChangeUIVisibility(bool isVisible, GameObject[] components)
//     {
//         if (isVisible)
//         {
//             foreach (GameObject i in components)
//             {
//                 i.SetActive(true);
//             }
//         }
//         else
//         {
//             foreach (GameObject i in components)
//             {
//                 i.SetActive(false);
//             }
//         }
//     }
//     /// <summary>
//     /// Change device used to device
//     /// </summary>
//     /// <param name="device">int value corresponding to device to change to</param>
//     public void ChangeDevice(int device)
//     {
//         if (device == 0)
//         {
//             deviceName = DeviceEnum.DEFAULT;
//             ChangeConfiguration(DeviceEnum.DEFAULT);
//         }
//         else if (device == 1)
//         {
//             deviceName = DeviceEnum.PRIMA;
//             ChangeConfiguration(DeviceEnum.PRIMA);
//         }
//         else if (device == 2)
//         {
//             deviceName = DeviceEnum.ARGUS_2;
//             ChangeConfiguration(DeviceEnum.ARGUS_2);
//         }
//         else if (device == 3)
//         {
//             deviceName = DeviceEnum.ALPHA_AMS;
//             ChangeConfiguration(DeviceEnum.ALPHA_AMS);
//         }
//         else
//         {
//             deviceName = DeviceEnum.SQUARE;
//             ChangeConfiguration(DeviceEnum.SQUARE);
//         }
//     }
//     /// <summary>
//     /// Changes the configuration of the given device based off of
//     /// rhoIndex, lambdaIndex, positionIndex and rotationIndex
//     /// </summary>
//     /// <param name="deviceChoice"></param>
//     private void ChangeConfiguration(DeviceEnum deviceChoice)
//     {
//         Debug.Log("Device: " + deviceChoice + 
//             ", Rho: " + rhoValues[rhoIndex] +
//             ", Lambda: " + lambdaValues[lambdaIndex] +
//             ", Position: (" + positionX[positionIndex] + ", " + positionY[positionIndex] +
//             "), Rotation: " + rotationValues[rotationIndex]);
//
//         // Set to LetterTask1 Configuration for each setting
//         if (rhoIndex == 0)
//         {
//             if (lambdaIndex == 0)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch(deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//                                 
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//             else if (lambdaIndex == 1)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//             else if (lambdaIndex == 2)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//         }
//         else if (rhoIndex == 1)
//         {
//             if (lambdaIndex == 0)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//             else if (lambdaIndex == 1)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//             else if (lambdaIndex == 2)
//             {
//                 if (positionIndex == 0)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 1)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 2)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 3)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//                 else if (positionIndex == 4)
//                 {
//                     if (rotationIndex == 0)
//                     {
//                         // switch (deviceChoice)
//                         // {
//                         //     case DeviceEnum.PRIMA:
//                         //         ChangeUIVisibility(false, squareUIComponents);
//                         //         ChangeUIVisibility(false, defaultUIComponents);
//                         //
//                         //         VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//                         //
//                         //         break;
//                         //     case DeviceEnum.ARGUS_2:
//                         //         ChangeUIVisibility(false, squareUIComponents);
//                         //         ChangeUIVisibility(false, defaultUIComponents);
//                         //
//                         //         VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//                         //
//                         //         break;
//                         //     case DeviceEnum.ALPHA_AMS:
//                         //         ChangeUIVisibility(false, squareUIComponents);
//                         //         ChangeUIVisibility(false, defaultUIComponents);
//                         //
//                         //         VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//                         //
//                         //         break;
//                         //     case DeviceEnum.SQUARE:
//                         //         ChangeUIVisibility(false, defaultUIComponents);
//                         //         ChangeUIVisibility(true, squareUIComponents);
//                         //
//                         //         VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//                         //
//                         //         break;
//                         //     default:
//                         //         ChangeUIVisibility(false, squareUIComponents);
//                         //         ChangeUIVisibility(true, defaultUIComponents);
//                         //
//                         //         VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//                         //
//                         //         break;
//                         // }
//                     }
//                     else if (rotationIndex == 1)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 2)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                     else if (rotationIndex == 3)
//                     {
//                         switch (deviceChoice)
//                         {
//                             case DeviceEnum.PRIMA:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask1;
//
//                                 break;
//                             case DeviceEnum.ARGUS_2:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask2;
//
//                                 break;
//                             case DeviceEnum.ALPHA_AMS:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(false, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask3;
//
//                                 break;
//                             case DeviceEnum.SQUARE:
//                                 ChangeUIVisibility(false, defaultUIComponents);
//                                 ChangeUIVisibility(true, squareUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask4;
//
//                                 break;
//                             default:
//                                 ChangeUIVisibility(false, squareUIComponents);
//                                 ChangeUIVisibility(true, defaultUIComponents);
//
//                                 VariableManagerScript.Instance.predefinedSettings = AxonMapSettings_Enum.LetterTask5;
//
//                                 break;
//                         }
//                     }
//                 }
//             }
//         }
//
//         if (deviceChoice == DeviceEnum.DEFAULT)
//         {
//             calculateButton.SetActive(true);
//         }
//         else
//         {
//             calculateButton.SetActive(false);
//         }
//     }
//     
//     /// <summary>
//     /// Increment position in given direction.
//     /// Update position and configuration.
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangePosition(bool isRight)
//     {
//         if (isRight)
//         {
//             positionIndex++;
//         }
//         else
//         {
//             positionIndex--;
//         }
//         positionIndex %= 5;
//
//         // Text to change is at index 2, 5, 8, 11 in uiComponents
//         Debug.Log("Change Position");
//         uiComponents[2].GetComponent<Text>().text = "Position: (" + positionX[positionIndex] + ", " + positionY[positionIndex] + ")";
//         Debug.Log(uiComponents[2].GetComponent<Text>().text );
//         VariableManagerScript.Instance.xPosition = positionX[positionIndex];
//         VariableManagerScript.Instance.yPosition = positionY[positionIndex];
//         ChangeConfiguration(deviceName);
//     }
//
//     /// <summary>
//     /// Increment rotation in given direction.
//     /// Update rotation and configuration.
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangeRotation(bool isRight)
//     {
//         if (isRight)
//         {
//             rotationIndex++;
//         }
//         else
//         {
//             rotationIndex--;
//         }
//         rotationIndex %= 4;
//
//         // Text to change is at index 2, 5, 8, 11 in uiComponents
//         uiComponents[8].GetComponent<Text>().text = "Rotation: " + rotationValues[rotationIndex];
//         VariableManagerScript.Instance.rotation = rotationValues[rotationIndex];
//         ChangeConfiguration(deviceName);
//     }
//     /// <summary>
//     /// Increments rho based on direction.
//     /// Updates rho and configuration
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangeRho(bool isRight)
//     {
//         if (isRight)
//         {
//             rhoIndex++;
//         }
//         else
//         {
//             rhoIndex--;
//         }
//
//         rhoIndex %= 2;
//
//         // Text to change is at index 2, 5, 8, 11 in uiComponents
//         uiComponents[5].GetComponent<Text>().text = "Rho: " + rhoValues[rhoIndex];
//         VariableManagerScript.Instance.rho = rhoValues[rhoIndex];
//         ChangeConfiguration(deviceName);
//     }
//
//     /// <summary>
//     /// Increments lambda based on direction.
//     /// Updates lambda and configuration
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangeLambda(bool isRight)
//     {
//         if (isRight)
//         {
//             lambdaIndex++;
//         }
//         else
//         {
//             lambdaIndex--;
//         }
//
//         lambdaIndex %= 3;
//
//         // Text to change is at index 2, 5, 8, 11 in uiComponents
//         uiComponents[11].GetComponent<Text>().text = "Lambda: " + lambdaValues[lambdaIndex];
//         VariableManagerScript.Instance.lambda = lambdaValues[lambdaIndex];
//         ChangeConfiguration(deviceName);
//     }
//
//     /// <summary>
//     /// Increments electrodeIndex based on direction.
//     /// Updates num electrodes and configuration
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangeNumElectrodes(bool isRight)
//     {
//         if (isRight)
//         {
//             electrodeIndex++;
//         }
//         else
//         {
//             electrodeIndex--;
//         }
//
//         electrodeIndex %= 3;
//
//         squareUIComponents[2].GetComponent<Text>().text = "Dimensions: " + electrodesArray[electrodeIndex] + "x" + electrodesArray[electrodeIndex];
//         VariableManagerScript.Instance.numberXelectrodes = electrodesArray[electrodeIndex];
//         VariableManagerScript.Instance.numberYelectrodes = electrodesArray[electrodeIndex];
//     }
//     
//     /// <summary>
//     /// Increments spacing based on direction.
//     /// Updates electrode spacing and configuration
//     /// </summary>
//     /// <param name="isRight"></param>
//     public void ChangeElectrodeSpacing(bool isRight)
//     {
//         if (isRight)
//         {
//             spacingIndex++;
//         }
//         else
//         {
//             spacingIndex--;
//         }
//         spacingIndex %= 2;
//
//         squareUIComponents[5].GetComponent<Text>().text = "Spacing: " + spacingArray[spacingIndex];
//         VariableManagerScript.Instance.electrodeSpacing = spacingArray[spacingIndex];
//     }
//     /// <summary>
//     /// Changes number of X electrodes based off of slider value.
//     /// </summary>
//     /// <param name="slider"></param>
//     public void XElectrodeCountChange(Slider slider)
//     {
//         xElectrodeCount.text = "# Xelectrodes: " + (int)slider.value;
//         VariableManagerScript.Instance.numberXelectrodes = (int)slider.value;
//         Debug.Log(slider.value);
//     }
//
//     /// <summary>
//     /// Changes number of Y electrodes based off of slider value.
//     /// </summary>
//     /// <param name="slider"></param>
//     public void YElectrodeCountChange(Slider slider)
//     {
//         yElectrodeCount.text = "# Yelectrodes: " + (int)slider.value;
//         VariableManagerScript.Instance.numberYelectrodes = (int)slider.value;
//         Debug.Log(slider.value);
//     }
//
//     /// <summary>
//     /// Changes electrode spacing based off of slider value.
//     /// </summary>
//     /// <param name="slider"></param>
//     public void ElectrodeSpacingChange(Slider slider)
//     {
//         electrodeSpacingValue.text = "Electrode Spacing: " + (int)slider.value;
//         VariableManagerScript.Instance.electrodeSpacing = (int)slider.value;
//         Debug.Log(slider.value);
//     }
//
//     /// <summary>
//     /// Changes amplitude based off of slider value.
//     /// </summary>
//     /// <param name="slider"></param>
//     public void AmplitudeChange(Slider slider)
//     {
//         amplitudeValue.text = "Amplitude: " + (int)slider.value;
//         VariableManagerScript.Instance.amplitude = (int)slider.value;
//         Debug.Log(slider.value);
//     }
//     /// <summary>
//     /// Updates value of VariableManager's xPosition, yPosition, rotation, rho, lambda and electrodeSpacing
//     /// </summary>
//     public void CalculateAll()
//     {
//         VariableManagerScript.Instance.xPosition = positionX[positionIndex];
//         VariableManagerScript.Instance.yPosition = positionY[positionIndex];
//
//         VariableManagerScript.Instance.rotation = rotationValues[rotationIndex];
//
//         VariableManagerScript.Instance.rho = rhoValues[rhoIndex];
//
//         VariableManagerScript.Instance.lambda = lambdaValues[lambdaIndex];
//
//         VariableManagerScript.Instance.electrodeSpacing = spacingArray[spacingIndex];
//     }
// }

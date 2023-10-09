using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// [Singleton] Keeps track of images on the UI
/// TODO: Attach to ?
/// Contains: 
///     void EnableOnly(Texture2D whichImage)
///     void SetPosition(Position whichPosition, Texture2D image, bool overridePosition=false)
///     void SetPosition(int whichPosition, Texture2D image, bool overridePosition
///     void FlipComponentUI(Texture2D whichImage)
///     void EnableComponentUI(Texture2D whichImage, bool enabled)
///     void EnableComponentUI(Position whichPosition, bool enabled)
///     void DisableAllComponentsUI()
///     bool GetEnabled(Texture2D whichImage)
///     void TheBlackness(bool turnOnBlack)
/// On Awake:
///     Finds all RawImage components in children with it's name being a Position, and sets the corresponding Positions to be the component
///     Initializes singleton
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class UI_Handler : MonoBehaviour
{
    [System.Serializable]
    public struct UiImage
    {
        public UiImage(Texture2D image, Position pos)
        {
            this.image = image;
            this.pos = pos;
        }

        public Texture2D image;
        public Position pos;
    }

    [Tooltip("0 = Fullscreen, 1 = PartialScreenMiddle, 2 = PartialScreenBottomLeft, 3 = PartialScreenBottom, " +
             "4 = PartialScreenBottomRight, 5 = PartialScreenTopLeft, 6 = PartialScreenTop, 7 = PartialScreenTopRight, " +
             "8 = PartialScreenLeft, 9 = PartialScreenRight")]
    [FormerlySerializedAs("UI_overlays")]
    [SerializeField]
    public RawImage[] UiOverlayPositions = new RawImage[10];

    private List<UiImage> uiOverlayImages;
    private RawImage[] components;
    public RawImage pleaseWait, finished, theBlackness;
    public TextMeshProUGUI textbox1, textbox2, textbox3, textbox4, label;
    public TextMeshProUGUI experimenterOutput1, experimenterOutput2, experimenterOutput3, experimenterOutput4;

    public enum Position
    {
        FullScreen,
        PartialScreenMiddle,
        PartialScreenBottomLeft,
        PartialScreenBottom,
        PartialScreenBottomRight,
        PartialScreenTopLeft,
        PartialScreenTop,
        PartialScreenTopRight,
        PartialScreenLeft,
        PartialScreenRight
    }

    /// <summary>
    /// Disables all images in  UI_overlays except for given Texture2D
    /// </summary>
    /// <param name="whichImage">Image to keep enabled</param>
    public void EnableOnly(Texture2D whichImage)
    {
        DisableAllComponentsUI();
        EnableComponentUI(whichImage, true);
    }

    /// <summary>
    /// Sets Texture2D at given screen Position to the given Texture2D
    /// If override is true, it will replace any Texture2D already in the whichPosition
    /// If override is false and there is already a Texture2D at whichPosition, it will not replace the preexisting Texture2D
    /// </summary>
    /// <param name="whichPosition">Position to place image at</param>
    /// <param name="image">Texture2D to place at Position</param>
    /// <param name="overridePosition">Whether or not to override a preexisting Texture2D at whichPosition</param>
    public void SetPosition(Position whichPosition, Texture2D image)
    {
        uiOverlayImages.Add(new UiImage(image, whichPosition));
    }

    /// <summary>
    /// Disables whichImage if it's enabled
    /// Enables whichImage if it's disabled
    /// </summary>
    /// <param name="whichImage">Image to switch enabled of</param>
    public void FlipComponentUI(Texture2D whichImage)
    {
        foreach (var uiImage in UiOverlayPositions)
        {
            if (!uiImage.texture.Equals(null))
                if (uiImage.texture.name == whichImage.name)
                {
                    uiImage.enabled = !uiImage.enabled;
                }
        }
    }

    /// <summary>
    /// Sets given image's enabled value to passed in enabled value
    /// </summary>
    /// <param name="whichImage">Image to set enable value of</param>
    /// <param name="enabled">Whether or not to enable whichImage</param>
    public void EnableComponentUI(Texture2D whichImage, bool enabled)
    {
        if (enabled)
        {
            foreach (UiImage uiImage in uiOverlayImages)
            {
                if (!uiImage.image.Equals(null) && uiImage.image.name == whichImage.name)
                {
                    UiOverlayPositions[(int) uiImage.pos].texture = uiImage.image;
                    UiOverlayPositions[(int) uiImage.pos].enabled = true;
                    break;
                }
            }
        }
        else
        {
            foreach (RawImage img in UiOverlayPositions)
            {
                if (img.texture == whichImage)
                {
                    img.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Sets image at given position's enabled value to passed in enabled value
    /// </summary>
    /// <param name="whichPosition">Position of image to set enable value of</param>
    /// <param name="enabled">Whether or not to enable image at whichPosition</param>
    public void EnableComponentUI(Position whichPosition, bool enabled = true)
    {
        components = gameObject.GetComponentsInChildren<RawImage>();
        foreach (var component in components)
            if (component.name == whichPosition.ToString())
                component.enabled = enabled;
    }

    public void DisableComponentUI(Texture2D whichImage)
    {
        EnableComponentUI(whichImage,false);
    }

/// <summary>
    /// Disables all UI_overlays images
    /// </summary>
    public void DisableAllComponentsUI() {
        foreach (var image in UiOverlayPositions)
            if( image != null && !image.texture.Equals(null))
                    image.enabled = false; }
    /// <summary>
    /// Find out whether or not given image is enabled
    /// Returns false if image is not found on the UI
    /// </summary>
    /// <param name="whichImage">Image to find enabled value of</param>
    /// <returns>Returns true if the image is found and enabled, false otherwise</returns>
    public bool GetEnabled(Texture2D whichImage) {
        foreach (var image in UiOverlayPositions)
            if (!image.texture.Equals(null) && image.texture.name == whichImage.name)
                return image.enabled;

        Debug.Log("*** Searched image does not appear to be on any active UI Component: " +whichImage.name); 
        return false; }
    /// <summary>
    /// Disables all images in UI_overlays, then sets theBlackness' enabled value to turnOnBlack
    /// </summary>
    /// <param name="turnOnBlack">Value to set theBlackness' enabled to</param>
    /// <returns></returns>
    public void TheBlackness(bool turnOnBlack) {
        DisableAllComponentsUI();
        theBlackness.enabled = turnOnBlack; }
    
    public static UI_Handler Instance { get; private set; }
    private void Awake()
    {
        uiOverlayImages = new List<UiImage>();
        components = gameObject.GetComponentsInChildren<RawImage>();
        foreach (var component in components) {
            for (int i = 0; i < Enum.GetNames(typeof(Position)).Length; i++) {
                if(component.name == Enum.GetValues(typeof(Position)).GetValue(i).ToString()) {
                    UiOverlayPositions[i] = component; } } }

        foreach (var rawImage in UiOverlayPositions)
        {
            rawImage.enabled = false;
        }

        if ( Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); } }

}

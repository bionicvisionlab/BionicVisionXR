using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Will play a sound and display a message if any
/// object with "Other/SafetyWall" script attached.
/// Implemented with SafetyMessage(bool enable)
/// </summary>
public class SafetyHandler : MonoBehaviour
{
    [SerializeField] AudioSource emergencySound;
    [SerializeField] RawImage emergencyImage;
    [SerializeField] bool displayEmergency; 
    
    public void SafetyMessage(bool enable)  {  displayEmergency = enable;  }

    void Update() {
        if (displayEmergency) {
            emergencySound.Play();
            emergencyImage.enabled = true; }
        else {
            emergencySound.Stop();
            emergencyImage.enabled = false; }

        displayEmergency = false; 
    }
    
    /// *** Singleton information updated at startup ***
    public static SafetyHandler Instance { get; private set; }
    private void Awake() {  if ( Instance == null) {Instance = this; DontDestroyOnLoad(gameObject);} else Destroy(gameObject);  }
    
}

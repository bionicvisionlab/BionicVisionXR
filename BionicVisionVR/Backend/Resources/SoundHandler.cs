using UnityEngine;

/// <summary>
/// [Singleton] Handles sounds
/// Should be attached to the same object as the audio listener
/// Contains:
///     void Beep()
///     void Buzz()
///     void Ding()
///     void CustomSound(int soundNumber)
/// On Awake:
///     Initializes singleton
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private AudioClip beep, ding, buzz;
    [SerializeField] private AudioClip[] customClips = new AudioClip[10]; 
    /// <summary>
    /// Plays beep sound effect
    /// </summary>
    public void Beep()
        {soundPlayer.PlayOneShot(beep);}
    /// <summary>
    /// Plays buzz sound effect
    /// </summary>
    public void Buzz()
        {soundPlayer.PlayOneShot(buzz);}
    /// <summary>
    /// Plays ding sound effect
    /// </summary>
    public void Ding()
        {soundPlayer.PlayOneShot(ding);}
    /// <summary>
    /// Plays sound effect at given index of customClips array
    /// </summary>
    /// <param name="soundNumber">index of sound effect to play</param>
    /// <returns></returns>
    public void CustomSound(int soundNumber)
        {soundPlayer.PlayOneShot(customClips[soundNumber]);}
    
    public static SoundHandler Instance { get; private set; }
    private void Awake() {  if ( Instance == null) {Instance = this; DontDestroyOnLoad(gameObject);} else Destroy(gameObject);  }
}

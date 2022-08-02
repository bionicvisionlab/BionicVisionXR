using BionicVisionVR.Backend.Resources;
using UnityEngine;

/// <summary>
/// Attach to any object that you would like to trigger the safety message stop
/// </summary>
public class SafetyWall : MonoBehaviour {
    void Update() {
        if (CollisionHandler.Instance.collidedObject == gameObject.name)
            SafetyHandler.Instance.SafetyMessage(true); } }

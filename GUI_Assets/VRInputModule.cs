using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR; 
/// <summary>
/// Handles VR camera input
/// TODO where to attach
/// Contains:
///     void Process()
///     PointerEventData GetData()
///     void ProcessPress(PointerEventData data)
///     void ProcessRelease(PointerEventData data)
/// On Awake:
///     Initializes a new PointerEventData
/// On Start:
///     N/A
/// On Update:
///     N/A
/// </summary>
public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();
        m_Data = new PointerEventData(eventSystem);
    }
    /// <summary>
    /// Resets the PointerEventData, then sets its values and processes it
    /// </summary>
    public override void Process()
    {
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject; 
        
        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        if (m_ClickAction.GetStateDown(m_TargetSource))
            ProcessPress(m_Data);

        if (m_ClickAction.GetStateUp(m_TargetSource))
            ProcessRelease(m_Data); 
    }
    /// <summary>
    /// Returns the PointerEventData
    /// </summary>
    /// <returns></returns>
    public PointerEventData GetData()
    {
        return m_Data;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    private void ProcessPress(PointerEventData data)
    {
        
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    private void ProcessRelease(PointerEventData data)
    {
        
    }
    
}

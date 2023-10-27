using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Test_DestroyCameraMobilityAndReset : MonoBehaviour
{
    public float m_activationTime = 20;

    public XROrigin m_orgine;
    public TrackedPoseDriver m_tracker;
    public Transform m_camera;
    public Transform [] m_others;

    void Start()
    {
        Invoke("Apply", m_activationTime);       
    }

    [ContextMenu("Apply")]
    public void Apply()
    {

        Destroy(m_tracker);
        Destroy(m_orgine);
        m_camera.position = Vector3.zero;
        m_camera.rotation = Quaternion.identity;
    }
}

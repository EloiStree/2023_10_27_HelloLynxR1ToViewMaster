using System;
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
    public Texture2D [] m_backgroundToHave;
    public Material m_targetCybeSkyeboxMaterial;
    public float m_backgroundChangeInSecond = 3;

    void Start()
    {
        Invoke("Apply", m_activationTime);
        if(m_useTextureChanged) StartCoroutine(ChangeBackground());
    }

    int m_index = 0;
    public bool m_useTextureChanged;
    private IEnumerator ChangeBackground()
    {
        while (true) {

            Texture2D t = m_backgroundToHave[m_index];
            m_targetCybeSkyeboxMaterial.SetTexture("_FrontTex", t);
            m_targetCybeSkyeboxMaterial.SetTexture("_BackTex", t);
            m_targetCybeSkyeboxMaterial.SetTexture("_LeftTex", t);
            m_targetCybeSkyeboxMaterial.SetTexture("_RightTex", t);
            m_targetCybeSkyeboxMaterial.SetTexture("_UpTex", t);
            m_targetCybeSkyeboxMaterial.SetTexture("_DownTex", t);
            m_index++;
            if (m_index >= m_backgroundToHave.Length)
                m_index = 0;
            yield return new WaitForSeconds(m_backgroundChangeInSecond);
            yield return new WaitForEndOfFrame();
        }
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

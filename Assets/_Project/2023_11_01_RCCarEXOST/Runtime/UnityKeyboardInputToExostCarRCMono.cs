using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityKeyboardInputToExostCarRCMono : MonoBehaviour
{
    public KeyCode m_leftFront= KeyCode.A;
    public KeyCode m_rightFront = KeyCode.Q;
    public KeyCode m_leftBack = KeyCode.E;
    public KeyCode m_rightBack = KeyCode.D; 
    public UnityEventBool m_leftFrontEvent;
    public UnityEventBool m_rightFrontEvent;
    public UnityEventBool m_leftBackEvent;
    public UnityEventBool m_rightBackEvent;


    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }


    void Update()
    {
        KeyCodeToUnityEvent(m_leftFront, m_leftFrontEvent);
        KeyCodeToUnityEvent(m_rightFront,m_rightFrontEvent);
        KeyCodeToUnityEvent(m_leftBack,  m_leftBackEvent);
        KeyCodeToUnityEvent(m_rightBack, m_rightBackEvent);

    }

    private static void KeyCodeToUnityEvent(KeyCode c, UnityEventBool e)
    {
        if (Input.GetKeyDown(c)) e.Invoke(true);
        if (Input.GetKeyUp(c)) e.Invoke(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisplayForSometime : MonoBehaviour
{
    public OnTurnOnOffEvent m_affected;
    public OnPourcentChangeEvent m_pourcentTurnOn;

    public float m_delay=4f;
    public float m_timeLeft;

  
    public void DisplayWithDefaultTime()
    {
        Display(m_delay);
    }
    public void Display(float time)
    {
        m_timeLeft = time;
        m_affected.Invoke(true);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            DisplayWithDefaultTime();

        if (m_timeLeft > 0f) {
            m_timeLeft -= Time.deltaTime;
            if(m_delay!=0)
                m_pourcentTurnOn.Invoke(m_timeLeft / m_delay);
            if (m_timeLeft <= 0) {
                m_affected.Invoke(false);
                m_timeLeft = 0f;
            }
        }
    }
    [System.Serializable]
    public class OnTurnOnOffEvent : UnityEvent<bool> { }


}
[System.Serializable]
public class OnPourcentChangeEvent : UnityEvent<float> { }
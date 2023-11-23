using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Phobetor_DisplayWhenNearEnough : MonoBehaviour
{

    public Transform m_leftHand;
    public Transform m_rightHand;

    public float m_distance = 0.2f;

    public UnityEvent m_inRange;
    public UnityEvent m_outRange;

    public float m_previousDistance = 0;
    public float m_currentDistance = 0;

    void Update()
    {

        m_previousDistance = m_currentDistance;
        m_currentDistance = Vector3.Distance(m_leftHand.position, m_rightHand.position);

        if (m_currentDistance != m_previousDistance)
        {

            if (m_currentDistance < m_distance && m_previousDistance > m_distance)
            {
                m_inRange.Invoke();
            }
            if (m_currentDistance > m_distance && m_previousDistance < m_distance)
            {
                m_outRange.Invoke();
            }
        }

    }

}

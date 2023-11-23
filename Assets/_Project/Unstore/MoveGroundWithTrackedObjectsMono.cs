using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundWithTrackedObjectsMono : MonoBehaviour
{
    public Transform m_whatToMove;
    public Transform [] m_trackedObjects;
    public float m_additionalHeight = 0.03f;
    

    void Update()
    {
        for (int i = 0; i < m_trackedObjects.Length; i++)
        {
            if (m_trackedObjects[i].position.y < m_whatToMove.position.y)
            {

                Vector3 pos = m_whatToMove.position;
                pos.y = m_trackedObjects[i].position.y - m_additionalHeight;
                m_whatToMove.position = pos;

            }
        }
     
    }
}

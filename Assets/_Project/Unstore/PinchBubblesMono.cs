using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchBubblesMono : MonoBehaviour
{
    public Transform m_indexTip;
    public Transform m_thumbTip;

    public float m_distance=0.03f;

    public Transform m_whereToCreate;
    public GameObject m_prefab;

    public Transform m_parent;
    public float m_previousDistance=0;
    public float m_currentDistance=0;
 
    void Update()
    {

        m_previousDistance = m_currentDistance;
        m_currentDistance = Vector3.Distance(m_indexTip.position, m_thumbTip.position);

        if (m_currentDistance != m_previousDistance) {

            if (m_currentDistance < m_distance && m_previousDistance > m_distance)
            {
                CreatePrefab();
            }
        }
        
    }

    [ContextMenu("Create prefab")]
    private void CreatePrefab()
    {
        GameObject objt = GameObject.Instantiate(m_prefab, m_whereToCreate);
        objt.transform.parent = m_parent;


    }
}

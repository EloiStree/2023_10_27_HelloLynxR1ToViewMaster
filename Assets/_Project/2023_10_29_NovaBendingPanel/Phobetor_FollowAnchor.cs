using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phobetor_FollowAnchor : MonoBehaviour
{
    public Transform m_whatToFollow;
    public Transform m_whatToMove;
    public Vector3 m_rotation = new Vector3(90, 0, 0);
    public float m_adjustmentDepth = 0.1f;

    [ContextMenu("Update")]
    private void Update()
    {
        m_whatToMove.position = m_whatToFollow.position + m_whatToFollow.forward* m_adjustmentDepth;
        m_whatToMove.rotation = m_whatToFollow.rotation * Quaternion.Euler(m_rotation );
    }

    private void Reset()
    {
        m_whatToMove = transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlanetTextureMono : MonoBehaviour
{

    public Renderer m_rendererToAffect;
    public Texture2D[] m_planetsNoName;
    public int m_nextIndex;
    public float m_time = 20;
    // Start is called before the first frame update
    public void Next()
    {
        m_nextIndex++;
        if (m_nextIndex >= m_planetsNoName.Length) {
            m_nextIndex = 0;
        }
        m_rendererToAffect.material.mainTexture = m_planetsNoName[m_nextIndex];
    }

    public void Awake()
    {
        InvokeRepeating("Next", m_time, m_time);
    }

}

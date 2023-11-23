using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeColorByPourcent : MonoBehaviour
{
    public Color m_from;
    public Color m_to;
    public float m_pourcentByClick=0.1f;
    public ChangeOfColorEvent m_onColorChange;

    [Header("Debug")]
    public float m_pourcent= 0.5f;
    public Color m_current;


    private void Start()
    {
        SetColorTo(m_pourcent);
    }

    public void IncreaseColor()
    {
        IncreaseColor(m_pourcentByClick);
    }
    public void IncreaseColor(float value)
    {
        m_pourcent += value;
        SetColorTo(m_pourcent);
    }
    public void DecreaseColor()
    {
        DecreaseColor(m_pourcentByClick);
    }
    public void DecreaseColor(float value)
    {
        m_pourcent -= value;
        SetColorTo(m_pourcent);
    }
    public void SetColorTo(float pourcent)
    {
        m_current = Color.Lerp(m_from, m_to, pourcent);
        m_onColorChange.Invoke(m_current);
    }
    

}
[System.Serializable]
public class ChangeOfColorEvent : UnityEvent<Color> { }
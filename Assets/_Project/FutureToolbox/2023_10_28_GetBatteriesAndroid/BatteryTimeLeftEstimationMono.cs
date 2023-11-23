using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatteryTimeLeftEstimationMono : MonoBehaviour
{
    public float m_frameDelayInSecond=5;


    public float m_batteryState;
    public FloatHistory m_batteryStatePerFrame = new FloatHistory();

    [System.Serializable]
    public class FloatHistory : GenericClampHistory<float> { }



    public float m_totalTime;
    public float m_startState;
    public float m_endState;
    public float m_deltaState;
    public float m_percentPerSecond;
    public float m_percentPerMinute;
    public float m_percentPerHour;

    public StringDebug m_debugStringEvent;

    public bool m_autoRefresh=true;
    [System.Serializable]
    public class StringDebug : UnityEvent<string> { }

    public void SetBatteryState(float state) {
        m_batteryState = state;
    }

    private void Start()
    {
        if(m_autoRefresh)   
            InvokeRepeating("SaveFrame", 0, m_frameDelayInSecond);
    }
    public void SaveFrame() {
        if(m_batteryState>0f)
            m_batteryStatePerFrame.PushIn(m_batteryState);
    }

    void Update()
    {
        m_batteryStatePerFrame.GetHistoryAsArray(out float[] batteriesState);
        if (batteriesState.Length > 4) {
            m_totalTime = batteriesState.Length * m_frameDelayInSecond;
            m_startState = batteriesState[0];
            m_endState = batteriesState[batteriesState.Length-1];
            m_deltaState = m_endState - m_startState;
            m_percentPerSecond = m_deltaState / m_totalTime;
            m_percentPerMinute = 60f * m_percentPerSecond;
            m_percentPerHour = 3600 * m_percentPerSecond;
            m_debugStringEvent.Invoke(string.Format("{0:0.00}  % Per Hour - {1:0.00}  % Per Minute", m_percentPerHour*100f, m_percentPerMinute * 100f));
        }

    }
}

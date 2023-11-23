using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Events;
using System;



public class DeviceAndroidVolumeMono : MonoBehaviour
{
    public bool m_androidInit;
#if UNITY_ANDROID && !UNITY_EDITOR
    AndroidJavaClass unityPlayer;
    AndroidJavaObject context;
    AndroidJavaObject audioManager;

    public void InitAndroid() {
        if (!m_androidInit) { 
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            audioManager = context.Call<AndroidJavaObject>("getSystemService", "audio");
        }
        m_androidInit = true;
    }

    public void RefreshAndroid() {
            InitAndroid();
        float currentVolume = audioManager.Call<int>("getStreamVolume", 3); // AudioManager.STREAM_MUSIC
        float maxVolume = audioManager.Call<int>("getStreamMaxVolume", 3); // AudioManager.STREAM_MUSIC
        m_volumePercent = (float)Math.Round(currentVolume /maxVolume,2);
    }
#endif

    public float m_volumePercent;
    public float checkInterval = 1.0f;
    public UnityEvent onVolumeChanged;
    public UnityEvent onVolumeChangedPlus;
    public UnityEvent onVolumeChangedMinus;


    private void Start()
    {
        Refresh();
        StartCoroutine(CheckVolumeChanges());
    }

    public float m_currentVolume;
    public float m_previousVolume;

    public bool m_firstRefresh = true;
    private IEnumerator CheckVolumeChanges()
    {
        while (true)
        {
            Refresh();
            m_previousVolume = m_currentVolume;
            m_currentVolume = m_volumePercent;

            if (m_firstRefresh) {
                m_firstRefresh = false;
                continue;
            }
            if (m_previousVolume != m_currentVolume)
            {

                onVolumeChanged.Invoke();
                if (m_previousVolume < m_currentVolume)
                {
                    onVolumeChangedPlus.Invoke();

                }
                if (m_previousVolume > m_currentVolume)
                {

                    onVolumeChangedMinus.Invoke();
                }

            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void Refresh()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        RefreshAndroid();
#endif

    }
}

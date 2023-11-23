//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public static class Common
//{

//    private static AndroidJavaObject s_mainActivity = null;
//    private static AndroidJavaObject s_androidAudioManager = null;

//    public static AndroidJavaObject GetAndroidAudioManager()
//    {
//        if (s_androidAudioManager == null)
//        {
//            s_androidAudioManager = GetMainActivity().Call<AndroidJavaObject>("getSystemService", "audio");
//        }
//        return s_androidAudioManager;
//    }

//    public static AndroidJavaObject GetMainActivity()
//    {
//        if (s_mainActivity == null)
//        {
//            var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//            s_mainActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
//        }
//        return s_mainActivity;
//    }

//    public static bool IsRunningOnAndroid()
//    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//        return true;
//#else
//        return false;
//#endif
//    }
//}




//public class VolumeOverride : MonoBehaviour
//{

//    public UnityEvent m_onVolumeUpEvent;
//    public UnityEvent m_onVolumeDownEvent;

//    public bool m_bGetVolumeFromPhone = true;
 

//    public float m_fPrevVolume = -1;
//    public bool m_bShutDown = false;
//    public static VolumeOverride s_instance = null;


//    public static VolumeOverride Get()
//    {
//        return s_instance;
//    }
//    public float GetVolume()
//    {
//        if (m_bGetVolumeFromPhone && Common.IsRunningOnAndroid())
//        {
//            AndroidJavaObject audioManager = Common.GetAndroidAudioManager();
//            return audioManager.Call<int>("getStreamVolume", 3);
//        }
//        else
//        {
//            return AudioListener.volume;
//        }

//    }

//    public void SetVolume(float a_fVolume)
//    {
//        if (m_bGetVolumeFromPhone && Common.IsRunningOnAndroid())
//        {
//            AndroidJavaObject audioManager = Common.GetAndroidAudioManager();
//            audioManager.Call("setStreamVolume", 3, (int)a_fVolume, 0);
//        }
//        else
//        {
//            AudioListener.volume = a_fVolume;
//        }
//    }

//    private void ResetVolume()
//    {
//        SetVolume(m_fPrevVolume);
//    }

//    public float m_startVolume =0.25f;
//    void Start()
//    {
//        s_instance = this;
//        PowerOn();
//        SetVolume(m_startVolume);
//    }

//    void OnVolumeDown()
//    {
//        m_onVolumeDownEvent.Invoke();
//    }

//    void OnVolumeUp()
//    {
//        m_onVolumeUpEvent.Invoke();

//    }

    
    
//    public bool m_keepCurrentVolume=true;
//    //to unmute the script
//    public void PowerOn()
//    {
//        m_bShutDown = false;
//        //get the volume to avoid interpretating previous change (when script was muted) as user input
//        m_fPrevVolume = GetVolume();

//        //if volume is set to max, reduce it -> if not, there will be no detection for volume up
//        if (m_fPrevVolume == GetMaxVolume())
//        {
//            --m_fPrevVolume;
//            SetVolume(m_fPrevVolume);
//        }

//    }

//    //Get max volume phone
//    public float GetMaxVolume()
//    {
//        if (m_bGetVolumeFromPhone && Common.IsRunningOnAndroid())
//        {
//            AndroidJavaObject audioManager = Common.GetAndroidAudioManager();
//            return audioManager.Call<int>("getStreamMaxVolume", 3);
//        }
//        else
//        {
//            return 1;
//        }
//    }

//    //If user want to change volume, he has to mute this script first
//    //else the script will interpret this has a user input and resetvolume
//    public void ShutDown()
//    {
//        m_bShutDown = true;
//    }

//    public float m_currentVolume;
//    public FloatVolumeEvent m_onVolumeChanged;
//    public StringVolumeEvent m_onVolumeChangedDebug;

//    [System.Serializable]
//    public class FloatVolumeEvent : UnityEvent<float>
//    {

//    }
//    [System.Serializable]
//    public class StringVolumeEvent : UnityEvent<string>
//    {

//    }

//    void Update()
//    {
//        //if (m_bShutDown)
//        //    return;

//        float fCurrentVolume = GetVolume();
//        m_currentVolume = fCurrentVolume;
//        float fDiff = fCurrentVolume - m_fPrevVolume;

//        if (fDiff < 0)
//        {
           
//            ResetVolume();
//            OnVolumeDown();
//            ChangeVolumeNotification();
//        }
//        else if (fDiff > 0)
//        {
//            ResetVolume();
//            OnVolumeUp();
//            ChangeVolumeNotification();
//        }
//    }

//    private void ChangeVolumeNotification()
//    {
//        m_onVolumeChanged.Invoke(m_currentVolume);
//        m_onVolumeChangedDebug.Invoke(string.Format("{0:0.00}%", m_currentVolume));
//    }
//}
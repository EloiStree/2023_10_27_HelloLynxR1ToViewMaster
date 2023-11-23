using System;
using UnityEngine;
using UnityEngine.Events;

public class GetAndroidBatteriesMono : MonoBehaviour
{

    //    [SerializeField] IntEvent m_batteryLevel;
    [SerializeField] FloatEvent m_batteryLevelPercent;
    [SerializeField] StringEvent m_batteryLevelPercentAsString;
    [SerializeField] StringEvent m_batteryLevelPercentGeneralAsString;
    //  [SerializeField] IntEvent m_batteryState;
    [SerializeField] StringEvent m_batteryStateLabel;

    public bool m_useAutoRefresh=true;
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
    void Start()
    {
  
    }
    private void Update()
    {
        if (m_useAutoRefresh)
            Refresh();
    }

    private void Refresh()
    {



        float batteryLevel = SystemInfo.batteryLevel;
        float batteryLevelGeneric = SystemInfo.batteryLevel;


#if UNITY_ANDROID && !UNITY_EDITOR
        // Create an AndroidJavaObject for the "Context" using the current Android activity
        AndroidJavaClass contextClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = contextClass.GetStatic<AndroidJavaObject>("currentActivity");

        // Access the BatteryManager service
        AndroidJavaObject batteryManager = context.Call<AndroidJavaObject>("getSystemService", "batterymanager");

        // Get battery level
        batteryLevel = batteryManager.Call<int>("getIntProperty", 4) / 100f; // 4 represents BATTERY_PROPERTY_CAPACITY

        // Get battery status (charging, discharging, etc.)
//        int status = batteryManager.Call<int>("getIntProperty", 2); // 2 represents BATTERY_PROPERTY_STATUS

#endif

        BatteryStatus status = SystemInfo.batteryStatus;
        m_batteryLevelPercent.Invoke(batteryLevel);
        m_batteryLevelPercentAsString.Invoke(status.ToString());
        m_batteryLevelPercentGeneralAsString.Invoke(batteryLevelGeneric + "%");
        m_batteryStateLabel.Invoke(string.Format("> {0}% {2}%  {1}", batteryLevel * 100f, status.ToString(), batteryLevelGeneric));
        index++;
    }
    int index = 0;
}

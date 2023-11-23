using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTrackerTimeLapseMono : MonoBehaviour
{

    public float m_batteryStatePercent = 0;
    

    public BatteryStateDateTimeFrameCollection m_recordCollection;


    public void Start()
    {
        InvokeRepeating("AddFrame", 0, 10);
    }

    public void SetBatteriesState(float percentStateOfBattery) {
        m_batteryStatePercent = percentStateOfBattery;
    }
    public void CleanRecords()
    {
        m_recordCollection.m_frames.Clear();
    }

    public void AddFrame()
    {
        m_recordCollection.m_frames.Add(new BatteryStateDateTimeFrame(m_batteryStatePercent));
    }

    [ContextMenu("Save JSON")]
    public void SaveAsJsonInAppFolder() {
        Eloi.E_GeneralUtility.GetTimeULongIdWithNow(out ulong id);
        string path = Application.persistentDataPath + "/JSON/BatteryTimeFrame/" + id+".json";
        Debug.Log("PATH BATTERY LOG: " + path);
        Eloi.IMetaAbsolutePathFileGet file = new Eloi.MetaAbsolutePathFile(path);
        string json = JsonUtility.ToJson(m_recordCollection,true);
        Eloi.E_FileAndFolderUtility.ExportByOverriding(file, json);
        
    
    }

    private void OnDestroy()
    {
        SaveAsJsonInAppFolder();
    }


    [System.Serializable]
    public class BatteryStateDateTimeFrameCollection  {
        public List<BatteryStateDateTimeFrame> m_frames = new List<BatteryStateDateTimeFrame>();
    }


    [System.Serializable]
    public class BatteryStateDateTimeFrame {

        public float m_batteries;
        public Eloi.SerializableDateTime m_timeWhenRecorded;
        public BatteryStateDateTimeFrame(float batteryStatePercent)
        {
            this.m_batteries = batteryStatePercent;
            m_timeWhenRecorded = new Eloi.SerializableDateTime(DateTime.Now);
        }
    }
 
}

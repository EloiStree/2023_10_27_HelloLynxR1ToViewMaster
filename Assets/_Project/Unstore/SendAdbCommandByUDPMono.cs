using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SendAdbCommandByUDPMono : MonoBehaviour
{
    public string m_ipAddress= "192.168.1.47";
    public int m_port=3025;
    
    private UdpClient udpClient;

    public void TakeScreenshot(int pixelX, int pixelY)
    {
        SendUDPMessage(string.Format("adb shell screencap -p /sdcard/pixel_{0}x{1}.png && adb pull /sdcard/{0}x{1}.png", pixelX, pixelY));
    }

    public void SendUDPMessage(string message)
    {
        if (udpClient == null)
            udpClient = new UdpClient();

        try
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, m_ipAddress, m_port);
            Debug.Log("Sent: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending UDP message: " + e.ToString());
        }
    }
}

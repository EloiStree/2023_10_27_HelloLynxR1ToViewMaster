using UnityEngine;
using UnityEngine.UI;

public class MicrophoneController : MonoBehaviour
{
    public Text microphoneText;

    void Start()
    {
        // Get the list of available microphones
        string[] microphones = Microphone.devices;

        // Update the UI Text element with the available microphones
        string micList = "Available Microphones:\n";
        foreach (string microphone in microphones)
        {
            micList += microphone + "\n";
        }

        microphoneText.text = micList;
    }
}
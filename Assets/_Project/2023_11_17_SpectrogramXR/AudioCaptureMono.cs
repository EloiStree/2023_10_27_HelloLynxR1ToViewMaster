using UnityEngine;

public class AudioCaptureMono : MonoBehaviour
{
    public int sampleRate = 44100; // Adjust as needed
    public int windowSize = 1024; // Adjust as needed
    public float[] audioData;

    private void OnAudioFilterRead(float[] data, int channels)
    {
        audioData = data;
    }
}
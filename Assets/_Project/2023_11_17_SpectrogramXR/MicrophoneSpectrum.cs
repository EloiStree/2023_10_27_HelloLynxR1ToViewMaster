using UnityEngine;
using UnityEngine.UI;

public class MicrophoneSpectrum : MonoBehaviour
{
    public int m_microphoneIndex = -1;
    public Texture2D m_spectrogramTexture;
    public RawImage spectrumGraph;
    public int spectrumResolution = 256;
    public int spectrumLength = 1024;
    public float updateInterval = 0.05f;

    public AudioSource audioSource;
    private float[] spectrumData;

    private float timer;

    public string[] m_microphone;
    void Start()
    {
        m_microphone = Microphone.devices;
        m_spectrogramTexture = new Texture2D( spectrumLength, spectrumResolution);
        spectrumGraph.texture = m_spectrogramTexture;
        spectrumData = new float[spectrumResolution];
        string micChoosed = null;
        if (m_microphoneIndex >=0 && m_microphone.Length > m_microphoneIndex)
            micChoosed = m_microphone[m_microphoneIndex];
        audioSource.clip = Microphone.Start(micChoosed, true, 1, AudioSettings.outputSampleRate);
        audioSource.loop = true;
        //while (!(Microphone.GetPosition(null) > 0)) { }
        
        audioSource.Play();
    }
    public FFTWindow m_typeOfSpectrum = FFTWindow.Hamming;

    public AnimationCurve m_amplifier;
  
    public float maxMagnitude = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {

                audioSource.GetSpectrumData(spectrumData, 0, m_typeOfSpectrum);


            for (int x = 0; x < spectrumLength; x++)
            {
                for (int y = 0; y < spectrumResolution; y++)
                {
                    Color pixelColor = m_spectrogramTexture.GetPixel(x, y);

                    // Calculate the new X position (move one pixel to the left)
                    int newX = (x - 1 + spectrumLength) % spectrumLength;

                    // Set the pixel in the new texture
                    m_spectrogramTexture.SetPixel(newX, y, pixelColor);
                }
            }
            for (int i = 0; i < spectrumResolution; i++)
            {
                float spectrumValue = Mathf.Clamp01(spectrumData[i]) * 100; // Scale the value for visualization
                spectrumValue*= m_amplifier.Evaluate(spectrumValue);
                if (spectrumValue > maxMagnitude)
                {
                    maxMagnitude = spectrumValue;
                }

                m_spectrogramTexture.SetPixel(0, i, new Color(0,spectrumValue/ maxMagnitude, 0));

            }
            m_spectrogramTexture.Apply();

            timer = 0f;
        }
    }
}

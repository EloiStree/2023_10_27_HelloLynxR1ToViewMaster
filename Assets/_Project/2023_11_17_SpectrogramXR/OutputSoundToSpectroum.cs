using UnityEngine;
using UnityEngine.UI;

public class OutputSoundToSpectroum : MonoBehaviour
{
    public AudioCaptureMono m_clipCapture;
    public Texture2D m_spectrogramTexture;
    public RawImage spectrumGraph;
    public int spectrumResolution = 256;
    public int spectrumLength = 1024;
    public float updateInterval = 0.05f;

    private float[] spectrumData;

    private float timer;
    public AnimationCurve m_amplifier;


    private void Awake()
    {
        m_spectrogramTexture = new Texture2D(spectrumLength, spectrumResolution);
        spectrumGraph.texture = m_spectrogramTexture;
        spectrumData = new float[spectrumResolution];
    }

    public float maxMagnitude = 0;
    public FFTWindow m_type= FFTWindow.Hamming;
    void Update()
    {
        timer += Time.deltaTime;

        AudioListener.GetSpectrumData(spectrumData, 0, m_type);
        //spectrumData = m_clipCapture.audioData;
        if (timer >= updateInterval)
        {
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
                if (spectrumValue > maxMagnitude)
                {
                    maxMagnitude = spectrumValue;
                }
                spectrumValue *= m_amplifier.Evaluate(spectrumValue);
                m_spectrogramTexture.SetPixel(0, i, new Color(0, spectrumValue / maxMagnitude, 0));
            }
            m_spectrogramTexture.Apply();

            timer = 0f;
        }
    }
}

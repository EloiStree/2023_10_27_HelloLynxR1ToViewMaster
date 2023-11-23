using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPositionExperimentMono : MonoBehaviour
{
    public SendAdbCommandByUDPMono m_udpScreenshot;
    public Material m_targetCybeSkyeboxMaterial;
    public Material m_targetCybeSkyeboxMaterialDebug;

    Texture2D t;
    public float m_timeBetweenPixelChanged=0.1f;
    public int m_size=64;
    public bool linear=false;
    public bool mipChain = false;
    public TextureFormat format = TextureFormat.RGBA32;
    public Color m_background = Color.green;
    public Color m_cursor = Color.red;
    public bool m_sendUDP;
    public IEnumerator Start()
    {
        t = new Texture2D(m_size, m_size, format, mipChain, linear);
        m_targetCybeSkyeboxMaterial.SetTexture("_FrontTex", t);
        m_targetCybeSkyeboxMaterial.SetTexture("_BackTex", t);
        m_targetCybeSkyeboxMaterial.SetTexture("_LeftTex", t);
        m_targetCybeSkyeboxMaterial.SetTexture("_RightTex", t);
        m_targetCybeSkyeboxMaterial.SetTexture("_UpTex", t);
        m_targetCybeSkyeboxMaterial.SetTexture("_DownTex", t);
        m_targetCybeSkyeboxMaterialDebug.SetTexture("_MainTex", t);



        Color[] c = t.GetPixels();
        for (int j = 0; j < c.Length; j++)
        {
            c[j] = m_background;
        }
        int i=0;
        while (true) {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenPixelChanged);
            if (i < c.Length - 6)
            {
                c[i] = m_background;
                c[i + 1] = m_cursor;
                c[i + 2] = m_cursor;
                c[i + 3] = m_cursor;
                c[i + 4] = m_cursor;
                c[i + 5] = m_cursor;
                t.SetPixels(c);
                t.Apply();
                int x = i % t.width;
                int y = i / t.width;
                if (m_sendUDP)
                    m_udpScreenshot.TakeScreenshot(x, y);
            }
            else i = 0;
            i++;
        }
    }


}

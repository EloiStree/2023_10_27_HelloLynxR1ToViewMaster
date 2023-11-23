using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ApplicationScreencaptureToPermaDataMono : MonoBehaviour
{
    public int m_superSize = 4;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureAndSaveScreenshot();
        }
    }

    public void CaptureAndSaveScreenshot()
    {
        // Create a timestamped file name for the screenshot.
        string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

        // Combine the persistentDataPath with the desired subfolder for the screenshots.
        string subfolderPath = Path.Combine(Application.persistentDataPath, "Screenshots");
        string filePath = Path.Combine(subfolderPath, fileName);

        // Create the subfolder if it doesn't exist.
        if (!Directory.Exists(subfolderPath))
        {
            Directory.CreateDirectory(subfolderPath);
        }

        //ScreenCapture.CaptureScreenshot(filePath);
        Texture2D t = ScreenCapture.CaptureScreenshotAsTexture(2);
        Eloi.MetaAbsolutePathFile f = new Eloi.MetaAbsolutePathFile(filePath);
        Eloi.E_FileAndFolderUtility.ExportTextureAsPNG(f, t, true, true);
    }
}
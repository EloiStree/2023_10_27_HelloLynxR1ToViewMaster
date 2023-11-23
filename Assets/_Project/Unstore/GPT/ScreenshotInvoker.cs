using UnityEngine;

public class ScreenshotInvoker : MonoBehaviour
{
    public void InvokeScreenshot()
    {
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_MEDIA_BUTTON"));

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        intentObject.Call<AndroidJavaObject>("setClass", activity, activity.Call<string>("getClass"));

        AndroidJavaClass sendBroadcast = new AndroidJavaClass("android.content.Context");
        activity.Call("sendBroadcast", intentObject);
    }
}
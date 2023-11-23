using UnityEngine;

public class ButtonDetectorMono : MonoBehaviour
{
    public Eloi.PrimitiveUnityEvent_String m_buttonPressed;
    void Update()
    {

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                m_buttonPressed.Invoke(keyCode.ToString());
            }
        }
    }
}
/**
 * @file AutoSwitchARVR.cs
 * 
 * @author Geoffrey Marhuenda
 * 
 * @brief Automatically switch between AR and VR modes and display current mode in a TextMeshPro text.
 */
using System.Collections;
using UnityEngine;

namespace Lynx
{
    public class AutoSwitchARVR : MonoBehaviour
    {
        [Tooltip("Time between each switch (AR/VR).")]
        [SerializeField] private float m_timer = 5.0f;
        [SerializeField] private TMPro.TMP_Text m_console = null;

        public bool m_startInVR = true;
        public bool m_useTogggle = false;

        public bool IsRunning { get; set; } = false;

        IEnumerator Start()
        {
            IsRunning = true;

#if UNITY_EDITOR
            yield break ;
#endif

            // Each <m_timer> seconds, the headset switch between AR and VR mode.
            while (IsRunning)
            {

                
                yield return new WaitForSecondsRealtime(m_timer);
                if (m_startInVR)
                    LynxAPI.SetVR();
                else LynxAPI.SetAR();

                if (m_useTogggle && LynxAPI.IsAR())
                    LynxAPI.ToggleAR();
                yield return new WaitForEndOfFrame(); // Fix, otherwise the API is not able to see the change
                m_console.text = LynxAPI.IsAR() ? "Mode: AR" : "Mode: VR";
            }
        }

        private void OnApplicationQuit()
        {
            IsRunning = false;
        }


    }
}
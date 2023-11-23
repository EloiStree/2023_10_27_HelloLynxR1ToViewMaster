using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Temp_ChangeVideoClip : MonoBehaviour
{
    public VideoPlayer m_player;
    public VideoClip m_clipMetal;
    public VideoClip m_clipGanja;


    public void SetMetal()
    {
        m_player.clip = m_clipMetal;
    }
    public void SetGanja()
    {
        m_player.clip = m_clipGanja;
    }
}

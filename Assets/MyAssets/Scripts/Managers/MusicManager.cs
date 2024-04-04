using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class MusicManager : MonoBehaviour
{
    #region Variables
    public static MusicManager instance;
    AudioSource audioSource;
    public AudioClip musicMainMenu;
    public AudioClip[] musicGameplay;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Funciones Propias
    public void Play(int _num)
    {
        if (_num == 0)
        {
            audioSource.clip = musicMainMenu;
            audioSource.Play();
        }
        else if (_num <= musicGameplay.Length)
        {
            if (audioSource.clip != null)         
            {
                if (audioSource.clip != musicGameplay[_num - 1])
                {
                    audioSource.clip = musicGameplay[_num - 1];
                    audioSource.Play();
                }
                else
                    Resume();
            }
            else
            {
                audioSource.clip = musicGameplay[_num - 1];
                audioSource.Play();
            }
        }
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    } 

    public void Resume()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }

    public void Restart()
    { 
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }

    #endregion
}

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
    public AudioSource mainMenuAudioSource;
    public AudioSource gameplayAudioSource;
    public AudioClip musicMainMenu;
    public AudioClip[] musicGameplay;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        //audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Funciones Propias
    public void Play(int _num)
    {
        if (_num == 0)
        {
            Debug.Log("Ejecutando main Menu");
            mainMenuAudioSource.clip = musicMainMenu;
            mainMenuAudioSource.Play();
        }
        else if (_num <= musicGameplay.Length)
        {
            if (gameplayAudioSource.clip != null)
            {
                if (gameplayAudioSource.clip != musicGameplay[_num - 1])
                {
                    gameplayAudioSource.clip = musicGameplay[_num - 1];
                    gameplayAudioSource.Play();
                }
                else
                    Resume(_num);
            }
            else
            {
                gameplayAudioSource.clip = musicGameplay[_num - 1];
                gameplayAudioSource.Play();
            }
        }
    }

    public void Pause(int _num)
    {
        if (_num == 0)
        {
            if (mainMenuAudioSource.isPlaying)
                mainMenuAudioSource.Pause();
        }
        else
        {
            if (gameplayAudioSource.isPlaying)
                gameplayAudioSource.Pause();
        }
    } 

    public void Resume(int _num)
    {
        if (_num == 0)
        {
            if (!mainMenuAudioSource.isPlaying)
                mainMenuAudioSource.UnPause();
        }
        else
        {
            if (!gameplayAudioSource.isPlaying)
                gameplayAudioSource.UnPause();
        }
    }

    public void Restart(int _num)
    {
        if (_num == 0)
        {
            if (!mainMenuAudioSource.isPlaying)
            {
                mainMenuAudioSource.Stop();
                mainMenuAudioSource.Play();
            }
        }
        else
        {
            if (!gameplayAudioSource.isPlaying)
            {
                gameplayAudioSource.Stop();
                gameplayAudioSource.Play();
            }
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class SoundManager : MonoBehaviour
{
    #region Variables
    public static SoundManager instance;
    AudioSource audioSource;
    public AudioClip[] uiSounds;
    public AudioClip[] sounds;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this; 
        audioSource = GetComponent<AudioSource>();

    }
    #endregion

    #region Funciones Propias
    public void PlayUi(int _index)
    {
        audioSource.PlayOneShot(uiSounds[_index]);
    }

    public void Play(int _index)
    {
        audioSource.PlayOneShot(sounds[_index]);
    }

    #endregion
}

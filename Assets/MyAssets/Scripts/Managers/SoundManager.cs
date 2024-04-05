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
    public void PlayUi(int _index, float _volume)
    {
        audioSource.volume = _volume;
        audioSource.PlayOneShot(sounds[_index]);
    }

    #endregion
}

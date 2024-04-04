using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static SpidersBehavior;

/// <summary>
///
/// </summary>

public class CameraCarousel : MonoBehaviour
{
    #region Variables
    AutoExposure autoExposure;
    #endregion

    #region Funciones Unity

    private void Update()
    {
        
    }
    #endregion

    #region Funciones Propias
    public void StartAnimation(PostProcessProfile _profile) 
    {
        AutoExposure _tmp;
        if (_profile.TryGetSettings<AutoExposure>(out _tmp))
        {
            autoExposure = _tmp;
            StartCoroutine(AnimationCoroutine());
        }
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(2f);
        FeriaManager.instance.Carousel_End();
    }

    #endregion
}

using Cinemachine;
using Cinemachine.PostFX;
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
    PostProcessProfile profile;
    public Transform plane;
    public Texture[] planeTexture;
    Material material;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        profile = GetComponent<CinemachinePostProcessing>().m_Profile;
        profile.TryGetSettings(out autoExposure);

        MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
        material = renderer.material;
    }

    private void Update()
    {
        
    }
    #endregion

    #region Funciones Propias
    public void StartAnimation() 
    {
        plane.gameObject.SetActive(true);
        //AutoExposure _tmp;

        //bool _tmp_autoExposure = profile.TryGetSettings(out _tmp);
        //Debug.Log("AutoExposure existe");
        //if (autoExposure != null)
        //{
        //  //  autoExposure = _tmp;
        //    StartCoroutine(AnimationCoroutine());
        //}
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        Debug.Log("Ingresó antes de 2 segundos");
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[1]);
        yield return new WaitForSeconds(2f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[2]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[3]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[4]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[5]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[6]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlayUi(2);
        material.SetTexture("_MainTex", planeTexture[7]);
        yield return new WaitForSeconds(1f);
        material.SetTexture("_MainTex", planeTexture[0]);
        yield return new WaitForSeconds(2f);
        FeriaManager.instance.Carousel_End();
        plane.gameObject.SetActive(false);
    }

    #endregion
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///
/// </summary>

public class SpiderWeb : MonoBehaviour
{
    #region Variables
    public int val;

    public Texture spiderwebComplete, spiderwebMedium, spiderwebDestroyed;
    MeshRenderer renderer;
    Material material;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        material = renderer.material;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            isLit = !isLit;
            GameObject _fire = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            _fire.SetActive(isLit);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
    #endregion

    #region Funciones Propias

    public void BurningWeb(int _value)
    {
        if (_value == 1)
            material.SetTexture("_MainTex", spiderwebMedium);
        else if (_value == 2)
            material.SetTexture("_MainTex", spiderwebDestroyed);
        else
            material.SetTexture("_MainTex", spiderwebComplete);
    }

    #endregion
}

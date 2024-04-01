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

public class Torch : MonoBehaviour
{
    #region Variables
    public bool isLit;

    public bool playerIsOnTorch;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLit = !isLit;
            GameObject _fire = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            _fire.SetActive(isLit);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOnTorch = true;
            //torch = other.transform;
        }
    }
    #endregion

    #region Funciones Propias

    #endregion
}

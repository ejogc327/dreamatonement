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

public class Cocoon : MonoBehaviour
{
    #region Variables
    public bool playerIsOnCocoon;
    #endregion

    #region Funciones Unity
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOnCocoon = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsOnCocoon = false;
        }
    }
    #endregion

    #region Funciones Propias
    #endregion
}

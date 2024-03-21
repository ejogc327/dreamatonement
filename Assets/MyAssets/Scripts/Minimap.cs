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

public class Minimap : MonoBehaviour
{
    #region Variables
    public Transform player;
    public Canvas canvas;
    public Image image;
    bool mapBig;
    #endregion

    #region Funciones Unity

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.M))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (!mapBig)
            {
                mapBig = true;
                image.rectTransform.sizeDelta = new Vector2(1024f, 1024f);
                image.rectTransform.anchoredPosition = new Vector2(460f, -30f);
            }
            else
            {
                mapBig = false;
                image.rectTransform.sizeDelta = new Vector2(256f, 256f);
                image.rectTransform.anchoredPosition = new Vector2(20f, -20f);
            }
        }        
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
    #endregion

    #region Funciones Propias
    #endregion
}

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

public class ObjectsManager : MonoBehaviour
{
    #region Variables
    public static ObjectsManager instance;
    public Transform player;
    public RawImage rawImage;
    public Transform torchOrigin;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CreateTorch();
        }

        
            
    }

    #endregion

    #region Funciones Propias

    public void CreateTorch()
    {
        Vector3 _position = new Vector3(2f, 0f, 0f);
        Quaternion _rotation = Quaternion.Euler(0f, 0f, 0f);

        Transform _torch = Instantiate(torchOrigin, _position, _rotation);
        _torch.SetParent(transform);
    }
    #endregion
}

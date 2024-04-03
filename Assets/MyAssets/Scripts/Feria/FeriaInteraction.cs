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

public class FeriaInteraction : MonoBehaviour
{
    #region Variables
    public static FeriaInteraction instance;
    public Transform torchOrigin;
    public Transform barrelOrigin;
    public Transform spiderwebOrigin;
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
            CreateBarrel();
            CreateSpiderwebs();
        }
    }

    #endregion

    #region Funciones Propias

    public void CreateTorch()
    {
        Vector3 _position = new Vector3(2f, 0f, -3f);
        Quaternion _rotation = Quaternion.Euler(0f, 0f, 0f);

        Transform _torch = Instantiate(torchOrigin, _position, _rotation);
        _torch.SetParent(transform.GetChild(0));
    }

    public void CreateBarrel()
    {
        Vector3 _position = new Vector3(1f, 0f, -6f);
        Quaternion _rotation = Quaternion.Euler(0f, 0f, 0f);

        Transform _barrel = Instantiate(barrelOrigin, _position, _rotation);
        _barrel.SetParent(transform.GetChild(1));
    }

    public void CreateSpiderwebs()
    {
        Vector3 _position;
        Quaternion _rotation = Quaternion.identity;
        Transform _spiderweb;

        _position = new Vector3(11f, 0f, 13.25f);
        _spiderweb = Instantiate(spiderwebOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        _position = new Vector3(24f, 0f, 13.25f);
        _spiderweb = Instantiate(spiderwebOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

    }
    #endregion
}

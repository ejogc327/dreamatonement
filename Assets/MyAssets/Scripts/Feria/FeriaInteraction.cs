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
    public Transform spiderwebStrongOrigin;
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

        // Telarañas destruibles

        _position = new Vector3(11f, 0f, 13.25f);
        _spiderweb = Instantiate(spiderwebOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.3f);
        _spiderweb.SetParent(transform.GetChild(2));


        _position = new Vector3(27.3f, 0f, -6f);
        _spiderweb = Instantiate(spiderwebOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.6f);
        _spiderweb.SetParent(transform.GetChild(2));

        _position = new Vector3(23.5f, 0f, 2.6f);
        _rotation = Quaternion.Euler(0f, 90f, 0f);
        _spiderweb = Instantiate(spiderwebOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1f);
        _spiderweb.SetParent(transform.GetChild(2));


        // Telarañas no destruibles

        _position = new Vector3(3.9f, 0f, 18.3f);
        _rotation = Quaternion.Euler(0f, 45f, 0f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        _position = new Vector3(10.3f, 0f, 18.2f);
        _rotation = Quaternion.Euler(0f, -45f, 0f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        // Corredores de trabajadores.

        // Corredor 2 de trabajadores
        _position = new Vector3(11f, 0f, 8.5f);
        _rotation = Quaternion.identity;
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(23f, 0f, 8.5f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(24f, 0f, 8.5f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(36f, 0f, 8.5f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        // Corredor 3 de trabajadores
        _position = new Vector3(11f, 0f, -1f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(23f, 0f, -1f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(24f, 0f, -1f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(36f, 0f, -1f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        // Entre tiendas 1
        _position = new Vector3(23.5f, 0f, 14.5f);
        _rotation = Quaternion.Euler(0f, 90f, 0f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.SetParent(transform.GetChild(2));

        // Corredor 1
        _position = new Vector3(27f, 0f, 13.25f);
        _rotation = Quaternion.identity;
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.3f);
        _spiderweb.SetParent(transform.GetChild(2));

        // Corredor 2 (central)
        _position = new Vector3(17f, 0f, 3.65f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.3f);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(37f, 0f, 3.65f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.3f);
        _spiderweb.SetParent(transform.GetChild(2));

        // Corredor 3 (baños)
        _position = new Vector3(14f, 0f, -6.19f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.6f);
        _spiderweb.SetParent(transform.GetChild(2));
        _position = new Vector3(14f, 0f, -9.35f);
        _spiderweb = Instantiate(spiderwebStrongOrigin, _position, _rotation);
        _spiderweb.localScale = new Vector3(1f, 1f, 1.6f);
        _spiderweb.SetParent(transform.GetChild(2));

    }
    #endregion
}

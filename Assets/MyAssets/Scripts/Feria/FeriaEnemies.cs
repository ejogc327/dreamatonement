using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FeriaEnemies : MonoBehaviour
{
    #region 1. Variables
    public static FeriaEnemies instance;

    public Transform humanoidsOrigin;
    List<Transform> humanoids = new List<Transform>();
    public Transform spidersOrigin;
    List<Transform> spiders = new List<Transform>();
    public Transform spidersMediumOrigin;
    List<Transform> spidersMedium = new List<Transform>();

    

    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.L)) 
        {
            CreateSpiders();
        }*/
    }
    #endregion

    #region 3. Funciones Propias
    public void CreateHumanoids()
    {
        int i = 0;
        Vector3 _position = new Vector3(59f, 0f, 10f);
        Quaternion _rotation = Quaternion.Euler(0f, 180f, 0f);

        humanoids.Add(Instantiate(humanoidsOrigin, _position, _rotation));
        humanoids[i].SetParent(transform.GetChild(0));
        i++;
    }

    public void CreateSpiders()
    {
        int i = 0;
        Vector3 _position;
        Quaternion _rotation = Quaternion.identity;

        _position = new Vector3(7f, 0f, 13f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(19f, 0f, 13f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(20f, 0f, -6.25f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(20f, 0f, -8f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(58f, 0f, -7f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(58f, 0f, -8f);
        spiders.Add(Instantiate(spidersOrigin, _position, _rotation));
        spiders[i].SetParent(transform.GetChild(1));
        i++;
    }
    public void CreateSpidersMedium()
    {
        int i = 0;
        Vector3 _position;
        Quaternion _rotation = Quaternion.identity;

        _position = new Vector3(55f, 0f, -7f);
        spidersMedium.Add(Instantiate(spidersMediumOrigin, _position, _rotation));
        spidersMedium[i].SetParent(transform.GetChild(1));
        i++;

        _position = new Vector3(55f, 0f, -2f);
        spidersMedium.Add(Instantiate(spidersMediumOrigin, _position, _rotation));
        spidersMedium[i].SetParent(transform.GetChild(1));
        i++;

    }

    void CheckIfOverlap(Transform _transform)
    {
        bool _isIntersecting;
        do
        {
            Collider[] _intersecting = Physics.OverlapSphere(_transform.position + Vector3.up, 0.01f);

            if (_intersecting.Length > 0)
            {
                _isIntersecting = true;
                _transform.Translate(_transform.right * 1f * Time.deltaTime, Space.Self);
            }
            else
            {
                _isIntersecting = false;
            }

        } while (_isIntersecting);
    }
    #endregion


}

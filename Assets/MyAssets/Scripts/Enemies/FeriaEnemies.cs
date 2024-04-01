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

    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
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

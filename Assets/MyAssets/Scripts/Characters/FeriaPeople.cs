using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeriaCharacters : MonoBehaviour
{
    #region 1. Variables
    public static FeriaCharacters instance;

    public Transform[] employeesOrigin;
    public Transform[] publicOrigin;
    public Transform[] childrenOrigin;

    public Transform[] employees;
    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        CreateEmployees();
        SetEmployeesPosition();
    }
    #endregion

    #region 3. Funciones Propias
    void CreateEmployees()
    {
        employees = new Transform[46];
        for (int i = 0; i < employees.Length; i++)
        {
            Vector3 _pos = Vector3.zero;
            Quaternion _rot = Quaternion.identity;
            employees[i] = Instantiate(employeesOrigin[Random.Range(0, employeesOrigin.Length - 1)], _pos, _rot);
            employees[i].SetParent(transform.GetChild(0));
        }
    }

    void SetEmployeesPosition()
    {
        for (int i = 0; i < employees.Length; i++)
        {
            employees[i].position = FeriaBuildings.instance.employeesTransformData[i].position;
            employees[i].rotation = FeriaBuildings.instance.employeesTransformData[i].rotation;
        }            
    }


    #endregion


}

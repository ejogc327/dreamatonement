using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeriaCharacters : MonoBehaviour
{
    #region 1. Variables
    public static FeriaCharacters instance;

    public Transform[] employeesOrigin;
    public Transform[] peopleOrigin;
    public Transform[] childrenOrigin;

    Transform[] employees;
    Transform[] people;
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
        CreatePeople();
        SetPeoplePosition();
    }
    #endregion

    #region 3. Funciones Propias
    void CreateEmployees()
    {
        employees = new Transform[47];
        //Debug.Log("Employees Length: " + employees.Length);
        //Debug.Log("Employees Origin Length: " + employeesOrigin.Length);

        for (int i = 0; i < employees.Length; i++)
        {
            //int _random = UnityEngine.Random.Range(0, employeesOrigin.Length - 1);
            //Debug.Log("Random " + i + ": " + _random);
            employees[i] = Instantiate(employeesOrigin[UnityEngine.Random.Range(0, employeesOrigin.Length - 1)], Vector3.zero, Quaternion.identity);
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

    void CreatePeople()
    {
        people = new Transform[2];
        for (int i = 0; i < people.Length; i++)
        {
            people[i] = Instantiate(peopleOrigin[UnityEngine.Random.Range(0, peopleOrigin.Length - 1)], Vector3.zero, Quaternion.identity);
            people[i].SetParent(transform.GetChild(1));
        }
    }

    void SetPeoplePosition()
    {
        for (int i = 0; i < people.Length; i++)
        {
            int _random = UnityEngine.Random.Range(1, Enum.GetNames(typeof(FeriaPeopleBehavior.PeopleActions)).Length  - 1);
            people[i].position = FeriaBuildings.instance.peopleTransformData[i].position;
            people[i].rotation = FeriaBuildings.instance.employeesTransformData[i].rotation;
            Debug.Log("Trabajador " + i + ": " + people[i].position);
        }
    }
    #endregion


}

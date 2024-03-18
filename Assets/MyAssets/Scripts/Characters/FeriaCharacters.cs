using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            //people[i] = Instantiate(peopleOrigin[5], Vector3.zero, Quaternion.identity);
            people[i] = Instantiate(peopleOrigin[UnityEngine.Random.Range(0, peopleOrigin.Length - 1)], Vector3.zero, Quaternion.identity);
            people[i].SetParent(transform.GetChild(1));
        }
    }

    void SetPeoplePosition()
    {
        for (int i = 0; i < people.Length; i++)
        {
            int _random = UnityEngine.Random.Range(1, Enum.GetNames(typeof(FeriaPeopleBehavior.PeopleActions)).Length  - 1);
            people[i].position = FeriaBuildings.instance.peopleTransformData[1].position;
            people[i].rotation = FeriaBuildings.instance.peopleTransformData[1].rotation;
            bool _isIntersecting = false;
            do
            {
                Collider[] _intersecting = Physics.OverlapSphere(people[i].position + Vector3.up, 0.01f);
                if (_intersecting.Length > 0)
                {
                    Debug.Log("Hay un objeto encima");
                    _isIntersecting = true;
                    people[i].Translate(Vector3.right * Time.deltaTime, Space.World);
                }
                else
                {
                    Debug.Log("No hay un objeto encima");
                    _isIntersecting = false;
                }

            } while (_isIntersecting);

            FeriaPeopleBehavior _script = people[i].GetComponent<FeriaPeopleBehavior>();
            //if (_script != null) continue;
            _script.SetPeopleAction(FeriaPeopleBehavior.PeopleActions.GoToEntry);
        }
    }
    #endregion


}

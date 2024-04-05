using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FeriaCharacters : MonoBehaviour
{
    #region 1. Variables
    public static FeriaCharacters instance;

    public Transform[] employeesOrigin;
    public Transform[] peopleOrigin;
    public Transform[] kidsOrigin;

    List<Transform> employees = new List<Transform>();
    List<Transform> people = new List<Transform>();
    List<Transform> kids = new List<Transform>();

    Transform sara;
    Transform mati;
    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;
        sara = GameObject.FindWithTag("Player").transform;
        mati = GameObject.FindWithTag("Mati").transform;
    }

    private void Update()
    {
        
    }
    #endregion

    #region 3. Funciones Propias
    public void CreateFeriaPeople()
    {
        CreateEmployees();
        CreatePeople();
        CreateKids();
    }

    void CreateEmployees()
    {
        for (int i = 0; i < 48; i++)
        {
            int _peopleRandom = UnityEngine.Random.Range(0, peopleOrigin.Length);
            employees.Add(Instantiate(peopleOrigin[_peopleRandom], FeriaBuildings.instance.employeesTransformData[i].position, FeriaBuildings.instance.employeesTransformData[i].rotation));
            employees[i].SetParent(transform.GetChild(0));

            FeriaPeopleBehavior _script = employees[i].GetComponent<FeriaPeopleBehavior>();
            if (_script == null) continue;
            if (_peopleRandom > 2) _script.SetWoman();
            _script.ChangeSuit(0);
        }
    }

    void CreatePeople()
    {
        int _peopleCounter = 0;

        // Gente en la fila de tiquetes
        for (int i = 0; i < 10; i++)
        {
            int _peopleRandom = UnityEngine.Random.Range(0, peopleOrigin.Length);
            Vector3 _position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.TicketLine].position + new Vector3(0f, 0f, 0.5f) * i;
            //Debug.Log(_position);
            Quaternion _rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.TicketLine].rotation;

            people.Add(Instantiate(peopleOrigin[_peopleRandom], _position, _rotation));
            people[i].SetParent(transform.GetChild(1));
            CheckIfOverlap(people[i]);

            FeriaPeopleBehavior _script = people[i].GetComponent<FeriaPeopleBehavior>();
            if (_script == null) continue;
            if (_peopleRandom > 2) _script.SetWoman();
            _script.ChangeSuit(UnityEngine.Random.Range(1, 4));
        }
        _peopleCounter = people.Count;

        // Gente en cada tienda
        for (int i = 0; i < 40; i++)
        {
            int _i = i + _peopleCounter;

            int _peopleRandom = UnityEngine.Random.Range(0, peopleOrigin.Length);
            Vector3 _position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Shop1 + i].position;
            Quaternion _rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Shop1 + i].rotation;
            people.Add(Instantiate(peopleOrigin[_peopleRandom], _position, _rotation));
            people[_i].SetParent(transform.GetChild(1));
            CheckIfOverlap(people[_i]);

            FeriaPeopleBehavior _script = people[_i].GetComponent<FeriaPeopleBehavior>();
            if (_script == null) continue;
            if (_peopleRandom > 2) _script.SetWoman();
            _script.ChangeSuit(UnityEngine.Random.Range(1, 4));
            //_script.SetPeopleAction(FeriaPeopleBehavior.PeopleActions.GoToFreeZone);
        }
        _peopleCounter = people.Count;

        // Gente en zona libre
        for (int i = 0; i < 48; i++)
        {
            int _i = i + _peopleCounter;

            int _peopleRandom = UnityEngine.Random.Range(0, peopleOrigin.Length);
            Vector3 _position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone1].position;
            Quaternion _rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone1].rotation;
            people.Add(Instantiate(peopleOrigin[_peopleRandom], _position, _rotation));
            people[_i].SetParent(transform.GetChild(1));
            CheckIfOverlap(people[_i]);

            FeriaPeopleBehavior _script = people[_i].GetComponent<FeriaPeopleBehavior>();
            if (_script == null) continue;
            if (_peopleRandom > 2) _script.SetWoman();
            _script.ChangeSuit(UnityEngine.Random.Range(1, 4));
            _script.SetPeopleAction(FeriaPeopleBehavior.PeopleActions.GoToFreeZone);
        }
    }

    void CreateKids()
    {
        for (int i = 0; i < 15; i++)
        {
            int _kidsRandom = UnityEngine.Random.Range(0, kidsOrigin.Length);

            Vector3 _position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Carousel].position + new Vector3(0f, 0f, 0.5f) * i;
            //Debug.Log(_position);
            Quaternion _rotation = Quaternion.identity;

            kids.Add(Instantiate(kidsOrigin[_kidsRandom], _position, _rotation));
            kids[i].SetParent(transform.GetChild(2));
            CheckIfOverlap(kids[i]);

            /*FeriaPeopleBehavior _script = kids[i].GetComponent<FeriaPeopleBehavior>();
            if (_script == null) continue;
            if (_kidsRandom > 0) _script.SetWoman();
            _script.ChangeSuit(UnityEngine.Random.Range(1, 4));*/
        }

        // Put them in carousel
        for (int i = 0; i < 15; i++)
        {
            CarouselBehavior.instance.SetKidOnCarousel(kids[i], i);
        }
    }


    public void DestroyFeriaPeople()
    {
        DestroyEmployees();
        DestroyPeople();
        DestroyKids();
    }

    void DestroyEmployees()
    {
        //Debug.Log("destruyendo empleados");
        foreach (Transform _t in transform.GetChild(0).transform)
        {
            Destroy(_t.gameObject);
        }
    }

    void DestroyPeople()
    {
        //Debug.Log("destruyendo gente");
        foreach (Transform _t in transform.GetChild(1).transform)
        {
            Destroy(_t.gameObject);
        }
    }

    void DestroyKids()
    {

        foreach (Transform _t in transform.GetChild(2).transform)
        {
            Destroy(_t.gameObject);
        }
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

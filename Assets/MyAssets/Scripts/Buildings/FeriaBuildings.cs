using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeriaBuildings : MonoBehaviour
{
    #region 1. Variables
    public static FeriaBuildings instance;

    Transform entry;
    Transform info;
    Transform ticket;
    Transform restroomMale;
    Transform restroomFemale;
    Transform[] shops = new Transform[40];
    Transform carousel;
    Transform noria;
    Transform map;
    Transform ticketGames;

    public List<TransformData> buildingsTransformData = new List<TransformData>();
    public List<TransformData> employeesTransformData = new List<TransformData>();
    public List<TransformData> peopleTransformData = new List<TransformData>();
    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;

        SetBuildingsTransforms();
        SetBuildingsTransformData();
        SetEmployeesTransformData();
        SetPeopleTransformData();

        //Type type = Type.GetType("NoriaRotation");
        //gameObject.AddComponent(type);
    }

    private void Start()
    {
    }
    #endregion

    #region 3. Funciones Propias
    void SetBuildingsTransforms()
    {
        //entry = buildings.GetChild().transform;
        ticket = transform.GetChild(0);
        info = transform.GetChild(1).transform;
        restroomMale = transform.GetChild(2).transform;
        restroomFemale = transform.GetChild(3).transform;
        carousel = transform.GetChild(15).transform;
        noria = transform.GetChild(16).transform;

        map = transform.GetChild(17).transform;
        ticketGames = transform.GetChild(18).transform;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                shops[i * 4 + j] = transform.GetChild(i + 4).GetChild(j);
            }
        }
    }

    void SetBuildingsTransformData()
    {
        TransformData _transformData = new TransformData();

        // Entrada: 0
        _transformData.position = Vector3.zero;
        _transformData.rotation = Quaternion.identity;
        buildingsTransformData.Add(_transformData);

        // Ticket: 1
        _transformData.position = ticket.position + new Vector3(-1.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        buildingsTransformData.Add(_transformData);

        // Información: 2
        _transformData.position = info.position + new Vector3(3f, 0f, -1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        buildingsTransformData.Add(_transformData);

        // Baño de hombre: 3
        _transformData.position = restroomMale.position + new Vector3(4.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        buildingsTransformData.Add(_transformData);

        // Baño de mujer: 4
        _transformData.position = restroomFemale.position + new Vector3(-4.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        buildingsTransformData.Add(_transformData);

        // Tiendas: 5 - 44
        for (int i = 0; i < 40; i++)
        {
            int _v = i / 8;
            int _w = (int)Math.Pow(-1, _v);
            _transformData.position = shops[i].position + new Vector3(-1.5f * _w, 0f, -1.5f * _w);
            _transformData.rotation = Quaternion.Euler(0f, 90f + 90f * _w, 0f);
            buildingsTransformData.Add(_transformData);
        }

        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        // Shops1: 45 - 47
        _transformData.position = new Vector3(10f, 0f, 13.5f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(23.5f, 0f, 13.5f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(37f, 0f, 13.5f);
        buildingsTransformData.Add(_transformData);
        // Shops2: 48 - 50
        _transformData.position = new Vector3(10f, 0f, 4f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(23.5f, 0f, 4f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(37f, 0f, 4f);
        buildingsTransformData.Add(_transformData);
        // Shops3: 51 - 53
        _transformData.position = new Vector3(10f, 0f, -5.5f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(23.5f, 0f, -5.5f);
        buildingsTransformData.Add(_transformData);
        _transformData.position = new Vector3(37f, 0f, -5.5f);
        buildingsTransformData.Add(_transformData);

        // Carousel: 54
        _transformData.position = carousel.position;
        _transformData.rotation = Quaternion.identity;
        buildingsTransformData.Add(_transformData);

        // Noria: 55
        _transformData.position = noria.position + new Vector3(-4.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.identity;
        buildingsTransformData.Add(_transformData);

        // Map: 56
        _transformData.position = map.position;
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        buildingsTransformData.Add(_transformData);

        // Ticket games: 57
        _transformData.position = ticketGames.position + new Vector3(-1.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        buildingsTransformData.Add(_transformData);
    }

    void SetEmployeesTransformData()
    {
        TransformData _transformData = new TransformData();

        // Entrada: 0 - 1
        _transformData.position = new Vector3(0f, 0f, 2.5f);
        _transformData.rotation = Quaternion.Euler(0f, 180f, 0f);
        employeesTransformData.Add(_transformData);
        _transformData.position = new Vector3(0f, 0f, -2.5f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        employeesTransformData.Add(_transformData);

        // Ticket: 2 - 3
        _transformData.position = ticket.position + new Vector3(-2f, 0f, 1.5f);
        Debug.Log("Tiquet posición: " + ticket.position);
        Debug.Log("Employee Tiquet posición: " + _transformData.position);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        employeesTransformData.Add(_transformData);
        _transformData.position = ticket.position + new Vector3(-1.5f, 0f, 0.8f);
        _transformData.rotation = Quaternion.Euler(0f, 180f, 0f);
        employeesTransformData.Add(_transformData);

        // Información: 4 - 5
        _transformData.position = info.position + new Vector3(2f, 0f, -2f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        employeesTransformData.Add(_transformData);
        _transformData.position = info.position + new Vector3(2f, 0f, -4f);
        employeesTransformData.Add(_transformData);

        // Baños: 6
        _transformData.position = restroomMale.position + new Vector3(-1f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        employeesTransformData.Add(_transformData);

        // Tiendas: 7 - 46
        for (int i = 0; i < 40; i++)
        {
            int _v = i / 8;
            int _w = (int)Math.Pow(-1, _v);
            _transformData.position = shops[i].position + new Vector3(-1.5f * _w, 0f, -2f * _w);
            _transformData.rotation = Quaternion.Euler(0f, 90f + 90f * _w, 0f);
            employeesTransformData.Add(_transformData);
        }

        // TicketGames: 47
        _transformData.position = ticketGames.position + new Vector3(-2f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        employeesTransformData.Add(_transformData);

    }

    void SetPeopleTransformData()
    {
        TransformData _transformData = new TransformData();

        // Entrada: 0
        _transformData.position = Vector3.zero;
        _transformData.rotation = Quaternion.identity;
        peopleTransformData.Add(_transformData);

        // Ticket: 1 - 2
        _transformData.position = ticket.position + new Vector3(-4f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        _transformData.position = ticket.position + new Vector3(-4f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);

        // Información: 3 - 4
        _transformData.position = info.position + new Vector3(4f, 0f, -2f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        peopleTransformData.Add(_transformData);
        _transformData.position = info.position + new Vector3(4f, 0f, -4f);
        peopleTransformData.Add(_transformData);

        // Baño de hombre: 5
        _transformData.position = restroomMale.position + new Vector3(1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);

        // Baño de mujer: 6
        _transformData.position = restroomFemale.position + new Vector3(-1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        peopleTransformData.Add(_transformData);

        // Tiendas: 7 - 46
        for (int i = 0; i < 40; i++)
        {
            int _v = i / 8;
            int _w = (int)Math.Pow(-1, _v);
            _transformData.position = shops[i].position + new Vector3(-1.5f * _w, 0f, -4f * _w);
            _transformData.rotation = Quaternion.Euler(0f, 90f + 90f * -_w, 0f);
            peopleTransformData.Add(_transformData);
        }

        // Map: 47
        _transformData.position = map.position + new Vector3(-2f, 0f, 0f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // TicketGames: 48
        _transformData.position = ticketGames.position + new Vector3(-3.5f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Carrusel: 49
        _transformData.position = carousel.position + new Vector3(1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        peopleTransformData.Add(_transformData);

        // Noria: 50
        _transformData.position = noria.position + new Vector3(-1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        peopleTransformData.Add(_transformData);




        // Extra4: 52
        _transformData.position = new Vector3(23.6f, 0f, 13.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra5: 53
        _transformData.position = new Vector3(23.6f, 0f, 4f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra6: 54
        _transformData.position = new Vector3(23.6f, 0f, -6f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra7: 55
        _transformData.position = new Vector3(-8.5f, 0f, 13.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra8: 56
        _transformData.position = new Vector3(-8.5f, 0f, 13.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra9: 57
        _transformData.position = new Vector3(-8.5f, 0f, 13.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
        // Extra10: 58
        _transformData.position = new Vector3(-8.5f, 0f, 13.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        peopleTransformData.Add(_transformData);
    }

    #endregion

    public enum Buildings
    {
        Entry, Ticket, Info, RestroomMale, RestroomFemale,
        Shop1, Shop2, Shop3, Shop4, Shop5, Shop6, Shop7, Shop8, Shop9, Shop10,
        Shop11, Shop12, Shop13, Shop14, Shop15, Shop16, Shop17, Shop18, Shop19, Shop20,
        Shop21, Shop22, Shop23, Shop24, Shop25, Shop26, Shop27, Shop28, Shop29, Shop30,
        Shop31, Shop32, Shop33, Shop34, Shop35, Shop36, Shop37, Shop38, Shop39, Shop40,
        Shops1Start, Shops1Half, Shops1End, Shops2Start, Shops2Half, Shops2End, Shops3Start, Shops3Half, Shops3End,
        Carousel, Noria, Map, TicketGames
    }

    public enum EmployeesPositions
    {
        Entry1, Entry2, Ticket1, Ticket2, Info1, Info2, Restrooms,
        Shop1, Shop2, Shop3, Shop4, Shop5, Shop6, Shop7, Shop8, Shop9, Shop10,
        Shop11, Shop12, Shop13, Shop14, Shop15, Shop16, Shop17, Shop18, Shop19, Shop20,
        Shop21, Shop22, Shop23, Shop24, Shop25, Shop26, Shop27, Shop28, Shop29, Shop30,
        Shop31, Shop32, Shop33, Shop34, Shop35, Shop36, Shop37, Shop38, Shop39, Shop40,
        TicketGames,
        Carousel, Noria1, Noria2,
    }

    public enum PeoplePositions
    {
        Entry, Ticket1, Ticket2, Info1, Info2, RestroomMale, RestroomFemale,
        Shop1, Shop2, Shop3, Shop4, Shop5, Shop6, Shop7, Shop8, Shop9, Shop10,
        Shop11, Shop12, Shop13, Shop14, Shop15, Shop16, Shop17, Shop18, Shop19, Shop20,
        Shop21, Shop22, Shop23, Shop24, Shop25, Shop26, Shop27, Shop28, Shop29, Shop30,
        Shop31, Shop32, Shop33, Shop34, Shop35, Shop36, Shop37, Shop38, Shop39, Shop40,
        Map, TicketGames, Carousel, Noria,
        Extra1, Extra2, Extra3, Extra4, Extra5, Extra6, Extra7, Extra8, Extra9, Extra10
    }

}

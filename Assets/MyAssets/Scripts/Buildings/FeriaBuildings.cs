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

    public List<TransformData> workerTransformData = new List<TransformData>();
    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        instance = this;

        SetBuildingTransforms();

        //Type type = Type.GetType("NoriaRotation");
        //gameObject.AddComponent(type);
    }

    private void Start()
    {
        SetTransformData();
    }
    #endregion

    #region 3. Funciones Propias
    void SetBuildingTransforms()
    {
        //entry = buildings.GetChild().transform;
        ticket = transform.GetChild(0);
        info = transform.GetChild(1).transform;
        restroomMale = transform.GetChild(2).transform;
        restroomFemale = transform.GetChild(3).transform;
        carousel = transform.GetChild(15).transform;
        noria = transform.GetChild(16).transform;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                shops[i * 4 + j] = transform.GetChild(i + 4).GetChild(j);
            }
        }
    }

    void SetTransformData()
    {
        TransformData _transformData = new TransformData();

        // Zero: 0
        _transformData.position = Vector3.zero;
        _transformData.rotation = Quaternion.identity;
        workerTransformData.Add(_transformData);

        // Entrada: 1
        _transformData.position = new Vector3(0f, 0f, 2.5f);
        _transformData.rotation = Quaternion.Euler(0f, 180f, 0f);
        workerTransformData.Add(_transformData);

        // Ticket trabajador: 2
        _transformData.position = ticket.position + new Vector3(-2f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        workerTransformData.Add(_transformData);

        // Ticket cliente: 3
        _transformData.position = ticket.position + new Vector3(-4f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        workerTransformData.Add(_transformData);

        // Información trabajador: 4 - 5
        _transformData.position = info.position + new Vector3(2f, 0f, -2f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        workerTransformData.Add(_transformData);
        _transformData.position = info.position + new Vector3(2f, 0f, -4f);
        workerTransformData.Add(_transformData);

        // Información cliente: 6- 7
        _transformData.position = info.position + new Vector3(4f, 0f, -2f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        workerTransformData.Add(_transformData);
        _transformData.position = info.position + new Vector3(4f, 0f, -4f);
        workerTransformData.Add(_transformData);

        // Baños trabajador: 8
        _transformData.position = restroomMale.position + new Vector3(-1f, 0f, 1.5f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        workerTransformData.Add(_transformData);

        // Baño de hombre cliente: 9
        _transformData.position = restroomMale.position + new Vector3(1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        workerTransformData.Add(_transformData);

        // Baño de mujer cliente: 10
        _transformData.position = restroomFemale.position + new Vector3(-1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, -90f, 0f);
        workerTransformData.Add(_transformData);

        // Tiendas1 trabajadores: 11 - 50
        for (int i = 0; i < 40; i++)
        {
            int _v = i / 8;
            int _w = (int)Math.Pow(-1, _v);
            _transformData.position = shops[i].position + new Vector3(-1.5f * _w, 0f, -1.5f * _w);
            _transformData.rotation = Quaternion.Euler(0f, 90f + 90f * _w, 0f);
            workerTransformData.Add(_transformData);
        }
        // Tiendas1 clientes: 61 - 100
        for (int i = 0; i < 40; i++)
        {
            int _v = i / 8;
            int _w = (int)Math.Pow(-1, _v);
            _transformData.position = shops[i].position + new Vector3(-1.5f * _w, 0f, -4f * _w);
            _transformData.rotation = Quaternion.Euler(0f, 90f + 90f * - _w, 0f);
            workerTransformData.Add(_transformData);
        }



        // Baño de hombre cliente: 8
        _transformData.position = carousel.position + new Vector3(1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        workerTransformData.Add(_transformData);

        // Baño de mujer cliente: 9
        _transformData.position = noria.position + new Vector3(-1f, 0f, 2f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        workerTransformData.Add(_transformData);
    }

    public TransformData GetCarouselTransformData()
    {
        TransformData _transformData = new TransformData();
        _transformData.position = carousel.position;
        _transformData.rotation = carousel.rotation;
        return _transformData;
    }

    //public TransformData 
    #endregion

    public enum Buildings
    {
        Entry, Ticket, Info, RestroomMale, RestroomFemale,
        Shop1, Shop2, Shop3, Shop4, Shop5, Shop6, Shop7, Shop8, Shop9, Shop10,
        Shop11, Shop12, Shop13, Shop14, Shop15, Shop16, Shop17, Shop18, Shop19, Shop20,
        Shop21, Shop22, Shop23, Shop24, Shop25, Shop26, Shop27, Shop28, Shop29, Shop30,
        Shop31, Shop32, Shop33, Shop34, Shop35, Shop36, Shop37, Shop38, Shop39, Shop40,
        Carousel, Noria
    }


}

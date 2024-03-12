using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CarouselRotation;

public class CarouselHorses : MonoBehaviour
{
    #region 1. Variables
    public CarouselStates state;
    Transform rotationObject;
    public float speedRot;
    public float timeStarting;
    public float changeSpeed;
    public bool play;
    public List<TransformData> transformData = new List<TransformData>();
    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
    }

    private void Update()
    {
    }

    #endregion

    #region 3. Funciones Propias
    void SetTransformData()
    {
        TransformData _transformData = new TransformData();

        _transformData.position = new Vector3(-4f, 0f, 0f);
        _transformData.rotation = Quaternion.Euler(0f, 0f, 0f);
        transformData.Add(_transformData);
        _transformData.position = new Vector3(-4f, 0f, 0f);
        _transformData.rotation = Quaternion.Euler(0f, 45f, 0f);
        transformData.Add(_transformData);
        _transformData.position = new Vector3(-4f, 0f, 0f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        transformData.Add(_transformData);
        _transformData.position = new Vector3(-4f, 0f, 0f);
        _transformData.rotation = Quaternion.Euler(0f, 90f, 0f);
        transformData.Add(_transformData);
    }
    #endregion

}

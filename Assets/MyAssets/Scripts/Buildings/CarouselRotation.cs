using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCIÓN:
/// Script con el funcionamiento de la moneda.
/// </summary>
public class CarouselRotation : MonoBehaviour
{
    #region 1. Variables
    public static CarouselRotation instance;
    public CarouselStates state;
    Transform rotationObject;
    public float speedRot;
    public float timeStarting;
    public float changeSpeed;
    public bool play;

    public float counter;


    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
        instance = this;
        rotationObject = transform.GetChild(1).transform;
        state = CarouselStates.Stopped;
        //speedRot = 400f;
    }

    private void Update()
    {
        if (state == CarouselStates.Stopped && play)
        {
            play = false;
            SetCarouselState(CarouselStates.Starting);
        }
        if (state == CarouselStates.Starting)
            StartRotation();
        else if (state == CarouselStates.Playing)
            RegularRotation();
        else if (state == CarouselStates.Stopping)
            EndRotation();

        rotationObject.Rotate(Vector3.down * speedRot * Time.deltaTime);
    }

    #endregion

    #region 3. Funciones Propias

    void StartRotation()
    {
        if (counter <= timeStarting)
        {
            counter += Time.deltaTime;
            speedRot += Time.deltaTime * changeSpeed;
        }
        else
        {
            SetCarouselState(CarouselStates.Playing);
            counter = 0f;
        }
    }

    void RegularRotation()
    {
        if (counter <= 10f)
        {
            counter += Time.deltaTime;
        }
        else
        {
            SetCarouselState(CarouselStates.Stopping);
            counter = 0f;
        }
    }

    void EndRotation()
    {
        if (state == CarouselStates.Stopping)
        {
            if (speedRot > 0f)
            {
                speedRot -= Time.deltaTime * changeSpeed;
            }
            else
            {
                speedRot = 0f;
                SetCarouselState(CarouselStates.Stopped);
            }
        }
    }

    void SetCarouselState(CarouselStates _newState)
    {
        state = _newState;
        switch (state)
        {
            case CarouselStates.Stopped:
                speedRot = 0;
                break;
            case CarouselStates.Starting:
                //speedRot = 1;
                //speedRot = Time.deltaTime;
                break;
            case CarouselStates.Playing:
                //speedRot = speedRotMax;
                break;
            case CarouselStates.Stopping:
                break;
        }
    }
    #endregion

    public enum CarouselStates { Stopped, Starting, Playing, Stopping }
}

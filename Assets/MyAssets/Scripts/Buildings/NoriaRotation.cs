using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCIÓN:
/// Script con el funcionamiento de la moneda.
/// </summary>
public class NoriaRotation : MonoBehaviour
{
    #region 1. Variables
    public static NoriaRotation instance;
    Transform rotationObject;
    public float speedRot;
    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
        instance = this;
        rotationObject = transform.GetChild(1).transform;
        //speedRot = 400f;
    }

    private void Update()
    {
        Rotation();
    }
    #endregion

    #region 3. Funciones Propias
    void StartRotation()
    {
        
    }

    void Rotation()
    {
        rotationObject.Rotate(Vector3.right * speedRot * Time.deltaTime);
    }

    void EndRotation()
    {

    }
    #endregion

    public enum NoriaStates { Stopped, Starting, Playing, Stopping }

}

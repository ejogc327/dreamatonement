using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCI�N:
/// Script con el funcionamiento de la moneda.
/// </summary>
public class NoriaRotation : MonoBehaviour
{
    #region 1. Variables
    Transform rotationObject;
    public float speedRot;
    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
        rotationObject = transform.GetChild(1).transform;
        //speedRot = 400f;
    }

    private void Update()
    {
        Rotation();
    }

    void OnMouseDown()
    {
        gameObject.SetActive(false);
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

    
}
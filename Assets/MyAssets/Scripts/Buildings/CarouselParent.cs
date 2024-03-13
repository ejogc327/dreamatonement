using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCIÓN:
/// Script con el funcionamiento de la moneda.
/// </summary>
public class CarouselParent : MonoBehaviour
{
    #region 1. Variables
    public static CarouselParent instance;

    public Transform[] transformInt = new Transform[8];
    public Transform[] transformExt = new Transform[8];


    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
        instance = this;
        //speedRot = 400f;
    }

    private void Update()
    {
    }

    #endregion

    #region 3. Funciones Propias

    #endregion

}

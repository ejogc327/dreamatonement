using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FeriaDataManager : MonoBehaviour
{
    #region Variables
    public static FeriaDataManager instance;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }
    #endregion

    #region Funciones Propias

    #endregion
}

[Serializable]
public class FeriaData
{

}


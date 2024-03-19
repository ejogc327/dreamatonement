using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///
/// </summary>

public class SaraAnimatorIk: MonoBehaviour
{    
    #region Variables
    public static SaraAnimatorIk instance;

    Vector3 targetPosition;
    Animator anim;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (FeriaManager.instance.state == FeriaManager.FeriaStates.KinematicsMap)
        {
            anim.SetLookAtPosition(targetPosition);
            anim.SetLookAtWeight(1f, 0.2f, 1f);
        }
    }
    #endregion

    #region Funciones Propias
    public void SetTargetPositionMap()
    {
        targetPosition = FeriaBuildings.instance.buildingsTransformData[(int)FeriaBuildings.Buildings.Map].position + Vector3.up * 1.5f;
    }
    public void SetTargetPositionMati()
    {        
        targetPosition = MatiBehavior.instance.GetPosition() + Vector3.up * 1.2f;
    }
    #endregion

}

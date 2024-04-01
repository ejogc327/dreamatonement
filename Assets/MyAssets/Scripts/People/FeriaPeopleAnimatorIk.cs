using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///
/// </summary>

public class FeriaPeopleAnimatorIk : MonoBehaviour
{    
    #region Variables
    //public static FeriaPeopleAnimatorIk instance;

    Vector3 targetPosition;
    Animator anim;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        //instance = this;
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (FeriaManager.instance.state == FeriaManager.FeriaStates.KinematicsTransition || FeriaManager.instance.state == FeriaManager.FeriaStates.Gameplay3)
        {
            targetPosition = GameObject.FindWithTag("Player").transform.position + Vector3.up * 1.3f;
            anim.SetLookAtPosition(targetPosition);
            anim.SetLookAtWeight(1f, 0.2f, 1f);
        }
    }
    #endregion

    #region Funciones Propias
    #endregion

}

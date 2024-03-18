using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FeriaManager : MonoBehaviour
{
    #region Variables
    public static FeriaManager instance;
    public FeriaStates state;

    //Transform cam;
    Animator anim;
    public CinemachineVirtualCamera vc0;
    public CinemachineVirtualCamera vcIntro;
    public CinemachineVirtualCamera vcThirdPersonSara;
    bool isKinematics;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        instance = this;
        //cam = Camera.main.transform;

        anim = GetComponent<Animator>();
        //anim = GameObject GetComponent<Animator>();

    }

    private void Start()
    {
        SetFeriaState(FeriaStates.Gameplay1);

    }

    private void Update()
    {

    }
    #endregion

    #region Funciones Propias
    public void UpdateFeriaState()
    {

    }

    public bool IsKinematics()
    {
        return isKinematics;
    }

    public void SetFeriaState(FeriaStates _newState)
    {
        state = _newState;
        switch (state)
        {
            case FeriaStates.Intro:
                isKinematics = true;
                vc0.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 0;
                vcIntro.m_Priority = 2;
                anim.SetInteger("state", 1);
                //MatiMovement.
                //anim.Play("Intro", 0, 0f);
                break;
            case FeriaStates.Gameplay1:
                isKinematics = false;
                vc0.m_Priority = 0;
                vcIntro.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                break;
        }
    }

    public void Intro_End()
    {
        SetFeriaState(FeriaStates.Gameplay1);
        anim.enabled = false;
        anim.SetInteger("state", 2);
    }

    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1,
    }
}


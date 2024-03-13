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
    public CinemachineVirtualCamera vcIntro;
    public CinemachineVirtualCamera vcThirdPersonSara;
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
        SetFeriaState(FeriaStates.Intro);

    }

    private void Update()
    {

    }
    #endregion

    #region Funciones Propias
    public void UpdateFeriaState()
    {

    }


    public void SetFeriaState(FeriaStates _newState)
    {
        state = _newState;
        switch (state)
        {
            case FeriaStates.Intro:
                vcThirdPersonSara.m_Priority = 0;
                vcIntro.m_Priority = 2;
                anim.Play("Intro", 0, 0f);
                break;
            case FeriaStates.Gameplay1:
                vcIntro.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                break;
        }
    }

    public void Intro_End()
    {
        SetFeriaState(FeriaStates.Gameplay1);
        anim.enabled = false;
    }

    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1,
    }
}


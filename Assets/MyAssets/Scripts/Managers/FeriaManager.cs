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
    public CinemachineVirtualCamera vcKinematicsMap;

    public bool isKinematics;

    public bool isPlayerInside;

    GameObject player;
    SaraMovement saraMovementScript;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        instance = this;
        //cam = Camera.main.transform;

        anim = GetComponent<Animator>();
        //anim = GameObject GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        saraMovementScript = player.GetComponent<SaraMovement>();
    }

    private void Start()
    {
        SetFeriaState(FeriaStates.Gameplay1);
        NoriaRotation.instance.speedRot = 5f;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == FeriaStates.Gameplay1 && other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            SetFeriaState(FeriaStates.KinematicsMap);
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        isPlayerInside = false;
    //    }
    //}
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

                saraMovementScript.enabled = false;

                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);
                break;
            case FeriaStates.Gameplay1:
                isKinematics = false;
                vcIntro.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

                saraMovementScript.enabled = true;


                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);
                break;
            case FeriaStates.KinematicsMap:
                isKinematics = true;
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsMap.m_Priority = 2;
                transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

                saraMovementScript.enabled = false;
                //SaraBehavior.instance
                break;
        }
    }

    public void Intro_End()
    {
        SetFeriaState(FeriaStates.Gameplay1);
        anim.enabled = false;
        anim.SetInteger("state", 2);
    }

    public void MatiGoCarousel_Event()
    {
        //SetFeriaState()
        //anim.enabled
        //MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.None);
    }

    public void Map_End()
    {
        SetFeriaState(FeriaStates.Gameplay1);
        anim.enabled = false;
        anim.SetInteger("state", 2);
    }

    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1, KinematicsMap, Gameplay2
    }
}


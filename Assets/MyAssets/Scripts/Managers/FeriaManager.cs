using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Localization.Tables;

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

    string dialogue;

    GameObject player;
    SaraMovement saraMovementScript;
    SaraBehavior saraBehaviorScript;
    NavMeshAgent saraAgent;
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
        saraBehaviorScript = player.GetComponent<SaraBehavior>();
        saraAgent = player.GetComponent<NavMeshAgent>();
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
        GameObject _trigger;
        switch (state)
        {
            case FeriaStates.Intro:
                isKinematics = true;
                vc0.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 0;
                vcIntro.m_Priority = 2;
                anim.SetInteger("state", 1);

                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                break;
            case FeriaStates.Gameplay1:
                dialogue = LocalizationManager.instance.GetFeriaDialogueText("es", "Dialogue1_1");
                HudManager.instance.UpdateDialogue(2f, 2f, 2f, 0.075f, dialogue);

                isKinematics = false;
                vcIntro.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;

                _trigger = transform.GetChild(1).GetChild(0).gameObject;
                _trigger.SetActive(true);

                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;
                saraAgent.enabled = false;
                break;
            case FeriaStates.KinematicsMap:
                isKinematics = true;
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsMap.m_Priority = 2;
                anim.SetInteger("state", 3);

                _trigger = transform.GetChild(1).GetChild(0).gameObject;
                _trigger.SetActive(false);

                SaraAnimatorIk.instance.SetTargetPositionMap();

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                saraAgent.enabled = true;
                break;
            case FeriaStates.Gameplay2:
                isKinematics = false;
                vcKinematicsMap.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;
                saraAgent.enabled = false;
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
        MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.GoToCarousel);
        SaraAnimatorIk.instance.SetTargetPositionMati();
        

    }

    public void Map_End()
    {
        SetFeriaState(FeriaStates.Gameplay2);
        anim.enabled = false;
        anim.SetInteger("state", 4);
    }

    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1, KinematicsMap, Gameplay2
    }
}


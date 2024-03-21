using Cinemachine;
using Cinemachine.PostFX;
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
    public CinemachineVirtualCamera vcKinematicsTransition;


    public CinemachinePostProcessing postProcessing;

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

        postProcessing = vcThirdPersonSara.GetComponent<CinemachinePostProcessing>();
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
            SetFeriaState(FeriaStates.KinematicsTransition);
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

    public void SetFeriaState(FeriaStates _newState)
    {
        state = _newState;
        GameObject _trigger;
        switch (state)
        {
            case FeriaStates.Intro:
                //isKinematics = true;
                vc0.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 0;
                vcIntro.m_Priority = 2;
                anim.SetInteger("state", 1);

                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                break;
            case FeriaStates.Gameplay1:
                //isKinematics = false;
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
                //isKinematics = true;
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsMap.m_Priority = 2;
                anim.SetInteger("state", 2);

                _trigger = transform.GetChild(1).GetChild(0).gameObject;
                _trigger.SetActive(false);

                SaraAnimatorIk.instance.SetTargetPositionMap();

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                saraAgent.enabled = true;
                break;
            case FeriaStates.Gameplay2:
                //isKinematics = false;
                vcKinematicsMap.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;
                saraAgent.enabled = false;
                break;
            case FeriaStates.KinematicsTransition:
                //isKinematics = true;
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsTransition.m_Priority = 2;
                anim.SetInteger("state", 3);

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                saraAgent.enabled = true;
                break;
            case FeriaStates.Gameplay3:
                //isKinematics = false;
                vcKinematicsTransition.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                postProcessing.enabled = true;
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
        anim.SetInteger("state", 0);
    }

    public void StartDialogue1_Event()
    {
        dialogue = LocalizationManager.instance.GetFeriaDialogueText("es", "Dialogue1_1");
        HudManager.instance.UpdateDialogue(dialogue);
    }

    public void MatiGoCarousel_Event()
    {
        //SetFeriaState()
        //anim.enabled
        MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.GoToCarousel);
        SaraAnimatorIk.instance.SetTargetPositionMati();


        //dialogue = LocalizationManager.instance.GetFeriaDialogueText("es", "Dialogue1_1");
        //HudManager.instance.UpdateDialogue(dialogue);
    }

    public void Map_End()
    {
        SetFeriaState(FeriaStates.Gameplay2);
        anim.enabled = false;
        anim.SetInteger("state", 0);
    }

    public void Transition_End()
    {
        SetFeriaState(FeriaStates.Gameplay3);
        anim.enabled = false;
        anim.SetInteger("state", 0);
    }

    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1, KinematicsMap, Gameplay2, KinematicsTransition, Gameplay3
    }
}


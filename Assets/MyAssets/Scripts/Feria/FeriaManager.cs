using Cinemachine;
using Cinemachine.PostFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class FeriaManager : MonoBehaviour
{
    #region Variables
    public static FeriaManager instance;
    public FeriaStates state;

    Camera cam;
    Animator anim;
    public CinemachineVirtualCamera vc0;
    public CinemachineVirtualCamera vcIntro;
    public CinemachineVirtualCamera vcThirdPersonSara;
    public CinemachineVirtualCamera vcKinematicsMap;
    public CinemachineVirtualCamera vcKinematicsTransition;
    public CinemachineVirtualCamera vcCarousel;


    public CinemachinePostProcessing postProcessing;
    public PostProcessProfile[] profiles;

    //public bool isKinematics;

    //public bool isPlayerInside;

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
        cam = Camera.main;

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
        SetFeriaState(FeriaStates.Intro);

        FeriaCharacters.instance.CreateFeriaPeople();
        FeriaCharacters.instance.StartPositionSara();
        FeriaCharacters.instance.StartPositionMati();


        //MatiBehavior.instance.transform.position = new Vector3(50f, 0f, 0f);
        //MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.GoToCarousel);
        NoriaRotation.instance.speedRot = 5f;
    }

    private void Update()
    {
        //if 
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (state == FeriaStates.Gameplay1 && other.gameObject.CompareTag("Player"))
        //{
        //    SetFeriaState(FeriaStates.KinematicsTransition);
        //}


        if (state == FeriaStates.Gameplay1 && other.gameObject.CompareTag("Player"))
        {
            //isPlayerInside = true;
            SetFeriaState(FeriaStates.KinematicsMap);
        }
        if (state == FeriaStates.Gameplay2 && other.gameObject.CompareTag("Player"))
        {
            //isPlayerInside = true;
            SetFeriaState(FeriaStates.KinematicsTransition);
        }
        if (state == FeriaStates.Gameplay3 && other.gameObject.CompareTag("Player"))
        {
            //isPlayerInside = true;
            SetFeriaState(FeriaStates.KinematicsCarousel);
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
        CinemachineBrain _brain = cam.GetComponent<CinemachineBrain>();
        switch (state)
        {
            case FeriaStates.Intro:
                vc0.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 0;
                vcIntro.m_Priority = 2;
                anim.SetInteger("state", 1);

                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                saraAgent.enabled = false;
                break;
            case FeriaStates.Gameplay1:
                vcIntro.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                //anim.enabled = false;
                anim.SetInteger("state", 2);
                saraAgent.enabled = false;

                _trigger = transform.GetChild(1).GetChild(0).gameObject;
                _trigger.SetActive(true);
                //postProcessing.m_Profile = profiles[1];

                //MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.FollowSara);

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;

                MusicManager.instance.Play(1);
                //SoundManager.instance.Play(0);
                break;
            case FeriaStates.KinematicsMap:
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsMap.m_Priority = 2;
                anim.SetInteger("state", 3);
                saraAgent.enabled = true;

                _trigger = transform.GetChild(1).GetChild(0).gameObject;
                _trigger.SetActive(false);

                SaraAnimatorIk.instance.SetTargetPositionMap();

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                break;
            case FeriaStates.Gameplay2:
                vcKinematicsMap.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                anim.SetInteger("state", 4);
                saraAgent.enabled = false;

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;
                break;
            case FeriaStates.KinematicsTransition:
                vcThirdPersonSara.m_Priority = 0;
                vcKinematicsTransition.m_Priority = 2;
                anim.SetInteger("state", 5);
                saraAgent.enabled = false;

                _trigger = transform.GetChild(1).GetChild(1).gameObject;
                _trigger.SetActive(false);

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                SaraMovement.instance.SetHasFobia(true);
                MusicManager.instance.Play(2);
                break;
            case FeriaStates.Gameplay3:
                vcKinematicsTransition.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                //anim.enabled = false;
                anim.SetInteger("state", 6);
                saraAgent.enabled = false;
                //postProcessing.enabled = true;
                postProcessing.m_Profile = profiles[1];
                //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;

                RenderSettings.fog = true;
                //FeriaEnemies.instance.CreateHumanoids();
                _brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                break;
            case FeriaStates.KinematicsCarousel:
                vcThirdPersonSara.m_Priority = 0;
                vcCarousel.m_Priority = 2;
                PostProcessProfile _profile = vcCarousel.GetComponent<CinemachinePostProcessing>().m_Profile;
                CameraCarousel _script = vcCarousel.GetComponent<CameraCarousel>();
                _script.StartAnimation(_profile);

                _trigger = transform.GetChild(1).GetChild(2).gameObject;
                _trigger.SetActive(false);

                //SaraAnimatorIk.instance.SetTargetPositionMap();

                saraMovementScript.enabled = false;
                saraBehaviorScript.enabled = true;
                break;
            case FeriaStates.Gameplay4:
                vcKinematicsTransition.m_Priority = 0;
                vcThirdPersonSara.m_Priority = 2;
                //anim.SetInteger("state", 6);
                saraAgent.enabled = false;
                postProcessing.m_Profile = profiles[2];
                //postProcessing.enabled = true;

                saraMovementScript.enabled = true;
                saraBehaviorScript.enabled = false;
                break;
        }
    }

    public void Intro_End()
    {
        SetFeriaState(FeriaStates.Gameplay1);
    }

    public void StartDialogue1_Event()
    {
        //dialogue = LocalizationManager.instance.GetFeriaDialogueText("es", "Dialogue1_1");

        dialogue = "Mati:\r\nVoy al Corrusel.\r\n\r\nSara:\r\nNO.\r\nESPÉRAME!!!\r\n\r\n(...)\r\n\r\n(Tengo que encontrarlo.)";
        HudManager.instance.UpdateDialogue(dialogue);
    }

    public void MatiGoCarousel_Event()
    {
        MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.GoToCarousel);
        SaraAnimatorIk.instance.SetTargetPositionMati();

    }

    public void Map_End()
    {
        SetFeriaState(FeriaStates.Gameplay2);
    }

    public void Transition_End()
    {
        SetFeriaState(FeriaStates.Gameplay3);
    }

    public void Transition_Beat()
    {
        SoundManager.instance.PlayUi(2);
    }

    public void Carousel_End()
    {
        SetFeriaState(FeriaStates.Gameplay4);
    }
    #endregion

    public enum FeriaStates
    {
        Intro, Gameplay1, KinematicsMap, Gameplay2, KinematicsTransition, Gameplay3, 
        KinematicsCarousel, Gameplay4
    }
}


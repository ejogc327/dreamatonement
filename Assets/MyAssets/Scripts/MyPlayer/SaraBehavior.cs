using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

/// <summary>
///
/// </summary>

public class SaraBehavior : MonoBehaviour
{    
    #region Variables
    public static SaraBehavior instance;
    public SaraActions saraAction;

    Vector2 axis;
    Vector3 dirMove;
    float speedMove;
    public float speedRot;
    public float speedWalk;
    public float speedRun;

    public float forceJump;
    public float maxHeight;
    public float rayToBottomLength;

    public Vector3 destination;
    public Quaternion rotDestination;
    public float diff;

    Transform cam;
    Rigidbody rb;
    NavMeshAgent agent;
    Animator anim;
    Transform cmFollow;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        cmFollow = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSaraAction();
        if (saraAction == SaraActions.GoToMap)
        {
            MoveTo();
        }

        
    }

    #endregion

    #region Funciones Propias


    void MoveTo()
    {
        //Debug.Log("Est� moviendo");

        diff = (transform.position - destination).magnitude;
        if (diff <= 0.5f)
        {
            agent.isStopped = true;
            anim.SetInteger("move", 0);

            //if (rotating)
            //Quaternion _rotFinal = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].rotation;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotDestination, 400 * Time.deltaTime);
            if (transform.rotation == rotDestination)
            {
                //Debug.Log("Rot1: " + rotDestination);
                //Debug.Log("Rot2: " + transform.rotation);
                //move = false;
                SetSaraAction(SaraActions.None);
            }
        }
    }

    public void StartPositionInFeria()
    {
        transform.position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Ticket1].position;
        transform.rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Ticket1].rotation;
    }

    public void StartPositionInFeriaGameplay4()
    {
        transform.position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Entry].position + new Vector3(1f, 0f, 0f);
        transform.rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Entry].rotation;
    }
    //void Posi

    void UpdateSaraAction()
    {
        if (FeriaManager.instance.state == FeriaManager.FeriaStates.KinematicsMap && saraAction != SaraActions.GoToMap)
        {
            SetSaraAction(SaraActions.GoToMap);
        }
        if (FeriaManager.instance.state == FeriaManager.FeriaStates.KinematicsTransition && saraAction != SaraActions.Transition)
        {
            SetSaraAction(SaraActions.Transition);
        }
    }

    public void SetSaraAction(SaraActions _newAction)
    {
        saraAction = _newAction;

        switch (saraAction)
        {
            case SaraActions.None:
                //agent.isStopped = true;
                anim.SetInteger("move", 0);
                //agent.enabled = false;
                break;
            case SaraActions.GoToMap:
                destination = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Map].position;

                agent.isStopped = false;
                anim.SetInteger("move", 1);
                agent.SetDestination(destination);
                break;
            case SaraActions.Transition:
                //if (agen)
                //agent.isStopped = true;
                anim.SetInteger("move", 0);
                break;
            case SaraActions.Carousel:
                //if (agen)
                //agent.isStopped = true;
                anim.SetInteger("move", 0);
                break;
        }
    }
    #endregion

    public enum SaraActions
    {
        None,
        GoToMap,
        Transition,
        Carousel
    }
}

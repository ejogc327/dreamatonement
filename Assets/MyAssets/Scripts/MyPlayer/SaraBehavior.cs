using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static FeriaPeopleBehavior;

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
        Debug.Log("Está moviendo");

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
                Debug.Log("Rot1: " + rotDestination);
                Debug.Log("Rot2: " + transform.rotation);
                //move = false;
                SetSaraAction(SaraActions.None);
            }
        }
    }

    void UpdateSaraAction()
    {
        if (FeriaManager.instance.state == FeriaManager.FeriaStates.KinematicsMap && saraAction != SaraActions.GoToMap)
        {
            SetSaraAction(SaraActions.GoToMap);
        }
    }

    public void SetSaraAction(SaraActions _newAction)
    {
        saraAction = _newAction;

        switch (saraAction)
        {
            case SaraActions.None:
                agent.isStopped = true;
                anim.SetInteger("move", 0);
                break;
            case SaraActions.GoToMap:
                destination = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Map].position;
                Debug.Log("Posición final_ " + destination);
                agent.isStopped = false;
                anim.SetInteger("move", 1);
                agent.SetDestination(destination);
                break;
        }
    }
    #endregion

    public enum SaraActions
    {
        None,
        GoToMap,
        
    }
}

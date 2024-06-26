using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MatiBehavior : MonoBehaviour
{
    #region Variables
    public static MatiBehavior instance;
    public MoveStates moveState;
    public MatiActions matiAction;

    float speedMove;
    public float speedWalk;
    public float speedRun;
    public bool followingSara;
    public int followingSaraIndex;

    float counter;
    public TransformData destination;
    public float diff;

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
        //target = GameObject.FindWithTag("Player").transform;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    target = GameObject.FindWithTag("Player").transform;
        //    SetMoveState(MoveStates.Walking);
        //    agent.speed = speedMove;
        //    followingSaraIndex = 1;
        //    followingSara = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    target = GameObject.FindWithTag("Player").transform;
        //    SetMoveState(MoveStates.Walking);
        //    agent.speed = speedMove;
        //    followingSaraIndex = 2;
        //    followingSara = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad3))
        //{
        //    target = GameObject.FindWithTag("Player").transform;
        //    SetMoveState(MoveStates.Walking);
        //    agent.speed = speedMove;
        //    followingSaraIndex = 3;
        //    followingSara = true;
        //}
        if (matiAction == MatiActions.FollowSara)
        {
            FollowingToSara(3);
        }
        else if (matiAction == MatiActions.GoToCarousel)
        {
            diff = (destination.position - transform.position).magnitude;

            if (diff < 0.5f)
            {
                SetMatiAction(MatiActions.InCarousel);
                int _emptyHorse = CarouselBehavior.instance.FindEmptyHorse();
                if (_emptyHorse != -1)
                {
                    CarouselBehavior.instance.SetKidOnCarousel(transform, _emptyHorse);
                    CarouselBehavior.instance.StartCarousel();
                }
            }
        }


    }
    #endregion

    #region Funciones Propias

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_positionIndex">0: any, 1: behind, 2: right, 3: left</param>
    void FollowingToSara(int _positionIndex)
    {

        if (counter <= 0.25f)
        {
            counter += Time.deltaTime;
            return;
        }
            
        //FollowingToSara(followingSaraIndex);

        Vector3 _position = target.position;

        switch (_positionIndex)
        {
            case 1:
                _position = target.position - target.forward;
                break;
            case 2:
                _position = target.position + target.right;
                break;
            case 3:
                _position = target.position - target.right;
                break;
        }
        agent.SetDestination(_position);

        diff = (_position - transform.position).magnitude;

        if (moveState != MoveStates.Walking && diff <= 2.5f)
        {
            SetMoveState(MoveStates.Walking);
        }
        if (moveState != MoveStates.Running && diff > 2.5f)
        {
            SetMoveState(MoveStates.Running);
        }
        if (moveState != MoveStates.Idle && diff < 1.5f)
        {
            SetMoveState(MoveStates.Idle);
        }

        counter = 0;
    }

    public void StartPositionMati()
    {
        transform.position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Ticket1].position + Vector3.forward * 0.25f;
        transform.rotation = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Ticket1].rotation;
    }

    public void SetMatiAction(MatiActions _newAction)
    {
        matiAction = _newAction;

        switch (matiAction)
        {
            case MatiActions.None:
                agent.isStopped = true;
                anim.SetInteger("move", 0);
                break;
            case MatiActions.FollowSara:
                target = GameObject.FindWithTag("Player").transform;
                agent.enabled = true;
                followingSara = true;
                //destination = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Map].position;
                //Debug.Log("Posici�n final_ " + destination);
                //agent.isStopped = false;
                //anim.SetInteger("move", 1);
                //agent.SetDestination(destination);
                break;
            case MatiActions.GoToCarousel:
                target = null;
                agent.enabled = true;
                followingSara = false;
                destination = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Carousel];
                agent.SetDestination(destination.position);
                SetMoveState(MoveStates.Running);
                break;
            case MatiActions.InCarousel:
                agent.isStopped = true;
                SetMoveState(MoveStates.Idle);
                agent.enabled = false;
                break;
        }
    }

    public void SetMoveState(MoveStates _newState)
    {
        moveState = _newState;

        switch (moveState)
        {
            case MoveStates.Idle:
                agent.isStopped = true;
                speedMove = 0;
                anim.SetInteger("move", 0);                
                break;
            case MoveStates.Walking:
                agent.isStopped = false;
                speedMove = speedWalk;
                anim.SetInteger("move", 1);
                agent.speed = speedMove;
                break;
            case MoveStates.Running:
                agent.isStopped = false;
                speedMove = speedRun;
                anim.SetInteger("move", 2);
                agent.speed = speedMove;
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }

    public enum MatiActions
    {
        None,
        FollowSara,
        GoToCarousel,
        InCarousel,

    }
}

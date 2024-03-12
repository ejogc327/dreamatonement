using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MatiMovement : MonoBehaviour
{
    #region Variables
    public static MatiMovement instance;
    public MoveStates moveState;
    public Transform target;
    NavMeshAgent agent;
    Animator anim;

    float speedMove;
    public float speedWalk;
    public float speedRun;
    public bool followingSara;
    public int followingSaraIndex;

    float counter;
    public float diff;

    #endregion

    #region Funciones Unity
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //target = GameObject.FindWithTag("Player").transform;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            target = GameObject.FindWithTag("Player").transform;
            SetMoveState(MoveStates.Walking);
            agent.speed = speedMove;
            followingSaraIndex = 1;
            followingSara = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            target = GameObject.FindWithTag("Player").transform;
            SetMoveState(MoveStates.Walking);
            agent.speed = speedMove;
            followingSaraIndex = 2;
            followingSara = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            target = GameObject.FindWithTag("Player").transform;
            SetMoveState(MoveStates.Walking);
            agent.speed = speedMove;
            followingSaraIndex = 3;
            followingSara = true;
        }

        FollowToSara();
    }
    #endregion

    #region Funciones Propias
    void FollowToSara()
    {
        if (followingSara)
        {
            if (counter <= 0.25f)
                counter += Time.deltaTime;
            else
            {
                FollowingToSara(followingSaraIndex);
                counter = 0;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_positionIndex">0: any, 1: behind, 2: right, 3: left</param>
    void FollowingToSara(int _positionIndex)
    {
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
                break;
            case MoveStates.Running:
                agent.isStopped = false;
                speedMove = speedRun;
                anim.SetInteger("move", 2);
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }
}

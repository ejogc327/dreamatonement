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

    #endregion

    #region Funciones Unity
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            speedMove = speedWalk;
            //anim.SetInteger("move", 1);
            SetMoveState(MoveStates.Walking);
            FollowObject();
        }

    }
    #endregion

    #region Funciones Propias
    void FollowObject()
    {
        agent.speed = speedMove;
        agent.SetDestination(target.position);
    }

    public void SetMoveState(MoveStates _newState)
    {
        moveState = _newState;

        switch (moveState)
        {
            case MoveStates.Idle:
                anim.SetInteger("move", 0);
                break;
            case MoveStates.Walking:
                anim.SetInteger("move", 1);
                break;
            case MoveStates.Running:
                anim.SetInteger("move", 2);
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }
}

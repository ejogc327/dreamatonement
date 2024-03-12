using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class ThirdPersonMovement : MonoBehaviour
{    
    #region Variables
    public static ThirdPersonMovement instance;
    public MoveStates moveState;
    public JumpStates jumpState;
    Vector2 axis;
    Vector3 dirMove;
    float speedMove;
    public float speedRot;
    public float speedWalk;
    public float speedRun;

    Transform cam;
    Rigidbody rb;
    Animator anim;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetVirtualAxis();
        SetMoveDirection();
        UpdateMoveState();
        SoftedRotation();
        //Run();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMove * speedMove * Time.fixedDeltaTime);
    }

    #endregion

    #region Funciones Propias
    void SetVirtualAxis()
    {
        axis.x = Input.GetAxisRaw("Horizontal");
        axis.y = Input.GetAxisRaw("Vertical");
    }


    void SetMoveDirection()
    {
        dirMove = cam.right * axis.x + cam.forward * axis.y;
        dirMove.y = 0f;
        dirMove.Normalize();
    }


    void UpdateMoveState()
    {
        if (axis.magnitude == 0f && moveState != MoveStates.Idle)
            SetMoveState(MoveStates.Idle);
        
        if (Input.GetKey(KeyCode.LeftShift))            
        {
            speedMove = speedRun;
            if (axis.magnitude != 0f && moveState != MoveStates.Running)
                SetMoveState(MoveStates.Running);
        }
        else
        {
            speedMove = speedWalk;
            if (axis.magnitude != 0f && moveState != MoveStates.Walking)
                SetMoveState(MoveStates.Walking);
        }
        
    }

    // void Run()
    // {
    //     if (Input.GetKey(KeyCode.LeftShift))            
    //     {
    //         speedMove = speedRun;
    //         if (moveState == MoveStates.Walking)
    //             SetMoveState(MoveStates.Running);
    //     }
    //     else
    //     {
    //         speedMove = speedWalk;
    //         if (axis.magnitude != 0f && moveState == MoveStates.Running)
    //             SetMoveState(MoveStates.Walking);
    //     }
    // }

    void SoftedRotation()
    {
        if (axis.magnitude != 0f)
        {
            Quaternion _rotFinal = Quaternion.LookRotation(dirMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotFinal, speedRot * Time.deltaTime);
        }
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
    public void SetJumplingState(JumpStates _newState)
    {
        jumpState = _newState;

        switch (jumpState)
        {
            case JumpStates.Grounded:
                break;
            case JumpStates.Jumping:
                break;
            case JumpStates.Falling:
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }

    public enum JumpStates { Grounded, Jumping, Falling }
}

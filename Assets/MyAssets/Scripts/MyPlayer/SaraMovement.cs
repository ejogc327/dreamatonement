using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class SaraMovement : MonoBehaviour
{    
    #region Variables
    public static SaraMovement instance;
    public MoveStates moveState;
    public JumpStates jumpState;
    Vector2 axis;
    Vector3 dirMove;
    float speedMove;
    public float speedRot;
    public float speedWalk;
    public float speedRun;

    public float forceJump;
    public float maxHeight;
    public float rayToBottomLength;

    Transform cam;
    Rigidbody rb;
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

        cmFollow = transform.GetChild(1);
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
        if (FeriaManager.instance.IsKinematics()) return;
        SetVirtualAxis();
        SetMoveDirection();
        UpdateMoveState();
        SoftedRotation();
        Jump();
        //Run();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMove * speedMove * Time.fixedDeltaTime);
        //rb.velocity = 
        RaycastToBottom();
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
            if (axis.magnitude != 0f && moveState != MoveStates.Running)
                SetMoveState(MoveStates.Running);
        }
        else
        {
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpState == JumpStates.Grounded)
        {
            SetJumpState(JumpStates.Jumping);
        }
    }
    void RaycastToBottom()
    {
        Ray _ray = new Ray(cmFollow.position, Vector3.down);
        RaycastHit hit;
        bool result = Physics.Raycast(_ray, out hit, rayToBottomLength);

        if (result)
        {
            if (jumpState != JumpStates.Grounded && jumpState != JumpStates.Jumping)
                SetJumpState(JumpStates.Grounded);
        }
        else
        {
            if (jumpState != JumpStates.Falling && rb.velocity.y < 0f)
                SetJumpState(JumpStates.Falling);
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
                speedMove = speedWalk;
                anim.SetInteger("move", 1);
                break;
            case MoveStates.Running:
                speedMove = speedRun;
                anim.SetInteger("move", 2);
                break;
        }
    }
    public void SetJumpState(JumpStates _newState)
    {
        jumpState = _newState;

        switch (jumpState)
        {
            case JumpStates.Grounded:
                anim.SetInteger("jump", 0);
                break;
            case JumpStates.Jumping:
                rb.velocity = Vector3.zero;
                float _forceJump = Mathf.Sqrt(maxHeight * -forceJump * Physics.gravity.y * rb.mass);
                rb.AddForce(Vector3.up * _forceJump, ForceMode.VelocityChange);
                anim.SetInteger("jump", 1);
                break;
            case JumpStates.Falling:
                anim.SetInteger("jump", 2);
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }

    public enum JumpStates { Grounded, Jumping, Falling }
}

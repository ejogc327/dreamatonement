using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>

public class SaraMovement : MonoBehaviour
{    
    #region Variables
    public static SaraMovement instance;
    public MoveStates moveState;
    public JumpStates jumpState;
    public GrabStates grabState;
    public TorchStates torchState;
    public HitStates hitState;

    Vector2 axis;
    Vector3 dirMove;
    bool canMove;
    float speedMove;
    public float speedRot;
    public float speedWalk;
    public float speedRun;

    public float forceJump;
    public float maxHeight;
    public float rayToBottomLength;

    public bool isOnTorch;
    public bool hasTorch;
    public bool isOnFire;
    public bool isOnWeb;
    public bool isOnSpider;
    public bool hasFobia;

    Transform torch;
    Transform spiderWeb;
    Transform spider;

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
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            SetVirtualAxis();
            SetMoveDirection();
            UpdateMoveState();
            SoftedRotation();
            Jump();
            Grab();
        }
        //StopMovement();

        UpdateHit();
        UpdateTorch();
        //Run();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMove * speedMove * Time.fixedDeltaTime);
        //rb.velocity = 
        RaycastToBottom();
        RaycastToLeft();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            isOnTorch = true;
            torch = other.transform;
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            isOnFire = true;
        }
        if (other.gameObject.CompareTag("People"))
        {
            if (hasFobia)
            {
                var _direction = transform.InverseTransformPoint(other.transform.position); //this helps us find which direction the object collided from

                if (_direction.x > 0f)
                { //Change the axis to fit your needs
                    anim.SetInteger("someoneNext", 1);
                }
                else if (_direction.x < 0f)
                {
                    anim.SetInteger("someoneNext", 2);
                }
            }
        }

        if (other.gameObject.CompareTag("Web"))
        {
            isOnWeb = true;
            spiderWeb = other.transform;
        }

        if (other.gameObject.CompareTag("Spiders"))
        {
            isOnSpider = true;
            spider = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            isOnTorch = false;
            torch = null;
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            isOnFire = false;
        }
        if (other.gameObject.CompareTag("People"))
        {
            anim.SetInteger("someoneNext", 0);
        }
        if (other.gameObject.CompareTag("Web"))
        {
            isOnWeb = false;
            spiderWeb = null;
        }
        if (other.gameObject.CompareTag("Spiders"))
        {
            isOnSpider = false;
            spider = null;
        }
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
            if (axis.magnitude != 0f && grabState == GrabStates.None && hitState == HitStates.None && moveState != MoveStates.Running)
                SetMoveState(MoveStates.Running);
        }
        else
        {
            if (axis.magnitude != 0f && grabState == GrabStates.None && hitState == HitStates.None && moveState != MoveStates.Walking)
                SetMoveState(MoveStates.Walking);
        }
        
    }

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

    void Grab()
    {
        if (Input.GetKeyDown(KeyCode.C) && jumpState == JumpStates.Grounded && moveState == MoveStates.Idle && torchState != TorchStates.HasTorch && grabState != GrabStates.Grabbing)
        {
            SetGrabState(GrabStates.Grabbing);
            if (isOnTorch && torchState == TorchStates.None)
            {
                SetTorchState(TorchStates.GrabbingTorch);
                Debug.Log("Sara ha recogido la antorcha.");
                //hasTorch = true;
                anim.SetBool("hasTorch", true);
            }
        }

        if (torchState == TorchStates.GrabbingTorch && grabState == GrabStates.None)
        {
            SetTorchState(TorchStates.HasTorch);
        }

        //if (hasTorch && Input.GetKeyDown(KeyCode.V))
        //{
        //    hasTorch = false;
        //    anim.SetBool("grabTorch", false);
        //    Debug.Log("Sara ha soltado la antorcha");
        //}
    }

    void UpdateTorch()
    {
        if (Input.GetKeyDown(KeyCode.I) && torchState == TorchStates.HasTorch)
        {
            anim.SetTrigger("light");
            SetTorchState(TorchStates.LightingTorch);
        }

        if (Input.GetKeyDown(KeyCode.O) && torchState == TorchStates.HasTorch)
        {
            anim.SetTrigger("burnWeb");
            SetTorchState(TorchStates.BurningWeb);
        }

        //    anim.
    }

    public void SetTorch()
    {
        if (isOnTorch && torch != null)
        {
            Transform _leftHand = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            torch.SetParent(_leftHand);
            torch.localPosition = new Vector3(-0.024f, 0.07f, 0.024f);
            torch.localRotation = Quaternion.Euler(0f, 0f, 0f);
            SphereCollider _collider = torch.GetComponent<SphereCollider>();
            _collider.enabled = false;
        }
    }

    public void SetFireOnTorch()
    {
        if (isOnFire)
        {
            Torch _script = torch.GetComponent<Torch>();
            _script.LightFire(true);
        }
    }

    public void BurningWeb(int _value)
    {
        if (isOnWeb && spiderWeb != null)
        {
            SpiderWeb _script = spiderWeb.GetComponent<SpiderWeb>();
            _script.BurningWeb(_value);
            if (_value == 2)
            {
                BoxCollider _collider = spiderWeb.GetChild(0).GetComponent<BoxCollider>();
                _collider.enabled = false;
            }
        }
    }

    void UpdateHit()
    {
        if (Input.GetKeyDown(KeyCode.J) && hitState == HitStates.None)
        {
            //anim.SetTrigger("kick");
            SetHitState(HitStates.Kicking);
        }

        if (Input.GetKeyDown(KeyCode.K) && hitState == HitStates.None)
        {
            SetHitState(HitStates.Stomping);
        }

        if (Input.GetKeyDown(KeyCode.U) && hitState == HitStates.None && torchState == TorchStates.HasTorch)
        {
            SetHitState(HitStates.TorchDownward);
        }
    }
    
    public void SetHasFobia(bool _hasFobia)
    {
        hasFobia = _hasFobia;
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

    void RaycastToLeft()
    {
        Ray _ray = new Ray(cmFollow.position, cmFollow.forward);
        RaycastHit hit;
        bool result = Physics.Raycast(_ray, out hit, 1f);

        if (result)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 1f, Color.green);
        }
    }

    #endregion

    #region Machine States
    public void SetMoveState(MoveStates _newState)
    {
        moveState = _newState;

        switch (moveState)
        {
            case MoveStates.Idle:
                speedMove = 0f;
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
    public void SetGrabState(GrabStates _newState)
    {
        grabState = _newState;

        switch (grabState)
        {
            case GrabStates.None:

                canMove = true;
                // SaraGrabbing script
                break;
            case GrabStates.Grabbing:
                anim.SetTrigger("grab");
                canMove = false;
                SetMoveState(MoveStates.Idle);
                break;
        }
    }
    public void SetTorchState(TorchStates _newState)
    {
        torchState = _newState;

        switch (torchState)
        {
            case TorchStates.None:
                canMove = true;
                break;
            case TorchStates.GrabbingTorch:
                canMove = false;

                break;
            case TorchStates.HasTorch:
                canMove = true;
                break;
            case TorchStates.LightingTorch:
                canMove = false;
                SetMoveState(MoveStates.Idle);
                break;
            case TorchStates.BurningWeb:
                canMove = false;
                SetMoveState(MoveStates.Idle);
                break;
        }
    }

    public void SetHitState(HitStates _newState)
    {
        hitState = _newState;

        switch (hitState)
        {
            case HitStates.None:
                canMove = true;
                break;
            case HitStates.Punching:
                break;
            case HitStates.Kicking:
                anim.SetTrigger("kick");
                canMove = false;
                SetMoveState(MoveStates.Idle);
                //if ()
                break;
            case HitStates.Stomping:
                anim.SetTrigger("stomp");
                canMove = false;
                if (spider != null)
                {
                    SpidersBehavior _script = spider.GetComponent<SpidersBehavior>();
                    _script.SetSpiderAction(SpidersBehavior.SpiderActions.Dying);
                }
                SetMoveState(MoveStates.Idle);
                break;
            case HitStates.TorchDownward:
                anim.SetTrigger("attackDownward");
                canMove = false;
                if (spider != null)
                {
                    SpidersBehavior _script = spider.GetComponent<SpidersBehavior>();
                    _script.SetSpiderAction(SpidersBehavior.SpiderActions.DyingBurn);
                }
                SetMoveState(MoveStates.Idle);
                break;
        }
    }
    #endregion

    public enum MoveStates { Idle, Walking, Running }

    public enum JumpStates { Grounded, Jumping, Falling }

    public enum GrabStates { None, Grabbing }

    public enum TorchStates { None, GrabbingTorch, HasTorch, LightingTorch, BurningWeb, AttackDownward }

    public enum HitStates { None, Punching, Kicking, Stomping, TorchDownward }
}

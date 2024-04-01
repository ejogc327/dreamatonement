using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaraInteraction;
using static SaraMovement;

/// <summary>
///
/// </summary>

public class SaraInteraction : MonoBehaviour
{    
    #region Variables
    public static SaraInteraction instance;
    public GrabStates grabState;
    public TorchStates torchState;

    public bool isOnTorch;
    public bool hasTorch;

    Transform torch;

    Transform cam;
    Rigidbody rb;
    Animator anim;
    Transform cmFollow;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        //cam = Camera.main.transform;
        //rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        cmFollow = transform.GetChild(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
    }

    #endregion

    #region Funciones Propias

    

    public void SetGrabState(GrabStates _newState)
    {
        grabState = _newState;

        switch (grabState)
        {
            case GrabStates.None:
                if (hasTorch)
                {
                }
                // SaraGrabbing script
                break;
            case GrabStates.Grabbing:
                anim.SetTrigger("grab");
                break;
        }
    }
    public void SetTorchState(TorchStates _newState)
    {
        torchState = _newState;

        switch (torchState)
        {
            case TorchStates.None:
                break;
            case TorchStates.GrabbingTorch:
                break;
            case TorchStates.HasTorch:
                if (torch != null)
                {
                    Transform _leftHand = transform.GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                    torch.SetParent(_leftHand);
                    torch.localPosition = new Vector3(-0.024f, 0.07f, 0.024f);
                    torch.localRotation = Quaternion.Euler(0f, 90f, 0f);
                }
                break;
        }
    }
    #endregion

    public enum GrabStates { None, Grabbing }

    public enum TorchStates { None, GrabbingTorch, HasTorch }
}

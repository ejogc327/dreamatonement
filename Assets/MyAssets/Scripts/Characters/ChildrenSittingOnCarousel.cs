using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildrenSittingOnCarousel : MonoBehaviour
{
    #region Variables
    // position: 0, 0.67, -0.28
    // leftFoot position: -0.15, 0.074, 0.2
    // leftHand position: -0.054, 0.61, 0.34
    // leftHand rotation: 0, 0, 90
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftHand;
    public Transform rightHand;
    Animator anim;
    #endregion

    #region Funciones Unity
    private void Awake()
    {
        anim = GetComponent<Animator>();

        leftFoot.localPosition = new Vector3(-0.15f, 0.01f, 0.1f);
        rightFoot.localPosition = new Vector3(0.15f, 0.01f, 0.1f);
        leftHand.localPosition = new Vector3(-0.08f, 0.61f, 0.25f);
        rightHand.localPosition = new Vector3(0.08f, 0.61f, 0.25f);

        leftFoot.localRotation = Quaternion.identity;
        rightFoot.localRotation = Quaternion.identity;
        leftHand.localRotation = Quaternion.Euler(0f, 0f, 90f);
        rightHand.localRotation = Quaternion.Euler(0f, 0f, -90f);
    }

    private void Start()
    {
        leftFoot.localPosition = new Vector3(-0.15f, 0.01f, 0.1f);
        rightFoot.localPosition = new Vector3(0.15f, 0.01f, 0.1f);
        leftHand.localPosition = new Vector3(-0.08f, 0.61f, 0.25f);
        rightHand.localPosition = new Vector3(0.08f, 0.61f, 0.25f);

        leftFoot.localRotation = Quaternion.identity;
        rightFoot.localRotation = Quaternion.identity;
        leftHand.localRotation = Quaternion.Euler(0f, 0f, 90f);
        rightHand.localRotation = Quaternion.Euler(0f, 0f, -90f);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoot.position);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoot.rotation);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFoot.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFoot.rotation);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
    }
    #endregion

    #region Funciones Propias

    #endregion
}

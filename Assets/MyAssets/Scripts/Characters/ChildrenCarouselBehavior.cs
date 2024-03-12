using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildrenCarouselBehavior : MonoBehaviour
{

    #region Variables
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
    }

    private void Update()
    {
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

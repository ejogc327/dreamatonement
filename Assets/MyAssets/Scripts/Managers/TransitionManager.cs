using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class TransitionManager : MonoBehaviour
{
    #region Variables
    public static TransitionManager instance;
    Animator anim;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        anim.enabled = false; //no mostrar al iniciar
    }
    #endregion

    #region Funciones Propias
    public void Transition_FadeInStart()
    {
        anim.enabled = true;
        anim.Play("FadeIn", 0, 0f);
    }

    public void Transition_FadeInEnd()
    {
        GameManager.instance.LoadSceneDuringTransition();
        anim.Play("FadeOut", 0, 0f);
    }

    public void Transition_FadeOutEnd()
    {
        GameManager.instance.SetStateDuringTransition();
        anim.Rebind();
        anim.enabled = false;
    }

    #endregion
}

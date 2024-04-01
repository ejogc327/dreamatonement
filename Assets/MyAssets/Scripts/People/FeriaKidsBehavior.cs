using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FeriaKidsBehavior : MonoBehaviour
{
    #region 1. Variables
    public bool getOnCarousel;
    //public bool move;
    Animator anim;
    NavMeshAgent agent;
    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //destination = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //GetOnCarousel();
    }


    #endregion

    #region 3. Funciones Propias
    public void GetOnCarousel()
    {
        if (getOnCarousel)
        {
            getOnCarousel = false;

            agent.enabled = false;

            Transform _parent = GameObject.FindWithTag("CarouselParent").transform;
            transform.SetParent(_parent.GetChild(0).GetChild(0));
            //TransformData _carouselTD = FeriaBuildings.instance.GetCarouselTransformData();
            transform.localPosition = new Vector3(0f, 0.67f, -0.28f);
            transform.localRotation = Quaternion.identity;
            anim.SetInteger("move", 0);
            anim.SetBool("sit_carousel", true);
            
        }
    }


    #endregion

}

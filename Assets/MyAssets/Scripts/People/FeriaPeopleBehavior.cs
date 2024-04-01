using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FeriaPeopleBehavior : MonoBehaviour
{
    #region 1. Variables
    public PeopleActions peopleAction;
    //public Transform employeeOrigin;
    //public FeriaCharacters employeesValues;
    public int peopleActionIndex;
    public bool play;
    public bool move;
    public TransformData destination;
    //public Quaternion finalRotation;
    //public float 
    public float diff;

    public Material[] clothes;

    Transform[] employees;

    NavMeshAgent agent;
    Animator anim;

    public bool translate;
    public bool isEmployee;
    public bool isWoman;
    public bool isChild;
    public bool isOnCarouselHorse;

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
        MovePeopleTo();
        TranslatePeopleTo();
    }

    /*void OnCollisionEnter(Collision collision)
    {
        destination = Vector3.right;
        transform.Translate(destination * Time.deltaTime, Space.World);
    }*/

    #endregion

    #region 3. Funciones Propias

    void MovePeopleTo()
    {
        if (play)
        {
            play = false;
            move = true;
            //destination = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].position;
            //Vector3 _destination = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].position;

            agent.isStopped = false;
            agent.SetDestination(destination.position);

            anim.SetBool("walk", true);
            //transform.Rotate()
            //Quaternion _rotFinal = Quaternion.LookRotation(dirMove);
            //destination = _destination;
        }

        diff = (transform.position - destination.position).magnitude;
        //Debug.Log("El personaje " + transform.name + " está a " + diff + " de su objetivo.");
        if (diff <= 0.5f)
        //if (move && diff <= 0.5f)
        {
            agent.isStopped = true;
            anim.SetBool("walk", false);

            //if (rotating)
            Quaternion _rotFinal = destination.rotation;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotFinal, 400 * Time.deltaTime);
            if (transform.rotation == _rotFinal)
            {
                //Debug.Log("Rot1: " + _rotFinal);
                //Debug.Log("Rot2: " + transform.rotation);
                move = false;
                SetPeopleAction(PeopleActions.None);
            }
        }

    }

    void TranslatePeopleTo()
    {
        if (translate)
        {
            translate = false;

            if (isChild && isOnCarouselHorse)
            {
                Transform _parent = GameObject.FindWithTag("CarouselParent").transform;
                transform.SetParent(_parent.GetChild(0).GetChild(0));
                //TransformData _carouselTD = FeriaBuildings.instance.GetCarouselTransformData();
                transform.localPosition = new Vector3(0f, 0.67f, -0.28f);
                transform.localRotation = Quaternion.identity;
                anim.SetInteger("move", 0);
                anim.SetBool("sit_carousel", true);
            }
        }
    }

    void MakeCircularRoute()
    {
        //if ()
    }
    bool IsObjectHere(Vector3 _position)
    {
        Collider[] _intersecting = Physics.OverlapSphere(_position, 0.01f);
        if (_intersecting.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetWoman()
    {
        isWoman = true;
    }

    public void ChangeSuit(int _index)
    {
        if (clothes.Length == 0 || clothes[_index] == null) return;

        int _child; // Child for clothes
        if (isWoman) _child = 2; else _child = 3;
 
        SkinnedMeshRenderer _renderer = transform.GetChild(0).GetChild(_child).GetComponent<SkinnedMeshRenderer>();
        _renderer.sharedMaterial = clothes[_index];
    }

    public TransformData GoToFreeZone()
    {
        TransformData[] _transformData = new TransformData[4];

        _transformData[0] = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone1];
        _transformData[1] = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone2];
        _transformData[2] = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone3];
        _transformData[3] = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.FreeZone4];

        Vector3 _position = Vector3.zero;
        _position.x = UnityEngine.Random.Range(_transformData[0].position.x, _transformData[2].position.x);
        _position.z = UnityEngine.Random.Range(_transformData[0].position.z, _transformData[1].position.z);

        Quaternion _rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 360), 0f);

        //Debug.Log("Destination: " + _position);
        TransformData _t = new TransformData();
        _t.position = _position;
        _t.rotation = _rotation;

        return _t;
    }


    public void SetPeopleAction(PeopleActions _newAction)
    {
        peopleAction = _newAction;

        switch (peopleAction)
        {
            case PeopleActions.None:
                agent.isStopped = true;
                anim.SetBool("walk", false);
                break;
            case PeopleActions.GoToEntry:
                destination.position = FeriaBuildings.instance.peopleTransformData[(int)FeriaBuildings.PeoplePositions.Entry].position;
                destination.rotation = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].rotation;
                //Debug.Log("Posición final_ " + destination);
                agent.isStopped = false;
                anim.SetBool("walk", true);
                agent.SetDestination(destination.position);

                break;
            case PeopleActions.GoToTicket:
                //_destination = FeriaBuildings.instance.GetTicketTransformData().position;
                //agent.SetDestination(_destination);
                break;
            case PeopleActions.GoToInfo1:
                //_destination = FeriaBuildings.instance.GetInfoTransformData()[0].position;
                // agent.SetDestination(_destination);
                break;
            case PeopleActions.GoToInfo2:
                //_destination = FeriaBuildings.instance.GetInfoTransformData()[1].position;
                // agent.SetDestination(_destination);
                break;
            case PeopleActions.GoToInfo3:
                //_destination = FeriaBuildings.instance.GetInfoTransformData()[2].position;
                // agent.SetDestination(_destination);
                break;
            case PeopleActions.GoToCarousel:
                //_destination = FeriaBuildings.instance.GetCarouselTransformData().position;
                //agent.SetDestination(_destination);
                break;
            case PeopleActions.TranslateToCarousel:
                //transform.Translate()
                break;
            case PeopleActions.MakeCircularRoute:
                break;
            case PeopleActions.GoToFreeZone:
                destination = GoToFreeZone();
                //Debug.Log("Posición final_ " + destination);
                agent.isStopped = false;
                anim.SetBool("walk", true);
                //Debug.Log("Destination: " + destination);
                agent.SetDestination(destination.position);
                break;
        }
    }
    #endregion

    public enum PeopleActions
    {
        None,
        GoToEntry,
        GoToTicket,
        GoToInfo1,
        GoToInfo2,
        GoToInfo3,
        GoToCarousel,
        MakeCircularRoute,
        TranslateToCarousel,
        GoToFreeZone
    }
}

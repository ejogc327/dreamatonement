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
    public Vector3 destination;
    public float diff;

    Transform[] employees;

    NavMeshAgent agent;
    Animator anim;

    public bool translate;
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
        destination = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        MovePeopleTo();
        TranslatePeopleTo();
    }

    #endregion

    #region 3. Funciones Propias

    void MovePeopleTo()
    {
        if (play)
        {
            play = false;
            move = true;
            Vector3 _destination = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].position;

            agent.isStopped = false;
            agent.SetDestination(_destination);

            anim.SetBool("walk", true);
            //transform.Rotate()
            //Quaternion _rotFinal = Quaternion.LookRotation(dirMove);
            destination = _destination;
        }

        diff = (transform.position - destination).magnitude;
        if (move && diff <= 0.5f)
        {
            agent.isStopped = true;
            anim.SetBool("walk", false);

            //if (rotating)
            Quaternion _rotFinal = FeriaBuildings.instance.employeesTransformData[peopleActionIndex].rotation;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotFinal, 400 * Time.deltaTime);
            if (transform.rotation == _rotFinal)
            {
                Debug.Log("Rot1: " + _rotFinal);
                Debug.Log("Rot2: " + transform.rotation);
                move = false;
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


    void SetPeopleAction(PeopleActions _newAction)
    {
        peopleAction = _newAction;

        //Vector3 _destination;
        switch (peopleAction)
        {
            case PeopleActions.None:
                agent.isStopped = true;
                anim.SetBool("walk", false);
                break;
            case PeopleActions.GoToEntry:
                //_destination = FeriaBuildings.instance.GetEntryTransformData().position;
                // agent.SetDestination(_destination);
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
        TranslateToCarousel
    }
}

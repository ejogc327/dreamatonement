using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FeriaEmployeeBehavior : MonoBehaviour
{
    #region 1. Variables
    public EmployeesActions employeesAction;
    //public Transform employeeOrigin;
    //public FeriaCharacters employeesValues;
    public int employeesActionIndex;
    public bool play;
    public bool move;
    public Vector3 destination;
    public float diff;

    Transform[] employees;

    NavMeshAgent agent;
    Animator anim;

    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        //anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        destination = Vector3.zero;
        agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.isStopped = true;
        UpdateEmployeesActions();
    }

    #endregion

    #region 3. Funciones Propias

    void UpdateEmployeesActions()
    {
        if (play)
        {
            play = false;
            move = true;
            Vector3 _destination = FeriaBuildings.instance.employeesTransformData[employeesActionIndex].position;

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
            Quaternion _rotFinal = FeriaBuildings.instance.employeesTransformData[employeesActionIndex].rotation;
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotFinal, 400 * Time.deltaTime);
            if (transform.rotation == _rotFinal)
            {
                Debug.Log("Rot1: " + _rotFinal);
                Debug.Log("Rot2: " + transform.rotation);
                move = false;
            }
        }

       /* if (workersAction != WorkersActions.GoToEntry && workersActionIndex == 0)
        {
            SetWorkersAction(WorkersActions.GoToEntry);
        }
        else if (workersAction != WorkersActions.GoToTicket && workersActionIndex == 2)
        {
            SetWorkersAction(WorkersActions.GoToTicket);
        }
        else if (workersAction != WorkersActions.GoToInformation1 && workersActionIndex == 3)
        {
            SetWorkersAction(WorkersActions.GoToInformation1);
        }
        else if (workersAction != WorkersActions.GoToInformation2 && workersActionIndex == 4)
        {
            SetWorkersAction(WorkersActions.GoToInformation2);
        }
        else if (workersAction != WorkersActions.GoToInformation3 && workersActionIndex == 5)
        {
            SetWorkersAction(WorkersActions.GoToInformation3);
        }
        else if (workersAction != WorkersActions.GoToCarrousel && workersActionIndex == 5)
        {
            SetWorkersAction(WorkersActions.GoToCarrousel);
        }*/

    }

    void SetEmployeesAction(EmployeesActions _newAction)
    {
        employeesAction = _newAction;

        //Vector3 _destination;
        /*switch (workersAction)
        {
            case WorkersActions.None:
                break;
            case WorkersActions.GoToEntry:
                _destination = FeriaBuildings.instance.GetEntryTransformData().position;
                agent.SetDestination(_destination);
                break;
            case WorkersActions.GoToTicket:
                _destination = FeriaBuildings.instance.GetTicketTransformData().position;
                agent.SetDestination(_destination);
                break;
            case WorkersActions.GoToInformation1:
                _destination = FeriaBuildings.instance.GetInfoTransformData()[0].position;
                agent.SetDestination(_destination);
                break;
            case WorkersActions.GoToInformation2:
                _destination = FeriaBuildings.instance.GetInfoTransformData()[1].position;
                agent.SetDestination(_destination);
                break;
            case WorkersActions.GoToInformation3:
                _destination = FeriaBuildings.instance.GetInfoTransformData()[2].position;
                agent.SetDestination(_destination);
                break;
            case WorkersActions.GoToCarrousel:
                _destination = FeriaBuildings.instance.GetCarouselTransformData().position;
                agent.SetDestination(_destination);
                break;
        }*/
    }
    #endregion

    public enum EmployeesActions
    {
        None,
        GoToEntry,
        GoToTicket,
        GoToInformation1,
        GoToInformation2,
        GoToInformation3,
        GoToCarrousel
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpidersBehavior : MonoBehaviour
{
    #region 1. Variables
    public SpiderActions spiderAction;

    public bool play;
    public bool move;
    public TransformData destination;
    public float diff;
    float counter;

    public Vector3 initialPosition;
    public Transform sara;
    int numAttack;

    NavMeshAgent agent;
    Animator anim;


    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //anim = transform.GetChild(0).GetComponent<Animator>();
        sara = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        //destination = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (spiderAction == SpiderActions.MoveToSara)
        {
            UpdateSpiderAction();
            MovingToSara();
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        destination = Vector3.right;
        transform.Translate(destination * Time.deltaTime, Space.World);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ha detectado a Sara");
            SetSpiderAction(SpiderActions.MoveToSara);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ha dejado de detectar a Sara");
            SetSpiderAction(SpiderActions.BackToInitialPosition);
        }
    }
    #endregion

    #region 3. Funciones Propias

    public void UpdateSpiderAction()
    {
        diff = (transform.position - sara.position).magnitude;

        if (diff <= 0.5f)
        {
            SetSpiderAction(SpiderActions.Attacking);
        }
    }

    public void MovingToSara()
    {
        if (counter < 0.25f)
        {
            counter++;
        }
        else
        {
            agent.SetDestination(sara.position);
            counter = 0;
        }
         
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(2f);
        numAttack++;
        Debug.Log("Atacando..." + numAttack);
        SetSpiderAction(SpiderActions.MoveToSara);
    }

    public void SetSpiderAction(SpiderActions _newAction)
    {
        spiderAction = _newAction;

        switch (spiderAction)
        {
            case SpiderActions.None:
                break;
            case SpiderActions.MoveToSara:
                agent.isStopped = false;
                StopCoroutine(WaitAttack());
                break;
            case SpiderActions.BackToInitialPosition:
                agent.isStopped = false;
                agent.SetDestination(initialPosition);
                break;
            case SpiderActions.Attacking:
                agent.isStopped = true;
                StartCoroutine(WaitAttack());
                PlayerDataManager.instance.SubstractLife(2f);
                break;
            case SpiderActions.Dying:
                agent.isStopped = true;
                break;
        }
    }
    #endregion

    public enum SpiderActions
    {
        None,
        MoveToSara,
        BackToInitialPosition,
        Attacking,
        Dying
    }
}

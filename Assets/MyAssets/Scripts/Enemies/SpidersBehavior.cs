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
    public float attackDistance;
    float counter;
    bool isOnAttack;
    public Vector3 initialPosition;
    public Transform sara;
    int numAttack;
    public int life;

    NavMeshAgent agent;
    //Animator anim;
    Animation anim;


    #endregion

    #region 2. Funciones Unity
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animation>();
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
        if (spiderAction == SpiderActions.Dying)
            return;
        if (spiderAction == SpiderActions.MoveToSara)
        {
            UpdateSpiderAction();
            MovingToSara();
        }
    }

    private void FixedUpdate()
    {
        RaycastToFront();
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

        if (diff <= attackDistance && (spiderAction == SpiderActions.Idle || spiderAction == SpiderActions.MoveToSara))
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
        if (isOnAttack)
            PlayerDataManager.instance.SubstractLife(1f);

        Debug.Log("Atacando..." + numAttack);
        SetSpiderAction(SpiderActions.MoveToSara);
    }

    public void IsAttacked(int _damage)
    {
        if (life > 0)
            life -= _damage;
        if (life <= 0)
            SetSpiderAction(SpiderActions.Dying);
    }

    void RaycastToFront()
    {
        Ray _ray = new Ray(transform.position + new Vector3(0f, 0.1f, 0f), transform.forward);
        RaycastHit _hit;
        bool _result = Physics.Raycast(_ray, out _hit, 0.3f);

        if (_result)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _hit.distance, Color.red);
            if (_hit.transform.CompareTag("Player"))
            {
                isOnAttack = true;
            }
            else isOnAttack = false;
        }
        else
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 0.3f, Color.green);
            isOnAttack = false;
        }
    }

    public void SetSpiderAction(SpiderActions _newAction)
    {
        spiderAction = _newAction;

        switch (spiderAction)
        {
            case SpiderActions.Idle:
                anim.Play("idle");
                break;
            case SpiderActions.MoveToSara:
                agent.isStopped = false;
                anim.Play("walk");
                StopCoroutine(WaitAttack());
                break;
            case SpiderActions.BackToInitialPosition:
                agent.isStopped = false;
                anim.Play("walk");
                agent.SetDestination(initialPosition);
                break;
            case SpiderActions.Attacking:
                agent.isStopped = true;
                anim.Play("attack1");
                StartCoroutine(WaitAttack());
                break;
            case SpiderActions.Dying:
                anim.Play("death2");
                transform.GetChild(1).gameObject.SetActive(true);
                agent.isStopped = true;
                transform.GetComponent<SphereCollider>().enabled = false;
                enabled = false;
                break;
            case SpiderActions.DyingBurn:
                anim.Play("death1");
                transform.GetChild(1).gameObject.SetActive(true);
                agent.isStopped = true;
                transform.GetComponent<SphereCollider>().enabled = false;
                enabled = false;
                break;
        }
    }
    #endregion

    public enum SpiderActions
    {
        Idle,
        MoveToSara,
        BackToInitialPosition,
        Attacking,
        Dying,
        DyingBurn
    }
}

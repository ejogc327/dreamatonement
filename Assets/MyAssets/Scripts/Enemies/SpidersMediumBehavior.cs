using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpidersMediumBehavior : MonoBehaviour
{
    #region 1. Variables
    public SpiderActions spiderAction;

    public bool play;
    public bool move;
    public TransformData destination;
    public float diff;
    public float attackDistance;
    float counter;
    public Vector3 initialPosition;
    public Transform sara;
    int numAttack;
    public int powerAttack;
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

        if (diff <= attackDistance)
        {
            if (SaraMovement.instance.spiderMedium == null)
                SaraMovement.instance.spiderMedium = transform;
        }
        else
        {
            if (SaraMovement.instance.spiderMedium == transform)
                SaraMovement.instance.spiderMedium = null;
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
        PlayerDataManager.instance.SubstractLife(powerAttack);
        SetSpiderAction(SpiderActions.MoveToSara);
    }

    public void IsAttacked(int _damage)
    {
        if (life > 0)
            life -= _damage;
        if (life <= 0)
            SetSpiderAction(SpiderActions.Dying);
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

                if (SaraMovement.instance.spiderMedium == transform)
                    SaraMovement.instance.spiderMedium = null;
                break;
            case SpiderActions.DyingBurn:
                anim.Play("death1");
                transform.GetChild(1).gameObject.SetActive(true);
                agent.isStopped = true;
                transform.GetComponent<SphereCollider>().enabled = false;
                enabled = false;

                if (SaraMovement.instance.spiderMedium == transform)
                    SaraMovement.instance.spiderMedium = null;
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

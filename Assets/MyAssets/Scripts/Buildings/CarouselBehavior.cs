using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// DESCRIPCIÓN:
/// Script con el funcionamiento de la moneda.
/// </summary>
public class CarouselBehavior : MonoBehaviour
{
    #region 1. Variables
    public static CarouselBehavior instance;
    public bool[] isBusyHorse = new bool[16];

    #endregion

    #region 2. Funciones Unity
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //Transform kid0 = GameObject.FindWithTag("CharactersParent").transform.GetChild(2).GetChild(1);


            //Transform _parent = GameObject.FindWithTag("CarouselParent").transform;
            //transform.SetParent(_parent.GetChild(0).GetChild(0));
            ////TransformData _carouselTD = FeriaBuildings.instance.GetCarouselTransformData();
            //transform.localPosition = new Vector3(0f, 0.67f, -0.28f);
            //transform.localRotation = Quaternion.identity;
            //anim.SetInteger("move", 0);
            //anim.SetBool("sit_carousel", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Mati"))
        {
            Debug.Log("Mati ha ingresado al carrusel");
            bool _isMatiInHorse;
            for (int i = 0; i < isBusyHorseInt.Length; i++)
            {
                if (isBusyHorseInt[i]) continue;

                Debug.Log("Mati se subirá en el caballo interior " + (i + 1));
                MatiBehavior.instance.SetMatiAction(MatiBehavior.MatiActions.InCarousel);
                other.transform.SetParent(transform.GetChild(0).GetChild(1).GetChild(1).GetChild(i).GetChild(0));
                other.transform.localRotation = Quaternion.identity;
                other.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                break;

            }
        }*/
    }
    #endregion

    #region 3. Funciones Propias
    public void SetKidOnCarousel(Transform _kid, int _numHorse)
    {
        NavMeshAgent _agent = _kid.GetComponent<NavMeshAgent>();
        _agent.enabled = false;


        //Transform _parent = GameObject.FindWithTag("CarouselParent").transform;
        _kid.SetParent(transform.GetChild(0).GetChild(1).GetChild(1).GetChild(_numHorse).GetChild(0));
        //TransformData _carouselTD = FeriaBuildings.instance.GetCarouselTransformData();
        _kid.localPosition = new Vector3(0f, 0.67f, -0.28f);
        _kid.localRotation = Quaternion.identity;
        Animator _anim = _kid.GetChild(0).GetComponent<Animator>();
        //_anim.SetInteger("move", 0);
        Rigidbody _rb = _kid.GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeAll;

        ChildrenSittingOnCarousel _script = _kid.GetChild(0).GetComponent<ChildrenSittingOnCarousel>();
        _script.isAnimatorIk = true;
        _anim.SetBool("sit_carousel", true);
        isBusyHorse[_numHorse] = true;
    }

    public int FindEmptyHorse()
    {
        for (int i = 0; i < isBusyHorse.Length; i++)
        {
            if (isBusyHorse[i]) continue;

            return i;
        }
        return -1;
    }

    public void StartCarousel()
    {
        StartCoroutine(CarouselCoroutine());
    }

    IEnumerator CarouselCoroutine()
    {
        yield return new WaitForSeconds(2f);
        CarouselRotation.instance.play = true;
    }
    #endregion

}

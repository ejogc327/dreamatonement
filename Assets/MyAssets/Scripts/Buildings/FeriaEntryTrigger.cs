using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeriaEntryTrigger : MonoBehaviour
{
    public bool isPlayerInside;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}

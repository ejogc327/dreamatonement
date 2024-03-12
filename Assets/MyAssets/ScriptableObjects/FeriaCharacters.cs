using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FeriaCharacters", menuName = "Characters/Feria")]
public class FeriaCharacters : ScriptableObject
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Transform model;
}

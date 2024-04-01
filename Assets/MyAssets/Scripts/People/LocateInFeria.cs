using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class LocateInFeria : MonoBehaviour
{
    #region 1. Variables
    public Transform characterOrigin;
    public Transform workerOrigin;

    float[] charactersRotation;
    float[] charactersScale;
    int numCharacters;
    int numWorkers;
    #endregion

    #region 2. Funciones Unity

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCharactersValues();
        LocateCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region 3. Funciones Propias
    void CreateWorkers()
    {
        for (int i = 0; i < numWorkers; i++)
        {
            Vector3 _pos = Vector3.zero;
            Quaternion _rot = Quaternion.identity;
            Transform _workerClone = Instantiate(workerOrigin, _pos, _rot);
            _workerClone.SetParent(transform.GetChild(0).transform);
        }
    }

    void SetCharactersValues()
    {
        numCharacters = 7;
        charactersPosition = new Vector2[] {
            new Vector2(9, 12), 
            new Vector2(15, 12), 
            new Vector2(21, 12),
            new Vector2(9, 0),
            new Vector2(15, 0),
            new Vector2(21, 0),
            new Vector2(9, -12)
        };
        charactersRotation = new float[] { 90f, -90f, 0f, 180f, 90f, -90f, 0f };
        charactersScale = new float[] { 0.8f, 1f, 0.8f, 0.7f, 0.8f, 1f, 0.8f };
    }
    void LocateCharacters()
    {
        for (int i = 0; i < numCharacters; i++)
        {
            Vector3 _position = new Vector3(charactersPosition[i].x, 0f, charactersPosition[i].y);
            Quaternion _rotation = Quaternion.Euler(0f, charactersRotation[i], 0f);
            Transform characterClone = Instantiate(characterOrigin, _position, _rotation);
            characterClone.SetParent(transform);
            characterClone.transform.localScale = new Vector3(charactersScale[i], charactersScale[i], charactersScale[i]);
        }
    }
    #endregion

    Vector2[] charactersPosition = new Vector2[]
    {       
        new Vector2(9, 12),
        new Vector2(15, 12),
        new Vector2(21, 12),
        new Vector2(9, 0),
        new Vector2(15, 0),
        new Vector2(21, 0),
        new Vector2(9, -12)
    };

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
///
/// </summary>

public class PlayerDataManager : MonoBehaviour
{
    #region Variables
    public static PlayerDataManager instance;
    public PlayerData playerData;
    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData.maxLife = 100f;
        playerData.lifeActual = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Funciones Propias
    public void AddLife(float _increase)
    {
        playerData.lifeActual += _increase;
        if (playerData.lifeActual > playerData.maxLife)
            playerData.lifeActual = playerData.maxLife;

        HudManager.instance.UpdateLifeBar();
    }


    public void SubstractLife(float _decrease)
    {
        playerData.lifeActual -= _decrease;
        if (playerData.lifeActual <= 0f)
        {
            playerData.lifeActual = 0f;
            //GameManager.instance.SetGameState(GameState.GameOver);
        }
        HudManager.instance.UpdateLifeBar();


    }

    #endregion
}

[Serializable]
public class PlayerData
{
	public float lifeActual;
	public float maxLife;
}

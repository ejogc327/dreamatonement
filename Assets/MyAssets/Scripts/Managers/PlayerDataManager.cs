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
        playerData.maxLife = 100;
        playerData.lifeActual = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Funciones Propias
    public void AddLife(int _increase)
    {
        playerData.lifeActual += _increase;
        if (playerData.lifeActual > playerData.maxLife)
            playerData.lifeActual = playerData.maxLife;

        HudManager.instance.UpdateLifeBar();
    }


    public void SubstractLife(int _decrease)
    {
        playerData.lifeActual -= _decrease;
        if (playerData.lifeActual <= 0)
        {
            playerData.lifeActual = 0;
            GameManager.instance.InfoTransition(3, GameStates.GameOver);
            GameManager.instance.SetGameState(GameStates.Loading);
        }
        HudManager.instance.UpdateLifeBar();
    }

    #endregion
}

[Serializable]
public class PlayerData
{
	public int lifeActual;
	public int maxLife;
}

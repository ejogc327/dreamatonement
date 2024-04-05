using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class GameOverManager : MonoBehaviour
{
    #region Variables
    public static GameOverManager instance;

    public GameObject panelHall;

    float timer;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        panelHall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Funciones Propias
    public void BtnExit_Pressed()
    {
        SoundManager.instance.PlayUi(0, 1f);
        //GameManager.instance.SetLevel(Levels.Level1);
        GameManager.instance.InfoTransition(0, GameStates.MainMenu);
        GameManager.instance.SetGameState(GameStates.Loading);
    }
    #endregion
}

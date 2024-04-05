using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class MainMenuManager : MonoBehaviour
{
    #region Variables
    public static MainMenuManager instance;

    public GameObject panelHall;
    public GameObject panelConfirmExit;

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
        panelConfirmExit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Funciones Propias
    public void BtnStart_Pressed()
    {
        SoundManager.instance.PlayUi(0, 1f);
        GameManager.instance.SetLevel(Levels.Level1);
        GameManager.instance.InfoTransition(1, GameStates.Gameplay);
        GameManager.instance.SetGameState(GameStates.Loading);
    }

    public void BtnExit_Pressed()
    {
        SoundManager.instance.PlayUi(0, 1f);
        panelHall.SetActive(false);
        panelConfirmExit.SetActive(true);
    }

    public void BtnConfirmExit_Pressed()
    {
        Application.Quit();
    }

    public void BtnCancelExit_Pressed()
    {
        SoundManager.instance.PlayUi(1, 1f);        
        panelHall.SetActive(true);
        panelConfirmExit.SetActive(false);
    }

    IEnumerator BtnExit_Coroutine()
    {
        yield return new WaitForSeconds(1.2f);
        panelHall.SetActive(false);
        panelConfirmExit.SetActive(true);
    }


    #endregion
}

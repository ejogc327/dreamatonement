using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>

public class PauseMenuManager : MonoBehaviour
{
    #region Variables
    public static PauseMenuManager instance;
    public GameObject panelHall;
    public GameObject panelInventory;
    public GameObject panelConfirmRestart;
    public GameObject panelConfirmExit;

    public int scenePaused;
    public GameStates gameStatePaused;

    GameObject pauseMenu;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        instance = this;
        pauseMenu = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        HideAllPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Funciones Propias
    public void HideAllPanels()
    {
        pauseMenu.SetActive(false);
        panelHall.SetActive(false);
        panelInventory.SetActive(false);
        // panelConfirmRestart.SetActive(false); 
        panelConfirmExit.SetActive(false);
    }

    public void ShowPanelHall()
    {
        pauseMenu.SetActive(true);
        panelHall.SetActive(true);
        panelInventory.SetActive(false);
        //panelConfirmRestart.SetActive(false); 
        panelConfirmExit.SetActive(false);
    }
    public void ShowPanelInventory()
    {
        pauseMenu.SetActive(true);
        panelHall.SetActive(false);
        panelInventory.SetActive(true);
        //panelConfirmRestart.SetActive(false); 
        panelConfirmExit.SetActive(false);
    }

    void ShowPanelConfirmRestart()
    {
        panelHall.SetActive(false);
        panelInventory.SetActive(false);
        //panelConfirmRestart.SetActive(true); 
        panelConfirmExit.SetActive(false);
    }

    void ShowPanelConfirmExit()
    {
        panelHall.SetActive(false);
        panelInventory.SetActive(false);
        //panelConfirmRestart.SetActive(false); 
        panelConfirmExit.SetActive(true);
    }


    public void InfoTransition(int _scene, GameStates _state)
    {
        scenePaused = _scene;
        gameStatePaused = _state;
    }


    public void BtnContinue_Pressed()
    {
        HideAllPanels();
        GameManager.instance.SetGameState(GameStates.Gameplay);
    }

    public void BtnRestart_Pressed()
    {
        ShowPanelConfirmRestart();
    }

    public void BtnInventory_Pressed()
    {
        ShowPanelInventory();
    }

    public void BtnExit_Pressed()
    {
        ShowPanelConfirmExit();
    }

    public void BtnInventoryPausa_Pressed()
    {
        ShowPanelHall();
    }
    public void BtnInventoryContinue_Pressed()
    {
        HideAllPanels();
        GameManager.instance.SetGameState(GameStates.Gameplay);
    }

    public void BtnConfirmRestart_Pressed()
    {
        MusicManager.instance.Restart();
        GameManager.instance.InfoTransition(1, GameStates.Gameplay);
        GameManager.instance.SetGameState(GameStates.Loading);
    }

    public void BtnCancelRestart_Pressed()
    {
        ShowPanelHall();
    }

    public void BtnConfirmExit_Pressed()
    {
        GameManager.instance.InfoTransition(0, GameStates.MainMenu);
        GameManager.instance.SetGameState(GameStates.Loading);
    }

    public void BtnCancelExit_Pressed()
    {
        ShowPanelHall();
    }

    #endregion
}

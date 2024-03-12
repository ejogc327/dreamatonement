using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;
    public GameStates gameState;
    public int sceneLoadedAfterTransition;
    public GameStates gameStateAfterTransition;
    public Levels levelActual;

    #endregion

    #region Funciones Unity

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            transform.GetChild(0).gameObject.SetActive(true); //Activa OtherManagers
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInitialState();
    }

    // Update is called once per frame
    void Update()
    {
        NavigateBetweenScenes();
        SetPausedState();
    }

    #endregion

    #region Funciones Propias
    void SetInitialState()
    {
        int _scene = SceneManager.GetActiveScene().buildIndex;
        if (_scene == 0)
            SetGameState(GameStates.MainMenu);
        if (_scene == 1)
        {
            levelActual = Levels.Level1;
            SetGameState(GameStates.Playing);
        }
        if (_scene == 2)
        {
            levelActual = Levels.Level2;
            SetGameState(GameStates.Playing);
        }
    }


    void NavigateBetweenScenes()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (gameState == GameStates.MainMenu)
            {
                SetLevel(Levels.Level1);
                InfoTransition(1, GameStates.Playing);
                SetGameState(GameStates.Loading);
            }
            else if (gameState == GameStates.PauseMenu && levelActual == Levels.Level1)
            {
                SetLevel(Levels.Level2);
                InfoTransition(2, GameStates.Playing);
                SetGameState(GameStates.Loading);
            }
            else if (gameState == GameStates.PauseMenu && levelActual == Levels.Level2)
            {
                SetLevel(Levels.None);
                InfoTransition(0, GameStates.MainMenu);
                SetGameState(GameStates.Loading);
            }
        }
    }

    void SetPausedState()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameState == GameStates.Playing)
            {
                SetGameState(GameStates.PauseMenu);
            }
            else if (gameState == GameStates.PauseMenu)
            {
                SetGameState(GameStates.Playing);
            }
        }
    }

    public void InfoTransition(int _scene, GameStates _state)
    {
        sceneLoadedAfterTransition = _scene;
        gameStateAfterTransition = _state;
    }

    public void LoadSceneDuringTransition()
    {
        SceneManager.LoadScene(sceneLoadedAfterTransition);
    }

    public void SetStateDuringTransition()
    {
        SetGameState(gameStateAfterTransition);
    }

    public void SetLevel(Levels _newLevel)
    {
        levelActual = _newLevel;
    }

    public void SetGameState(GameStates _newState)
    {
        gameState = _newState;

        switch (gameState)
        {
            case GameStates.MainMenu:
			    Time.timeScale = 1f; //velocidad de la simulación
                break; 
            case GameStates.Loading:
                Time.timeScale = 1f;
                TransitionManager.instance.Transition_FadeInStart();

                if (gameStateAfterTransition == GameStates.Playing)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else if (gameStateAfterTransition == GameStates.MainMenu)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                break;
            case GameStates.Playing:
                Time.timeScale = 1f; 

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PauseMenuManager.instance.HideAllPanels();
                break;
            case GameStates.PauseMenu:
                Time.timeScale = 0f; 

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                PauseMenuManager.instance.ShowPanelHall();
                break;
        }
    }
    #endregion    
}

public enum GameStates 
{ 
    MainMenu, 
    Loading, 
    Playing,
    PauseMenu 
}

public enum Levels
{
    None,
    Level1,
    Level2
}

public struct TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}
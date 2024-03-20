using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    // Definición instancia singleton
    static private GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private PlayerInventory inventory;

    [SerializeField]
    private SaveData save;

    [SerializeField]
    private GameState _state;
    public GameState State => _state;

    private UnityEvent<GameState> _gameStateChanged = new UnityEvent<GameState>();
    public UnityEvent<GameState> GameStateChanged => _gameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {
            case GameState.StartScreen:
                break;
            case GameState.Config:
                break;
            case GameState.Day:
                SaveProgress();
                break;
            case GameState.Night:
                break;
            case GameState.Pause:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException
                (
                    nameof(newState), 
                    newState, 
                    "GameState not included in GameManager statemachine"
                );
        }

        _gameStateChanged.Invoke(newState);
    }

    public void UpdateGamestateByInt(int state)
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        MenuManager.Instance.CloseMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SaveProgress()
    {
        //TO DO
    }

    // Aplicación de singletón
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    void Start()
    {
        inventory.Reset();
    }

    void OnValidate()
    {
        _gameStateChanged.Invoke(State);
    }
}

/// <summary>
/// Posibles estados del juego
/// </summary>
public enum GameState
{
    StartScreen = 0,
    Config = 1,
    Day = 2,
    Night = 3,
    Pause = 4,
    Lose = 5,
    Win = 6
}


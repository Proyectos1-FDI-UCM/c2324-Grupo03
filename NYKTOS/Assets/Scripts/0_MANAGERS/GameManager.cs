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
    StartScreen,
    Config,
    Day,
    Night,
    Pause,
    Lose,
    Win
}


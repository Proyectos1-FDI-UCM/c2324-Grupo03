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
    private GameState _state;
    public GameState State => _state;

    private UnityEvent<GameState> _gameStateChanged = new UnityEvent<GameState>();
    public UnityEvent<GameState> GameStateChanged => _gameStateChanged;

    [SerializeField]
    private InversionEffect _inversionEffect;

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
                _inversionEffect.Invert(false);
                break;
            case GameState.Night:
                _inversionEffect.Invert(true);
                break;
            case GameState.Pause:
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
        _state = GameState.StartScreen;
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
    Pause
}


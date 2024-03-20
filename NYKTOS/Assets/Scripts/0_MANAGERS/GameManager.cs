using System;
using System.Timers;
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

    // esto está para debug unicamente
    [SerializeField]
    private GameState _state;
    public GameState State => _state;

    private UnityEvent<GameState> _gameStateChanged = new UnityEvent<GameState>();
    public UnityEvent<GameState> GameStateChanged => _gameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        if(_state != newState)
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
                case GameState.TutorialDay:
                    break;
                case GameState.TutorialNight:
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
    }

    public void UpdateGameState(int state)
    {
        UpdateGameState(state);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveProgress()
    {
        //TO DO
    }

    private void LoadProgress()
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

        Debug.Log("[GAME MANAGER]: SERIALZIED FOR DEBUGGING");
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
    TutorialDay = 4,
    TutorialNight = 5, 
    Pause = 6,
    Lose = 7,
    Win = 8
}


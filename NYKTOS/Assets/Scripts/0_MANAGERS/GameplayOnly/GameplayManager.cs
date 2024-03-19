using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{

    #region references
    [SerializeField]
    private SaveData _saveData;

    [SerializeField]
    private Dictionary<SpawnerType, EnemySpawner> _spawnerList;

    [SerializeField]
    private NightWave[] _nightList;

    #endregion

    #region parameters

    [SerializeField]
    private int _nightLength = 180;

    private int _registeredSpawners = 0;

    [SerializeField]
    private int _maxEnemies = 5;
    public int MaxEnemies
    {
        get{return _maxEnemies;}
    }

    private int _concurrentEnemies = 0;
    public int ConcurrentEnemies
    {
        get{return _concurrentEnemies;}
    }
    public void AddConcurrentEnemy()
    {
        _concurrentEnemies++;
    }

    public void RemoveConcurrentEnemy()
    {
        _concurrentEnemies--;
    }

    private int _currentWaveNumber = 0;

    #endregion

    #region Crystals
    private int _currentRequiredYellow;
    public int CurrentRequiredYellow
    {
        get { return _currentRequiredYellow; }
        set { _currentRequiredYellow = value; }
    }

    private int _currentRequiredCyan;
    public int CurrentRequiredCyan
    {
        get { return _currentRequiredCyan; }
        set { _currentRequiredCyan = value; }
    }

    private int _currentRequiredMagenta;
    public int CurrentRequiredMagenta
    {
        get { return _currentRequiredMagenta; }
        set { _currentRequiredMagenta = value; }
    }

    private int _probabilityYellow;
    public int ProbabilityYellow
    {
        get { return _probabilityYellow; }
    } 
    private int _probabilityCyan;
    public int ProbabilityCyan
    {
        get { return _probabilityCyan; }
    }
    private int _probabilityMagenta;
    public int ProbabilityMagenta
    {
        get { return _probabilityMagenta; }
    }
    #endregion

    static private GameplayManager _instance;
    public static GameplayManager Instance
    {
        get { return _instance; }
    }

    private UnityEvent _stopSpawners = new UnityEvent();
    public UnityEvent StopSpawners => _stopSpawners;

    public void RegisterSpawner()
    {
        _registeredSpawners++;
    }

    public void UnregisterSpawner()
    {
        _registeredSpawners--;

        if(_registeredSpawners <= 0)
        {
            if(_currentWaveNumber < _nightList[_saveData.Night].waves.Length)
            {
                _currentWaveNumber ++;
            }

            InitializeWave();
        }
    }

    public void StartNight()
    {
        GameManager.Instance.UpdateGameState(GameState.Night);
        Invoke(nameof(EndNight), _nightLength);
        
        // Aquí se debería inicializar el reloj

        NightWave currentnight = _nightList[_saveData.Night];

        _currentRequiredYellow = currentnight.RequiredYellowCrystals;
        _currentRequiredCyan = currentnight.RequiredCyanCrystals;
        _currentRequiredMagenta = currentnight.RequiredMagentaCrystals;
        _probabilityYellow = currentnight.YellowProbability;
        _probabilityCyan = currentnight.CyanProbability;
        _probabilityMagenta = currentnight.MagentaProbability;

        _currentWaveNumber = 0;

        InitializeWave();
    }

    // Limite de noche, lo marca el numero de noches añadidas en la lista
    public void EndNight()
    {
        if(_saveData.Night >= _nightList.Length)
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Day);
            _saveData.AdvanceNight();
        }
    }

    void InitializeWave()
    {
        foreach(var data in _nightList[_saveData.Night].waves[_currentWaveNumber].SpawnerData)
        {
            if (_spawnerList.TryGetValue(data.Key, out EnemySpawner spawner))
            {
                (int points, Enemy[] pool) = data.Value;
                spawner.SetupSpawner(points, pool);
            }
        }
    }

    void GameStateListener(GameState state)
    {
        if(state == GameState.Night)
        {
            InitializeWave();
        }
        else
        {
            _stopSpawners.Invoke();
        }
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
        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
    }
}
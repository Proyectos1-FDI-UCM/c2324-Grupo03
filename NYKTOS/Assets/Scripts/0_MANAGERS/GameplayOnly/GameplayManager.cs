using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{

    #region references
    [SerializeField]
    private SaveData _saveData;

    [SerializeField]
    private Dictionary<SpawnerRegion, EnemySpawner> _spawnerList = new Dictionary<SpawnerRegion, EnemySpawner>();

    [SerializeField]
    private NightWave[] _nightList;

    #endregion

    #region parameters

    private int _registeredAltars = 0;

    [SerializeField]
    private int _nightLength = 180;

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

    #region events

    private UnityEvent _stopSpawners = new UnityEvent();
    public UnityEvent StopSpawners => _stopSpawners; 

    #endregion

    static private GameplayManager _instance;
    public static GameplayManager Instance
    {
        get { return _instance; }
    }

    public void RegisterSpawner(SpawnerRegion region, EnemySpawner spawner)
    {
        _spawnerList.Add(region, spawner);
    }

    public void RegisterAltar()
    {
        _registeredAltars++;
    }

    public void UnregisterAltar()
    {
        _registeredAltars--;

        if(_registeredAltars <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Win);
        }
    }    

    public void StartNight()
    {
        Invoke(nameof(EndNight), _nightLength);
        
        // Aquí se debería inicializar el reloj

        if(_saveData.Night < _nightList.Length)
        {
            NightWave currentnight = _nightList[_saveData.Night];

            _currentRequiredYellow = currentnight.RequiredYellowCrystals;
            _currentRequiredCyan = currentnight.RequiredCyanCrystals;
            _currentRequiredMagenta = currentnight.RequiredMagentaCrystals;
            _probabilityYellow = currentnight.YellowProbability;
            _probabilityCyan = currentnight.CyanProbability;
            _probabilityMagenta = currentnight.MagentaProbability;

            _currentWaveNumber = 0;

            if(currentnight.waves.Length > 0)
            {
                InitializeWave();
            }
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    public void EndNight()
    {
        _saveData.AdvanceNight();
        GameManager.Instance.UpdateGameState(GameState.Day);
    }

    void InitializeWave()
    {
        StopSpawners.Invoke();

        Wave currentWave = _nightList[_saveData.Night].waves[_currentWaveNumber];

        foreach(SubWave subWave in currentWave.subWaves)
        {
            if (_spawnerList.TryGetValue(subWave.spawnerRegion, out EnemySpawner spawner))
            {
                spawner.SetupSpawner(subWave.pool);
            }
        }

        if(_currentWaveNumber < _nightList[_saveData.Night].waves.Length)     
        {
            Invoke(nameof(AdvanceWave), currentWave.time);
        }
    }

    void AdvanceWave()
    {
        _currentWaveNumber ++;
        InitializeWave();    
    }

    void GameStateListener(GameState state)
    {
        if(state == GameState.Night)
        {
            //_nightLoader.LoadTransition();
            //Invoke(nameof(StartNight), 3f);
            StartNight();
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
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    #region Crystals
    private int _CurrentRequiredYellow;
    public int CurrentRequiredYellow
    {
        get { return _CurrentRequiredYellow; }
        set { _CurrentRequiredYellow = value; }
    }

    private int _CurrentRequiredCyan;
    public int CurrentRequiredCyan
    {
        get { return _CurrentRequiredCyan; }
        set { _CurrentRequiredCyan = value; }
    }

    private int _CurrentRequiredMagenta;
    public int CurrentRequiredMagenta
    {
        get { return _CurrentRequiredMagenta; }
        set { _CurrentRequiredMagenta = value; }
    }

    private int _ProbabilityYellow;
    public int ProbabilityYellow
    {
        get { return _ProbabilityYellow; }
        set { _ProbabilityYellow = value; }
    } 
    private int _ProbabilityCyan;
    public int ProbabilyCyan
    {
        get { return _ProbabilityCyan; }
        set { _ProbabilityCyan = value; }
    }
    private int _ProbabilityMagenta;
    public int ProbabilyMagenta
    {
        get { return _ProbabilityMagenta; }
        set { _ProbabilityMagenta = value; }
    }
    #endregion

    static private SpawnManager _instance;
    public static SpawnManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private int maxEnemies = 5;
    public int MaxEnemies
    {
        get
        {
            return maxEnemies;
        }
    }

    private int concurrentEnemies = 0;
    public int ConcurrentEnemies
    {
        get
        {
            return concurrentEnemies;
        }
        set
        {
            concurrentEnemies = value;
        }
    }

    [SerializeField]
    private NightWave[] _nightList;

    private int _currentNight = 0;

    private int _currentWave;

    [SerializeField]
    private int _currentWavePoints = 1;

    private UnityEvent<SpawnerType, int, Enemy[]> _initializeSpawners = new UnityEvent<SpawnerType, int, Enemy[]>();
    public UnityEvent<SpawnerType, int, Enemy[]> InitializeSpawners => _initializeSpawners;

    private UnityEvent _stopSpawners = new UnityEvent();
    public UnityEvent StopSpawners => _stopSpawners;

    private Wave currentPool;

    void InitializeNight()
    {
        // TODO
        // Aquí va el cálculo de puntos a gastar para esta noche, me da palo 

        _currentWave = 0;
        CurrentRequiredYellow = _nightList[_currentNight].RequiredYellowCrystals;
        CurrentRequiredCyan = _nightList[_currentNight].RequiredCyanCrystals;
        CurrentRequiredMagenta = _nightList[_currentNight].RequiredMagentaCrystals;
        ProbabilityYellow = _nightList[_currentNight].YellowProbability;
        ProbabilyCyan = _nightList[_currentNight].CyanProbability;
        ProbabilyMagenta = _nightList[_currentNight].MagentaProbability;
        InitializeWave();
    }

    [ContextMenu("InitializeWave")]
    void InitializeWave()
    {
        // TODO
        // Aquí va el cálculo de puntos a gastar para esta wave, me da palo 

        currentPool = _nightList[_currentNight].wavePoolList[_currentWave];

        float spawnerPointDistribution = 1 / currentPool.subWaveList.Length * _currentWavePoints;

        int poolNum = (int)Math.Round(spawnerPointDistribution, MidpointRounding.ToEven);

        foreach (SubWave s in currentPool.subWaveList)
        {
            _initializeSpawners.Invoke(s.spawnerType, poolNum, s.enemyPool);
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
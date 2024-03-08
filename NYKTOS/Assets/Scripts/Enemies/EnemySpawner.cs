using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{
    private bool _spawnEnabled = false;

    [SerializeField]
    private double _spawnTimeMin = 0.5;

    [SerializeField]
    private double _spawnTimeMax = 2;

    private double _currentSpawnTime = 0;

    [SerializeField]
    private SpawnerType _spawnerType;

    private int _spawnScore = 0;
    private int _curentSpawnScore = 0;

    [SerializeField]
    private Enemy[] _enemyPool;

    [SerializeField]
    private List<Enemy> _remainingEnemyPool = new List<Enemy>();

    void SetupSpawner(SpawnerType type, int score, Enemy[] enemyPool)
    {
        if(type == _spawnerType){
            _spawnScore = score;
            _curentSpawnScore = score;
            _enemyPool = enemyPool;
            _remainingEnemyPool = enemyPool.ToList<Enemy>();

        _spawnEnabled = true;

        enabled = true;
        }
    }

    void StopSpawner()
    {
        _spawnEnabled = false;

        _spawnScore = 0;
        _curentSpawnScore = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnManager.Instance.InitializeSpawners.AddListener(SetupSpawner);
        SpawnManager.Instance.StopSpawners.AddListener(StopSpawner);
    }

    // Update is called once per frame
    void Update()
    {
        // Este código que he hecho es una puta guarrada, hay que refactorizar un huevo
        if(SpawnManager.Instance.ConcurrentEnemies < SpawnManager.Instance.MaxEnemies)
        {
            if (_spawnEnabled) 
            {
                foreach(Enemy enemy in _remainingEnemyPool)
                {
                    if(enemy.number <= 0)
                    {
                        _remainingEnemyPool.Remove(enemy);
                    }
                }

                if(_curentSpawnScore <= 0 || (_remainingEnemyPool.Count == 0  && _curentSpawnScore == _spawnScore))
                {
                    enabled = false;
                }
                else if (_remainingEnemyPool.Count == 0 && _curentSpawnScore < _spawnScore)
                {
                    // TODO fin de la wave

                    _remainingEnemyPool = _enemyPool.ToList();
                }
                else
                {   
                    System.Random random = new System.Random();
                    
                    int enemySpawnPos = random.Next(0, _remainingEnemyPool.Count);

                    Instantiate
                    (
                        _remainingEnemyPool[enemySpawnPos].enemyPrefab, 
                        transform.GetChild(random.Next(0,  transform.childCount)).position,
                        Quaternion.identity
                    );

                    _curentSpawnScore--;
                    SpawnManager.Instance.ConcurrentEnemies++;

                    _currentSpawnTime = random.NextDouble() * (_spawnTimeMax - _spawnTimeMin + _spawnTimeMin);
                    _spawnEnabled = false;
                }   
            }       
            else if(_currentSpawnTime > 0)
            {
                _currentSpawnTime -= Time.deltaTime;

                if(_currentSpawnTime <= 0) 
                {
                    _spawnEnabled = true;
                }
            }
        }
    }
}
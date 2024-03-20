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

    [SerializeField]
    private Enemy[] _enemyPool;

    [SerializeField]
    private List<Enemy> _remainingEnemyPool = new List<Enemy>();

    public void SetupSpawner(Enemy[] enemyPool)
    {   
        _enemyPool = enemyPool;
        _remainingEnemyPool = enemyPool.ToList();

        _spawnEnabled = true;
    }

    void StopSpawner()
    {
        _spawnEnabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.StopSpawners.AddListener(StopSpawner);
    }

    void Update()
    {
        // Este código que he hecho es una puta guarrada, hay que refactorizar un huevo
        if(GameplayManager.Instance.ConcurrentEnemies < GameplayManager.Instance.MaxEnemies)
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

                if(_remainingEnemyPool.Count == 0)
                {
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

                    GameplayManager.Instance.AddConcurrentEnemy();

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
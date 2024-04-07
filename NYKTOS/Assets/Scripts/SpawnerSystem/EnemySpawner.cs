using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private double _spawnTimeMin = 0.5;

    [SerializeField]
    private double _spawnTimeMax = 2;
    private double _currentSpawnTime = 0;

    [Header("Configuration fields")]
    [SerializeField]
    private SpawnLimit _spawnLimit;

    private List<Enemy> _enemyPool = new List<Enemy>();
    private List<Enemy> _remainingEnemyPool = new List<Enemy>();

    /// <summary>
    /// Comprueba que en la wave haya algo que spawnear para este tipo de spawner
    /// <para>En caso de que no lo haya lo desactiva, en caso de que si recoge los valores utilizables y lo activa</para>
    /// </summary>
    public void SetupSpawner(Enemy[] waveData)
    {
        _enemyPool = new List<Enemy>();
        
        if(waveData.Length > 0)
        {
            foreach(var enemy in waveData)
            {
                if (enemy.number > 0) 
                {
                    _enemyPool.Add(enemy);
                }
            }
        }
        
        EnemyPoolChecker();
    }

    /// <summary>
    /// Resetea los valores variables internos a 0 y desactiva el componente
    /// </summary>
    public void Stop()
    {
        _currentSpawnTime = 0;
        _enemyPool = new List<Enemy>();
        _remainingEnemyPool = new List<Enemy>();

        enabled = false;
    }

    private void EnemyPoolChecker()
    {
        if (_enemyPool.Count > 0) 
        {
            _remainingEnemyPool = _enemyPool;
            enabled = true;
        }
        else
        {
            Stop();
            enabled = false;
        }
    }

    public void Start()
    {
        EnemyPoolChecker();
    }

    void Update()
    {
        if(_currentSpawnTime > 0)
        {
            _currentSpawnTime -= Time.deltaTime; 
        }       
        else
        {   
            if(_remainingEnemyPool.Count == 0)
            {
                _remainingEnemyPool = _enemyPool.ToList();
            }
            
            System.Random random = new System.Random();
            
            int enemySpawnPos = random.Next(0, _remainingEnemyPool.Count);

            if(_remainingEnemyPool[enemySpawnPos].number > 0)
            {
                if(_spawnLimit.AddConcurrentEnemy())
                {
                    GameObject thisEnemy = Instantiate
                    (
                        _remainingEnemyPool[enemySpawnPos].enemyPrefab, 
                        transform.GetChild(random.Next(0,  transform.childCount)).position,
                        Quaternion.identity
                    );

                    thisEnemy.GetComponent<EnemyVariant>().SetVariant(_remainingEnemyPool[enemySpawnPos].variantType);

                    if (_remainingEnemyPool[enemySpawnPos].number <= 0)
                    {
                        _remainingEnemyPool.RemoveAt(enemySpawnPos);
                    }
                }
            }

            _currentSpawnTime = random.NextDouble() * (_spawnTimeMax - _spawnTimeMin + _spawnTimeMin);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

/// <summary>
/// Componente de spawneo de enemigos
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Minimo del intervalo de spawneo 
    /// </summary>
    [SerializeField]
    private double _spawnTimeMin = 0.5;

    /// <summary>
    /// Maximo del intervalo de spawneo 
    /// </summary>
    [SerializeField]
    private double _spawnTimeMax = 2;

    /// <summary>
    /// Tiempo que queda para spawnear el siguiente enemigo
    /// </summary>
    private double _currentSpawnTime = 0;

    [Header("Configuration fields")]

    // Limite de enemigos que pueden existir concurrentemente
    [SerializeField]
    private SpawnLimit _spawnLimit;

    // Evento para avisar del spawneo al indicador de la UI de este spawner
    [SerializeField]
    private BoolEmitter _spawnMarker;

    // Pool base de enemigos a spawnear de la subwave. Regenera _remainingEnemyPool
    private List<Enemy> _enemyPool = new List<Enemy>();

    // Pool de enemigos que quedan por spawnear. Esta lista se regenera cuando se vacía
    private List<Enemy> _remainingEnemyPool = new List<Enemy>();

    /// <summary>
    /// Comprueba que en la wave haya algo que spawnear para este tipo de spawner
    /// <para>En caso de que no lo haya lo desactiva, en caso de que si recoge los valores utilizables y lo activa</para>
    /// </summary>
    /// <param name="waveData"> Listado de enemigos de la wave para este spawner </param>
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
        _spawnMarker?.InvokePerform(false);
    }

    /// <summary>
    /// Comprueba que _enemyPool tenga algo que spawnear, si no desactiva el spawneo
    /// </summary>
    private void EnemyPoolChecker()
    {
        if (_enemyPool.Count > 0) 
        {
            _remainingEnemyPool = _enemyPool;
            enabled = true;

            _spawnMarker?.InvokePerform(true);
        }
        else
        {
            Stop();
        }
    }


    public void Start()
    {
        EnemyPoolChecker();
    }

    /// <summary>
    /// Se van spawneando enemigos en los puntos de spawn (objetos hijo del GameObject) de manera atleatoria.
    /// 
    /// <para> Los enemigos spawneados se extraen de la pool de manera atleatoria, cuando la pool se agota se regenera </para>
    /// 
    /// <para> El tiempo entre cada spawneo está entre el minimo y el maximo del intervalo </para>
    /// </summary>
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

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class SpawnerPerformer : MonoBehaviour
{
    /// <summary>
    /// Instancia de scriptable object que lanza los eventos
    /// </summary>
    [SerializeField] 
    private SpawnerEmmiter _spawnerEmmiter;

    [SerializeField]
    private SpawnerRegion _spawnerRegion;

    private EnemySpawner _enemySpawner;

    private void LoadSpawnerPerformer(Dictionary<SpawnerRegion, Enemy[]> waveData)
    {
        if(waveData.TryGetValue(_spawnerRegion, out Enemy[] enemyPool))
        {
            _enemySpawner.SetupSpawner(enemyPool);
        }
        else
        {
            _enemySpawner.Stop();
        }
    }

    void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();

        _spawnerEmmiter.LoadSpawners.AddListener(LoadSpawnerPerformer);
        _spawnerEmmiter.StopSpawners.AddListener(_enemySpawner.Stop);
    }
}
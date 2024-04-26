using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class SpawnerPerformer : MonoBehaviour
{
    /// <summary>
    /// Instancia de scriptable object que lanza los eventos
    /// </summary>
    [SerializeField] 
    private SpawndataEmitter _spawnerStartEmitter;
    [SerializeField] 
    private VoidEmitter _spawnerStopEmitter;

    [SerializeField]
    private SpawnerRegion _spawnerRegion;

    private EnemySpawner _enemySpawner;

    private void LoadSpawnerPerformer(Dictionary<SpawnerRegion, Enemy[]> waveData)
    {
        if(waveData.TryGetValue(_spawnerRegion, out Enemy[] enemyPool))
        {
            Debug.Log("[SPAWNER PERFORMER] (" + gameObject.name + ") Spawner listado en la wave. Iniciando spawneo...");
            _enemySpawner.SetupSpawner(enemyPool);
        }
        else
        {
            Debug.Log("[SPAWNER PERFORMER] (" + gameObject.name + ") Spawner no presente en la wave. Parando...");
            _enemySpawner.Stop();
        }
    }

    void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();

        _spawnerStartEmitter.Perform.AddListener(LoadSpawnerPerformer);
        _spawnerStopEmitter.Perform.AddListener(_enemySpawner.Stop);
    }

    void OnDestroy()
    {
        _spawnerStartEmitter.Perform.RemoveListener(LoadSpawnerPerformer);
        _spawnerStopEmitter.Perform.RemoveListener(_enemySpawner.Stop);
    }
}
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
        Debug.Log("PERFORMER");
        if(waveData.TryGetValue(_spawnerRegion, out Enemy[] enemyPool))
        {
            _enemySpawner.SetupSpawner(enemyPool);
            Debug.Log("SI");
        }
        else
        {
            Debug.Log("NO");
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
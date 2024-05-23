using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Performer que se encarga de escuchar los eventos de activación de spawneo 
/// para habilitar y deshabilitar el spawn de enemigos
/// </summary>
[RequireComponent(typeof(EnemySpawner))]
public class SpawnerPerformer : MonoBehaviour
{
    /// <summary>
    /// Emitter que lleva los datos de la wave a spawnear. Señal de inicio de spawneo
    /// </summary>
    [SerializeField] 
    private SpawndataEmitter _spawnerStartEmitter;

    /// <summary>
    /// Emmiter vacio que indica el fin de spawneo
    /// </summary>
    [SerializeField] 
    private VoidEmitter _spawnerStopEmitter;

    /// <summary>
    /// Region de spawneo, se utiliza para saber si el spawner tiene que funcionar o no en esta wave
    /// </summary>
    [SerializeField]
    private SpawnerRegion _spawnerRegion;

    /// <summary>
    /// Componente de spawner en el GameObject
    /// </summary>
    private EnemySpawner _enemySpawner;

    /// <summary>
    /// Carga el spawner, 
    /// si en los datos de la wave hay una subwave destinada al tipo del spanwer se habilita. 
    /// Si no, se para.
    /// </summary>
    /// <param name="waveData"> Datos de spawneo de la wave </param>
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
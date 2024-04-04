using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Spawner Emitter", menuName = "Emitter/Spawner")]
public class SpawnerEmitter : ScriptableObject
{   
    private UnityEvent<Dictionary<SpawnerRegion, Enemy[]>> _loadSpawners = new UnityEvent<Dictionary<SpawnerRegion, Enemy[]>>();
    public UnityEvent<Dictionary<SpawnerRegion, Enemy[]>> LoadSpawners => _loadSpawners;
    private UnityEvent _stopSpawners = new UnityEvent();
    public UnityEvent StopSpawners => _stopSpawners;
    
    public void InvokeLoadSpawners(Dictionary<SpawnerRegion, Enemy[]> waveData)
    {
        _loadSpawners.Invoke(waveData);
    }

    public void InvokeStopSpawners()
    {
        _stopSpawners.Invoke();
    }    
}
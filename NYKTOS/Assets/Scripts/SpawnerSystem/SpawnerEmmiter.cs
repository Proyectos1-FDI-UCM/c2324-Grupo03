using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Spawner Emmiter", menuName = "Emmiter/Spawner")]
public class SpawnerEmmiter : ScriptableObject
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
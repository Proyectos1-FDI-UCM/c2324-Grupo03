using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLimit : ScriptableObject
{
    [SerializeField]
    private int _maxEnemies = 10;
    public int MaxEnemies
    {
        get{return _maxEnemies;}
    }
    
    private int _concurrentEnemies = 0;
    public void AddConcurrentEnemy() => _concurrentEnemies++;
    public void RemoveConcurrentEnemy() => _concurrentEnemies--;
}

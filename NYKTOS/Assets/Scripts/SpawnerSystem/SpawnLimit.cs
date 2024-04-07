using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnLimit", menuName = "SpawnLimit")]
public class SpawnLimit : ScriptableObject
{
    [SerializeField]
    private int _maxEnemies = 10;

    private int _concurrentEnemies = 0;
    public int ConcurrentEnemies
    {
        get{return _concurrentEnemies;}
    }

    public void ResetConcurrentEnemies() => _concurrentEnemies = 0;
    public bool AddConcurrentEnemy()
    {
        if(_maxEnemies+1 <= _maxEnemies)
        {
            _concurrentEnemies++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveConcurrentEnemy()
    {
        if(ConcurrentEnemies -1 >= 0)
        {
            _concurrentEnemies--;
        }
    }
}

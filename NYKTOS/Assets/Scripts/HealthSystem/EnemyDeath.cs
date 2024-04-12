using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    private bool _isDead = false;
    public bool isDead { get { return _isDead; } }
    [SerializeField]
    private SpawnLimit _spawnLimit;

    [SerializeField]
    private VoidEmitter _enemyDeathEmitter;

    public void Death()
    {
        _isDead = true;
        if(TryGetComponent<CrystalBag>(out CrystalBag enemyBag))
        {
            enemyBag.InstantiateCrystal(transform.position);
        }

        _spawnLimit?.RemoveConcurrentEnemy();
    }

    void Start(){
        _enemyDeathEmitter.Perform.AddListener(Death);
    }

    void OnDestroy() 
    {
        _enemyDeathEmitter.Perform.RemoveListener(Death);

    }
}

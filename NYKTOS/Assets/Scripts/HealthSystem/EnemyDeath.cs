using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    [SerializeField]
    private SpawnLimit _spawnLimit;

    [SerializeField]
    private VoidEmitter _enemyDeathEmitter;

    public void Death()
    {
        if(TryGetComponent<CrystalBag>(out CrystalBag enemyBag))
        {
            enemyBag.InstantiateCrystal(transform.position);
        }

        _spawnLimit?.RemoveConcurrentEnemy();
        Destroy(gameObject);
    }

    void Start(){
        _enemyDeathEmitter.Perform.AddListener(Death);
    }

    void OnDestroy() 
    {
        _enemyDeathEmitter.Perform.RemoveListener(Death);

    }
}

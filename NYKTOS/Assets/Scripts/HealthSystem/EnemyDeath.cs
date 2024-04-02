using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    // [Marco] Not optimal
    [SerializeField]
    private CustomState _day;

    public void Death()
    {
        if(TryGetComponent<CrystalBag>(out CrystalBag enemyBag))
        {
            enemyBag.InstantiateCrystal(transform.position);
        }

        // enemigos concurrentes
        Destroy(gameObject);
    }

    void Start(){
        _day.OnStateInstantExit.AddListener(Death);
    }
}

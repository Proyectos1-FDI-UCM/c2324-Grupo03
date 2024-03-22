using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    void GameStateListener(GameState state)
    {
        if(!(GameState.Night == state))
        {
            Death();
        }
    }

    public void Death()
    {
        GameplayManager.Instance.RemoveConcurrentEnemy();
        if(TryGetComponent<CrystalBag>(out CrystalBag enemyBag))
        {
            enemyBag.InstantiateCrystal(transform.position);
        }
        Destroy(gameObject);
    }

    void Start(){
        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
    }
}

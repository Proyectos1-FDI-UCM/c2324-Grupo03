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
        SpawnManager.Instance.ConcurrentEnemies--;
        GetComponent<CrystalBag>().InstantiateCrystal(transform.position);
        Destroy(gameObject);
    }

    public void Talk()
    {
        Debug.Log("Soy un enemigo!!");
    }

    void Start(){
        GameManager.Instance.GameStateChanged.AddListener(GameStateListener);
    }
}

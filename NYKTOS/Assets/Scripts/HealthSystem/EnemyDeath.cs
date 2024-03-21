using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    [SerializeField]
    private bool _siNoEsAraneaHija = true;
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
        if(_siNoEsAraneaHija) GetComponent<CrystalBag>().InstantiateCrystal(transform.position);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyDeath : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start() {
        spawnManager = GetComponent<SpawnManager>();
    }
    public void Die()
    {
        Debug.Log("murio");//maria
        //spawnManager.EnemyKilled();
        Destroy(this.gameObject);
        
    }
}

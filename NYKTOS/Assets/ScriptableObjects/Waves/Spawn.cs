using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave System/Wave", order = 1)]
public class WaveManager : ScriptableObject {
    public string waveName;
    public int noOfEnemies;
    public Enemy[] enemies; // Array of enemies for this wave
    public Transform[] spawnPoints; // Array of spawn points for this wave
    public float spawnInterval = 1f; // Interval between spawns
}

[System.Serializable]
public class Enemy {
    public GameObject enemyPrefab; // Prefab of the enemy
    public int enemyCost; // Cost of the enemy
}
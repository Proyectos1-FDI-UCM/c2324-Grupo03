using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MARIA
[CreateAssetMenu(fileName = "New Wave", menuName = "Wave System/Wave", order = 1)]
public class WaveInfo : ScriptableObject {
    public string waveName;
    public Enemy[] enemies; // Array of enemies for this wave
    public SpawnerPriority[] spawnerPriority; //Prioridades del spawner
    public float spawnInterval = 1f; // Interval between spawns
}

[System.Serializable]
public struct Enemy {
    public GameObject enemyPrefab; // Prefab of the enemy
    public int number; // numero de cuantos de ese bicho en esa wave
}
[System.Serializable]
public struct SpawnerPriority {
    public GameObject Spawner;
    public int priority;
} 

//FIN MARIA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MARIA
[CreateAssetMenu(fileName = "New Wave", menuName = "Wave System/Wave", order = 1)]
public class WaveInfo : ScriptableObject {
    public Enemy[] enemies; // Array of enemies for this wave
    public SpawnerPriority[] spawnerPriority; //Prioridades del spawner
}

[System.Serializable]
public struct Enemy {
    public GameObject enemyPrefab; // Prefab of the enemy
    public int number; // numero de cuantos de ese bicho en esa wave
}
[System.Serializable]
public struct SpawnerPriority {
    public GameObject Spawner;//el spawner
    public Spawner[] SpawnPoints;//el spawner
    public int priority;// la prob
    [HideInInspector]
    public int howManyEnemies;
}
public struct Spawner { 
    public Transform spawnerPoints;
}

//FIN MARIA
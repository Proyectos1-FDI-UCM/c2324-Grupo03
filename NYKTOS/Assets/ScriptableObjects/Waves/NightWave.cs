using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MARIA
[CreateAssetMenu(fileName = "New Night", menuName = "Wave System/Night", order = 1)]
public class NightWaves : ScriptableObject {
    public WavePool wavePoolList;
}

[System.Serializable]
public struct WavePool
{
    public SubWave[] subWaveList;
    public int wavePoolDistributionFactor;
}

public struct SubWave
{
    public SpawnerType spawnerType;
    public Enemy[] enemyPool;
    public int spawnerDistributionFactor;
}

[System.Serializable]
public struct Enemy {
    public GameObject enemyPrefab; // Prefab of the enemy
    public int number; // numero de cuantos de ese bicho en la iteración de la wave
}

public enum SpawnerType
{
    Amarillo,
    CianIntermedio,
    CianExtremo,
    MagentaIntermedio,
    MagentaExtremo

}
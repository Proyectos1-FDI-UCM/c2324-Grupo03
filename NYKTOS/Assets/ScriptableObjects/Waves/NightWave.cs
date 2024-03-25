using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MARIA
[CreateAssetMenu(fileName = "New Night", menuName = "Wave System/Night", order = 1)]
public class NightWave : ScriptableObject {
    public Wave[] waves;
    public int RequiredYellowCrystals;
    public int RequiredCyanCrystals;
    public int RequiredMagentaCrystals;
    public int YellowProbability;
    public int CyanProbability;
    public int MagentaProbability;
}

[System.Serializable]
public struct Wave
{
    public int time;
    public SubWave[] subWaves;
}

[System.Serializable]
public struct SubWave {
    public SpawnerRegion spawnerRegion;
    public Enemy[] pool;
}

[System.Serializable]
public struct Enemy {
    public GameObject enemyPrefab; // Prefab of the enemy
    public int number; // numero de cuantos de ese bicho en la iteración de la wave

    public AttackType variantType;
}


[System.Serializable]
public enum SpawnerRegion
{
    Amarillo,
    CianIntermedio,
    CianExtremo,
    MagentaIntermedio,
    MagentaExtremo
}
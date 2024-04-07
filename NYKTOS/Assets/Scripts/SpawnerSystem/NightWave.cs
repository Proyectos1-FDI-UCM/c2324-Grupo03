using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MARIA
[CreateAssetMenu(fileName = "New Night", menuName = "Wave System/Night", order = 1)]
public class NightWave : ScriptableObject {

    [SerializeField]
    private int _nightLength = 180;
    public int NightLength{get{return _nightLength;}}

    [SerializeField]
    private Wave[] _waveList;
    public Wave[] WaveList{get{return _waveList;}}

    [SerializeField]
    private CrystalDrops _nightCrystalDrops;
    public CrystalDrops NightCrystalDrops {get{return _nightCrystalDrops;}}

    void OnValidate()
    {
        foreach(var wave in _waveList)
        {
            wave.WaveValidate();
        }
    }
}

[System.Serializable]
public struct Wave
{
    public int time;
    public SubWave[] subWaveList;
    private Dictionary<SpawnerRegion, Enemy[]> waveData;
    public Dictionary<SpawnerRegion, Enemy[]> WaveData {get{return waveData;}}

    public void WaveValidate()
    {
        if(waveData == null)
        {
            waveData = new Dictionary<SpawnerRegion, Enemy[]>();
        }
        foreach(var subwave in subWaveList)
        {
            if (!waveData.ContainsKey(subwave.spawnerRegion))
            {
                waveData.Add(subwave.spawnerRegion, subwave.enemyPool);
            }
        }
    }
}

[System.Serializable]
public struct SubWave {
    public SpawnerRegion spawnerRegion;
    public Enemy[] enemyPool;
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
    Cian,
    Magenta
}
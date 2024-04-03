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
            foreach(SpawnerRegion region in Enum.GetValues(typeof(SpawnerRegion)))
            {
                bool matchCondition = false; 

                for(int i = 0; i < wave.subWaveList.Length && !matchCondition; i++)
                {
                    if(wave.subWaveList[i].spawnerRegion == region)
                    {
                        wave.WaveData.Add(region, wave.subWaveList[i].enemyPool);
                    }
                }
            }
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
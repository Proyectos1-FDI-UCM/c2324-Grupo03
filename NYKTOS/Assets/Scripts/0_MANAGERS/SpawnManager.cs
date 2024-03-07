using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private NightWaves[] _nightList;

    [SerializeField]
    private int _currentNight;

    [SerializeField]
    private int _currentWave;

    [SerializeField]
    private int _currentWavePoints;

    private UnityEvent initializeSpawners = new UnityEvent();
    public UnityEvent InitializeSpawners;

    private WavePool currentPool;

    void InitializeNight()
    {
        // TODO
        // Aquí va el cálculo de puntos a gastar para esta noche, me da palo 


    } 
}
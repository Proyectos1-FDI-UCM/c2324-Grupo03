using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Maria aaaaaaaaa
    #region references
    [SerializeField] private SpawnEnemy _spawnEnemy;
    #endregion

    #region parameters
    private bool _isNight=true;
    static private int _currentWaveNumber; //numero de la wave
    private bool _isGameActive = true; //para saber si el juego esta activo
    private bool _spawnEnemyWave = true; //para empezar los spawns
    #endregion


    #region methods

    public bool Nigth() {
        return _isNight;
    }

    public void SpawnEnemies() { 
        _spawnEnemyWave = true;
    }
    public int GetWaveNumber(int sum) {  //nos da el numero de la wave si la queremos en la ui
        return _currentWaveNumber += sum;
        Debug.Log(_currentWaveNumber);
    }

   
    public void EnableNextWaveSpawning() { //ui o lo que sea inicia los spawners
        _spawnEnemyWave = true;
    }
    #endregion

    // Start is called before the first frame update
    void Start() {
        _spawnEnemy.WaveSet();
        //_isNight = false;
        _currentWaveNumber =0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnEnemy != null && _isNight && _spawnEnemyWave) {
            _spawnEnemy.StartEnemySpawning();
            _spawnEnemyWave = false;
            //Debug.Log("aa");
        }
        

    }
}
//Fin Maria
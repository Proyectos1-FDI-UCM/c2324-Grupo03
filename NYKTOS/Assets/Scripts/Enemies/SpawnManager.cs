using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
[System.Serializable]
//  Codigo de Maria :p
public class Wave {
    public string waveName;
    public int noOfEnemies;
    public GameObject[] enemyPrefab; //lista de los enemigos
    public float spawnInterval;
}

public class SpawnManager : MonoBehaviour {
   
    #region parameters
    [SerializeField] Wave[] waves;
    [SerializeField] private Transform[] _spawnPoints;
    private Wave _currentWave; //la wave que estamos
    private int _currentWaveNumber; //numero de la wave
    [SerializeField] private float spawnRate = 1f; //tiempo entre los spaw
    private bool _isGameActive = true; //para saber si el juego esta activo
    private bool _spawEnemyWave = true; //para empezar los spawns
    
    
    
    
    // guarrería de Marco para testing
    [SerializeField] private int spawnLimit = 1; //maximo de enemigos en el juego(comentarios de Maria)
    private int currentSpawned = 0; //cuantos enemigos en el momento(comentarios de Maria)
                                    // FIN guarrería de Marco para testing

    //(he comentado esto ~Marco) private bool canSpawn = true;

    #endregion
    #region methods
    private IEnumerator SpawnEnemyRoutine() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); //se espera el tiempo qe pongas en spawRate
        while (_isGameActive && _spawEnemyWave) {
            if (currentSpawned  < spawnLimit) {
                GameObject enemyToSpawn = _currentWave.enemyPrefab[Random.Range(0, _currentWave.enemyPrefab.Length)];//entre los prebs de los enemigos elige uno y no spawnea
                Transform spawnPos = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                Instantiate(enemyToSpawn, spawnPos.position, Quaternion.identity); //cambiar el transform para que no sea un pto especifico.

                currentSpawned++;
                _currentWave.noOfEnemies--;

            }
           
            yield return wait;
            if (_currentWave.noOfEnemies == 0) {
                _spawEnemyWave = false;
            }

           
            if (_isGameActive == false) {
                break;
            }
            
        }
        //spawnLimit += 2; //para que aumente la cantidad de enemigos en la siguiente wave
        _spawEnemyWave = false;//deja de spawnear hasta que digan otra cosa
       
    }

    private void StartGameSpawning() { 
        //_spawnManager.SpawnNextWave();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void StartEnemySpawning() { //metodo para que a cada 2 rondas(o como querais) se incremente la dificultad
        if (_currentWaveNumber % 2 == 0) { 
            spawnRate -= 0.2f;
            if(spawnRate<= 0.4f){ 
                spawnRate = 0.4f;
            }
        }
        StartCoroutine(SpawnEnemyRoutine());//para que se inicie
    
    }
    public int GetWaveNumber() {  //nos da el numero de la wave si la queremos en la ui
        return _currentWaveNumber;
    }

    public void EnableNextWaveSpawning() { //ui o lo que sea inicia los spawners
        _spawEnemyWave = true;
    }

    public void EnemyKilled() {
        currentSpawned--; //cuando muere un enemigo, seria llamado por el script de muerte de enemigos
        Debug.Log("Menos un enemigo");
    }

    #endregion

    void Start() {
        _currentWaveNumber = 0; 
        _currentWave = waves[_currentWaveNumber]; // Inicializa _currentWave
        StartGameSpawning();
    }
    void Update() {
        if (_currentWaveNumber >= 0 && _currentWaveNumber < waves.Length) {
            _currentWave = waves[_currentWaveNumber];
        }

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentSpawned = totalEnemies.Length;
        Debug.Log("Enemigos en pantalla: " + currentSpawned);
        if (totalEnemies.Length <= 0 && !_spawEnemyWave) {
            if (_currentWaveNumber + 1 < waves.Length) {
                _currentWaveNumber++;
                EnableNextWaveSpawning();
                StartEnemySpawning(); //para que empiece
            } else {
                Debug.Log("Acabo el juego");
            }
        }
        if (!_isGameActive  /*|| currentSpawned >= spawnLimit*/) {
            StopCoroutine(SpawnEnemyRoutine());
        }

        //Debug.Log(_currentWaveNumber);
    }
    //  fin codigo de Maria :)
}

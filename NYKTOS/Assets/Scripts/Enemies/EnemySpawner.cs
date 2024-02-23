using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    //  Codigo de Maria :p
    #region references
    private SpawnManager _spawnManager;
    #endregion

    
    #region parameters
    [SerializeField] private float spawnRate = 1f; //tiempo entre los spaw
    [SerializeField] private GameObject[] enemyPrefab; //lista de los enemigos
    [SerializeField] private bool _isGameActive = true; //para saber si el juego esta activo
    [SerializeField] private bool _spawEnemyWave = true; //para empezar los spawns
    [SerializeField] private int _waveNumber = 1; //numero de la wave
    [SerializeField] private int _xPosRangeIz = 1; //area izquierda x
    [SerializeField] private int _XPosRangeDe = 1; //area derecha x
    [SerializeField] private int _yPosRangeIz = 1; //area izquierda y
    [SerializeField] private int _YPosRangeDe = 1; //area derecha y

    // guarrería de Marco para testing
    [SerializeField]
    private int spawnLimit = 1; //maximo de enemigos en el juego(comentarios de Maria)
    private int currentSpawned = 0; //cuantos enemigos en el momento(comentarios de Maria)
                                    // FIN guarrería de Marco para testing

    //(he comentado esto ~Marco) private bool canSpawn = true;

    #endregion
    #region methods
    private IEnumerator SpawnEnemyRoutine() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); //se espera el tiempo qe pongas en spawRate
        while (_isGameActive && _spawEnemyWave && currentSpawned < spawnLimit) {
            int rand = Random.Range(0, enemyPrefab.Length);//entre los prebs de los enemigos elige uno y no spawnea
            GameObject enemyToSpawn = enemyPrefab[rand];
                     Vector3 spawPos = new Vector3(Random.Range(_xPosRangeIz, _XPosRangeDe), Random.Range(_yPosRangeIz, _YPosRangeDe), 0);
            Instantiate(enemyToSpawn, spawPos, Quaternion.identity); //cambiar el transform para que no sea un pto especifico.
            currentSpawned++;
            yield return wait;
            if (_isGameActive == false) {
                break;
            }
            Debug.Log(currentSpawned + " " + spawnLimit );
        }
        _waveNumber++; 
        spawnLimit += 2; //para que aumente la cantidad de enemigos en la siguiente wave
        _spawEnemyWave = false;//deja de spawnear hasta que digan otra cosa
        Debug.Log(_waveNumber);
    }

    public void StartGameSpawning() { 
        //_spawnManager.SpawnNextWave();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void StartEnemySpawning() { //metodo para que a cada 2 rondas(o como querais) se incremente la dificultad
        if (_waveNumber % 2 == 0) { 
            spawnRate -= 0.2f;
            if(spawnRate<= 0.4f){ 
                spawnRate = 0.4f;
            }
        }
        StartCoroutine(SpawnEnemyRoutine());//para que se inicie
    
    }
    public int GetWaveNumber() {  //nos da el numero de la wave si la queremos en la ui
        return _waveNumber;
    }

    public void EnableNextWaveSpawning() { //ui o lo que sea inicia los spawners
        _spawEnemyWave = true;
    }

    public void EnemyKilled() {
        currentSpawned--; //cuando muere un enemigo, seria llamado por el script de muerte de enemigos
    }
    #endregion

    void Start() {
        _spawnManager = GetComponent<SpawnManager>();
    }
    void Update() {
        if (currentSpawned <= 0 && _spawEnemyWave == false) {
            //aqui se puede poner lo del boss cuando lo hagamos
            _spawnManager.SpawnNextWave();
            StartEnemySpawning(); //para que empiece
        }
        if (_isGameActive == false || currentSpawned >= spawnLimit) {
            StopCoroutine(SpawnEnemyRoutine());
        }
    }
    //  fin codigo de Maria :)
}

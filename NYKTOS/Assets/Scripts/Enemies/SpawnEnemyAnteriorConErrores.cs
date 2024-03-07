using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
[System.Serializable]
//  Codigo de Maria :p
public class SpawnEnemy : MonoBehaviour {

    [SerializeField] private SpawnManager _spawnManager;


    #region parameters
    [SerializeField] WaveInfo[] waves; //las waves
    private WaveInfo _currentWave; //la wave que estamos
    private int _currentWaveNumber; //numero de la wave
    [SerializeField] private float spawnRate = 1f; //tiempo entre los spaw
    private bool _spawnEnemyWave = true; //para empezar los spawns
    private int _totalEnemies; //cuenta el total del array
    // guarrería de Marco para testing
    [SerializeField] private int spawnLimit = 1; //maximo de enemigos en el juego(comentarios de Maria)
    private int currentSpawned = 0; //cuantos enemigos en el momento(comentarios de Maria)
                                    // FIN guarrería de Marco para testing

    //(he comentado esto ~Marco) private bool canSpawn = true;

    #endregion
    #region methods
    private IEnumerator SpawnEnemyRoutine() { //corutina de spawneo
        WaitForSeconds wait = new WaitForSeconds(spawnRate); //se espera el tiempo qe pongas en spawRate
        while (_spawnEnemyWave && waves != null) {
            if (currentSpawned  < spawnLimit) { // si hay menos enemigos que el limite, spawnea
                
                        if (_totalEnemies <= 0) { //si no queda enemigos acaba
                            _spawnEnemyWave = false;
                        }

                        GameObject enemyToSpawn = GetRandomEnemy();
                        //Transform spawnPos = _currentWave.spawnerPriority[i].SpawnPoints[Random.Range(0, _currentWave.spawnerPriority[i].SpawnPoints.Length)].spawnerPoints;

                        //Instantiate(enemyToSpawn, spawnPos.position, Quaternion.identity);
                        _totalEnemies--;
                        yield return wait;
                    
                
            }
        }
        spawnLimit += 2; //para que aumente la cantidad de enemigos en la siguiente wave
        _spawnEnemyWave = false;//deja de spawnear hasta que digan otra cosa
        _currentWaveNumber = _spawnManager.GetWaveNumber(1);       
    }

    private GameObject GetRandomEnemy() {
        int totalWeight = 0;
        foreach (var enemy in _currentWave.enemies) {
            totalWeight += enemy.number;
        }

        int randomWeight = Random.Range(0, totalWeight);
        foreach (var enemy in _currentWave.enemies) {
            if (randomWeight < enemy.number)
                return enemy.enemyPrefab;
            randomWeight -= enemy.number;
        }

        return null;
    }
    private int CountTotalEnemies() {
        int totalEnemies = 0;
        foreach (var enemy in _currentWave.enemies) {
            totalEnemies += enemy.number;
        }
        return totalEnemies;
    }
    public void StartEnemySpawning() { //metodo para que a cada 2 rondas(o como querais) se incremente la dificultad
        if (_spawnManager.GetWaveNumber(0) % 2 == 0) { 
            spawnRate -= 0.2f; //mas rapido
            if(spawnRate<= 0.4f){ //para que no se haga 0
                spawnRate = 0.4f;
            }
        }
        //Debug.Log("bb");
        StartCoroutine(SpawnEnemyRoutine());//para que se inicie
    }

    public void WaveSet() {
        if (_spawnManager.GetWaveNumber(0) >= 0 && _spawnManager.GetWaveNumber(0) < waves.Length) {
            _currentWave = waves[_spawnManager.GetWaveNumber(0)]; //avanzar prox wave
        }
        _totalEnemies = CountTotalEnemies();
        Debug.Log("Enemigos en pantalla: " + currentSpawned);
        Debug.Log("Enemigos en la wave " + _currentWaveNumber + ": " + _totalEnemies);
        
    }

    private int CountEnemies() {
        for (int i = 0; i < _currentWave.enemies.Length; i++)
        {
            _totalEnemies += _currentWave.enemies[i].number;
        }
        return _totalEnemies;
    }
    #endregion

    void Update() {
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentSpawned = totalEnemies.Length; //para saber cuantos enemigos hay en pantalla
        //Debug.Log("Enemigos en pantalla: " + currentSpawned);
        //Debug.Log("Wave: " + _currentWaveNumber);
        if (totalEnemies.Length <= 0 && !_spawnEnemyWave) { //si no es la primera wave y quedan enemigos por seren spawneadoss
            if (_currentWaveNumber + 1 <= waves.Length) { // si aun no se acabo las waves
                _currentWave = waves[_currentWaveNumber];
                _totalEnemies = CountTotalEnemies();
                _spawnManager.EnableNextWaveSpawning();
                _spawnEnemyWave = true;
                Debug.Log("ProxWave");
            } else {
                Debug.Log("Acabo el juego");
            }
        }
    }
    //  fin codigo de Maria :)
}

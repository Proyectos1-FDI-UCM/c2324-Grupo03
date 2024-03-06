using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Maria aaaaaaaaa
    
    #region references
    private SpawnEnemy _spawnEnemy;
    #endregion

    #region parameters
    static private int _currentWaveNumber; //numero de la wave
    #endregion


    #region methods

    public int GetWaveNumber(int sum) {  //nos da el numero de la wave si la queremos en la ui
        return _currentWaveNumber += sum;
        Debug.Log(_currentWaveNumber);
    }

   
    public void EnableNextWaveSpawning() { //ui o lo que sea inicia los spawners
        _spawnEnemy.StartEnemySpawning();
    }
    #endregion

    private void SpawnerEnabler(GameState state)
    {
        if (state == GameState.Night)
        {
            EnableNextWaveSpawning();
            Debug.Log("Empieza Wave");
        }
        else
        { 
            
        }

    }

    // Start is called before the first frame update
    void Start() {
        _spawnEnemy.WaveSet();
        _currentWaveNumber =0;
        GameManager.Instance.GameStateChanged.AddListener(SpawnerEnabler);
    }
}
//Fin Maria
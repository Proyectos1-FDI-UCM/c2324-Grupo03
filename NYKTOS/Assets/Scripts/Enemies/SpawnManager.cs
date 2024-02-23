using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    //  Codigo de Maria :p
    #region references
    private EnemySpawner _enemySpawner;
    //[SerializeField] private Text _waveText;
    #endregion

    #region methods
    public void SpawnNextWave() {
        StartCoroutine(WaveTextEnableRoutine()); //lo llame asi pq estaba probando con texto
    }

    private IEnumerator WaveTextEnableRoutine() {

        //_waveText.text = "Wave " + _enemySpawner.GetWaveNumber();
        //_waveText.gameObject.SetActive(true);
        _enemySpawner.EnableNextWaveSpawning();

        yield return new WaitForSeconds(3f);
        //_waveText.gameObject.SetActive(false);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.StartGameSpawning();
        //_waveText.text = "Wave " + 00;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  fin codigo de Maria :)

}

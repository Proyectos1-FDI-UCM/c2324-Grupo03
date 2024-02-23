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
    
    #endregion

    #region methods
    public void SpawnNextWave() {
        StartCoroutine(EnableRoutine()); 
    }

    private IEnumerator EnableRoutine() {

        
        _enemySpawner.EnableNextWaveSpawning();

        yield return new WaitForSeconds(3f);
      
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.StartGameSpawning();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  fin codigo de Maria :)

}

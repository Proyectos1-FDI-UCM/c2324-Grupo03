using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {


    //  Codigo de Maria :p
    [SerializeField] 
    private float spawnRate = 1f; //tiempo entre los spaw

    [SerializeField] 
    private GameObject[] enemyPrefab; //lista de los enemigos

    // guarrería de Marco para testing
    [SerializeField] 
    private int spawnLimit = 1;
    private int currentSpawned = 0;
    // FIN guarrería de Marco para testing

    //(he comentado esto ~Marco) private bool canSpawn = true;

    void Start() {
        StartCoroutine(Spawner()); //fijar un tiempo para que se spawnen
    }

    private IEnumerator Spawner() {

        WaitForSeconds wait = new WaitForSeconds(spawnRate); //se espera el tiempo qe pongas en spawRate
        while (currentSpawned < spawnLimit) {

            currentSpawned++;

            yield return wait;
            int rand = Random.Range(0, enemyPrefab.Length);//entre los prebs de los enemigos elige uno y no spawnea
            GameObject enemyToSpawn = enemyPrefab[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        }
    }

    //  fin codigo de Maria :)
}

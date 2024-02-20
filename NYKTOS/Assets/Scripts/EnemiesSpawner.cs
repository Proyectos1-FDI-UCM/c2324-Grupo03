using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {


    //  Codigo de Maria :p
    [SerializeField] 
    private float spawnRate = 1f; //tiempo entre los spaw
    [SerializeField] 
    private GameObject[] enemyPrefab; //lista de los enemigos
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Spawner()); //fijar un tiempo para que se spawnen
    }

    private IEnumerator Spawner() {

        WaitForSeconds wait = new WaitForSeconds(spawnRate); //se espera el tiempo qe pongas en spawRate
        while (canSpawn) {
            yield return wait;
            int rand = Random.Range(0, enemyPrefab.Length);//entre los prebs de los enemigos elige uno y no spawnea
            GameObject enemyToSpawn = enemyPrefab[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        }
    }

    //  fin codigo de Maria :)
}

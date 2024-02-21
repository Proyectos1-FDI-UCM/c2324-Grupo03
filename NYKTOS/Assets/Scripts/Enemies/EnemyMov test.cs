using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovtest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform _playertransfom;
    private float speed = 2f;

   
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playertransfom.transform.position, speed * Time.deltaTime);

    }

}

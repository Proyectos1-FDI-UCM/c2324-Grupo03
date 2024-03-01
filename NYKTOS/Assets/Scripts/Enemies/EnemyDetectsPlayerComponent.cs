using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Codigo de Iker :D
public class EnemyDetectsPlayerComponent : MonoBehaviour
{
    private EnemyController enemyController;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            enemyController.PlayerDetected(playerController.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            enemyController.PlayerNotDetected(playerController.transform);
            
        }
    }
}

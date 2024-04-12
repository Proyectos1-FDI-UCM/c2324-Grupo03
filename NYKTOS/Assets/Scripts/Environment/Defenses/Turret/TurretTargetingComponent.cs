using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingComponent : MonoBehaviour
{
    private Transform _myTransform;
    private Transform _enemyTransform;
    private bool _enemyDetected = false;
    [SerializeField]
    private float RotationVelocity = 5f;
    private Vector3 directionToEnemy;
    private List<Transform> _detectedEnemies = new List<Transform>();

    void Start()
    {
        _myTransform = transform;
    }

    public Vector3 DirectionToEnemy()
    {
        return directionToEnemy;
    }

    public Transform EnemyTransform()
    {
        return _enemyTransform;
    }

    void Update()
    {
        if (_enemyTransform != null)
        {
            directionToEnemy = (_enemyTransform.position - _myTransform.position).normalized;

            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

            _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, targetRotation, RotationVelocity);
        }
        /*else
        {
            directionToEnemy = Vector3.zero;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            _detectedEnemies.Add(enemy.transform);
            if (_enemyTransform == null)
            {
                _enemyTransform = enemy.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            _detectedEnemies.Remove(enemy.transform);

            if (_enemyTransform == enemy.transform)
            {
                if (_detectedEnemies.Count > 0)
                {
                    _enemyTransform = _detectedEnemies[0];
                }
                else
                {
                    _enemyTransform = null;
                }
            }
        }
    }
}

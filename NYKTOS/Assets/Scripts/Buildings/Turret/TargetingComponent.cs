using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingComponent : MonoBehaviour
{
    private Transform _myTransform;
    [SerializeField]
    private Transform _targetEnemy;
    private bool _enemyDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyDetected && _targetEnemy != null)
        {
            Vector3 lookDirection = _targetEnemy.position - _myTransform.position;
            lookDirection.y = 0;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            _myTransform.rotation = rotation;
        }
        else
        {
            Debug.Log("No detecto enemigos");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetEnemy.gameObject)
        {
            _enemyDetected = true;
            _targetEnemy = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _targetEnemy.gameObject)
        {
            _enemyDetected = false;
            _targetEnemy = null;
        }
    }
}

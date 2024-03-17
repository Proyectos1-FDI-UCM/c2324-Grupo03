using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletComponent : MonoBehaviour
{
    private TurretTargetingComponent targetingComponent;
    private TurretShootingComponent shootingComponent;
    private Vector3 _directionToEnemy;
    private Transform _myTransform;
    [SerializeField]
    private float BulletVelocity = 10f;
    [SerializeField]
    private int BulletDamage = 2;

    void Start()
    {
        targetingComponent = GetComponentInParent<TurretTargetingComponent>();
        shootingComponent = GetComponentInParent<TurretShootingComponent>();
        _myTransform = transform;
    }

    void Update()
    {
        if (targetingComponent != null && shootingComponent != null)
        {
            _directionToEnemy = targetingComponent.DirectionToEnemy();
            if (_directionToEnemy != Vector3.zero)
            {
                _myTransform.position += _directionToEnemy * BulletVelocity * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            Destroy(this.gameObject);
            enemy.GetComponent<HealthComponent>().Damage(BulletDamage);
        }
    }
}

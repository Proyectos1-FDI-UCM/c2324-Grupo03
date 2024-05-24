using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo de Iker
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

    //Obtenemos los componentes de apuntado y de disparo de la torreta
    void Start()
    {
        targetingComponent = GetComponentInParent<TurretTargetingComponent>();
        shootingComponent = GetComponentInParent<TurretShootingComponent>();
        _myTransform = transform;
    }

    //En caso de que los componentes de apuntado y disparo de la torreta existan se disparará una bala en función de la dirección del enemigo.
    //Se toma la dirección del enemigo y mientras el enemigo este en rango, la bala se moverá en dirección del enemigo.
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

    //En caso de que la bala alcance a un enemigo (se detecta que es un enemigo por el componente que lleva):
    //Se destruirá la bala, y se hará el daño correspondiente al enemigo que dependerá de la cantidad de daño que le hemos puesto a la bala
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            Destroy(this.gameObject);
            enemy.GetComponent<HealthComponent>().Damage(BulletDamage);
        }
    }
}

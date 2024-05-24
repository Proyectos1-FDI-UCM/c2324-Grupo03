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

    //En caso de que los componentes de apuntado y disparo de la torreta existan se disparar� una bala en funci�n de la direcci�n del enemigo.
    //Se toma la direcci�n del enemigo y mientras el enemigo este en rango, la bala se mover� en direcci�n del enemigo.
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
    //Se destruir� la bala, y se har� el da�o correspondiente al enemigo que depender� de la cantidad de da�o que le hemos puesto a la bala
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            Destroy(this.gameObject);
            enemy.GetComponent<HealthComponent>().Damage(BulletDamage);
        }
    }
}

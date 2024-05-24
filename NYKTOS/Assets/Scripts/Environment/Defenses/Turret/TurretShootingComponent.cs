using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo de Iker
public class TurretShootingComponent : MonoBehaviour
{
    private TurretTargetingComponent _turretTargetingComponent;
    private Vector3 _DirectionToEnemy;
    private Transform _enemyTransform;
    [SerializeField]
    private GameObject _proyectil;
    private Transform _myTransform;
    [SerializeField]
    private float DurationOfBullet = 4f;
    [SerializeField]
    private float RechargeTime = 1f;
    private float RechargeTimePrincipal;

    /// <summary>
    /// Se toma el componente de apuntado de la torreta, para obtener la posición de los enemigos
    /// Se toma el componente de apuntado de la torreta, para obtener la posición de los enemigos
    /// </summary>e tenga que volver a generar la bala.
    void Start()
    {
        _turretTargetingComponent = GetComponent<TurretTargetingComponent>();
        RechargeTimePrincipal = RechargeTime;
        _myTransform = transform;
    }

    ///<summary>
    /// Se toma la dirección al enemigo
    /// Se toma el transform del enemigo
    /// En caso de que ninguno de estos dos sea null, es decir, la torreta tiene un objetivo por el turretTargetingComponent:
    /// Se procede a recargar la torreta, cuando el tiempo de recarga sea menor que 0, se instanciara una bala, que se destruira en función de un tiempo dado.
    /// </summary>
    void Update()
    {
        
        _DirectionToEnemy = _turretTargetingComponent.DirectionToEnemy();
        _enemyTransform = _turretTargetingComponent.EnemyTransform();
        
        if (_enemyTransform != null && _DirectionToEnemy != null)
        {
            RechargeTime -= Time.deltaTime;

            if (RechargeTime < 0)
            {
                GameObject bullet = Instantiate(_proyectil, _myTransform.position, Quaternion.identity, _myTransform);
                Destroy(bullet,DurationOfBullet);
                RechargeTime = RechargeTimePrincipal;
            }

        }


    }


}

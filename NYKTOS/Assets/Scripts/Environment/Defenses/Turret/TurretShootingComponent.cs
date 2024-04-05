using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootingComponent : MonoBehaviour
{
    private TurretTargetingComponent _turretTargetingComponent;
    private Vector3 _DirectionToEnemy;
    [SerializeField]
    private GameObject _proyectil;
    private Transform _myTransform;
    [SerializeField]
    private float DurationOfBullet = 4f;
    [SerializeField]
    private float RechargeTime = 1f;
    private float RechargeTimePrincipal;

    // Start is called before the first frame update
    void Start()
    {
        _turretTargetingComponent = GetComponent<TurretTargetingComponent>();
        RechargeTimePrincipal = RechargeTime;
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _DirectionToEnemy = _turretTargetingComponent.DirectionToEnemy();

        if (_DirectionToEnemy != Vector3.zero && _DirectionToEnemy != null)
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

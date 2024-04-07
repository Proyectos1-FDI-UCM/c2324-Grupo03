using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : WeaponBehaviour
{
    #region references
    Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    float _speed = 1.0f;
    #endregion

    #region
    Vector3 _direction = Vector3.zero;
    #endregion
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        _myTransform.position = _myTransform.position + _direction.normalized * _speed *Time.deltaTime ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 7)
        {
            Damage(collision);
            Knockback(collision, _myTransform);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _myTransform = transform;

        Destroy(gameObject, 5);
    }
}

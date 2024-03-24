using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VespertilioAttackHitbox : WeaponBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
        Knockback(collision, _myTransform);
        print(_myTransform);
    }

    private void Awake()
    {
        _myTransform = transform;
    }
}

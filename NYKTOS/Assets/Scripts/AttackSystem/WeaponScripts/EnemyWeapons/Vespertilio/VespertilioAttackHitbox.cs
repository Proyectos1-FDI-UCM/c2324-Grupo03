using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VespertilioAttackHitbox : WeaponBehaviour
{
    #region references
    [NonSerialized]
    private Transform parentTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
        Knockback(collision, parentTransform);
    }

    public void SetTransform(Transform t)
    {
        parentTransform = t;
    }
}

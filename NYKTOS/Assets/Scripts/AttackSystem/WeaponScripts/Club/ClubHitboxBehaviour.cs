using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHitboxBehaviour : WeaponBehaviour
{
    #region references
    private Transform _myTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
        Knockback(collision, _myTransform);
    }

    public IEnumerator DestroyMe(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void Awake()
    {
        _myTransform = transform;
        
    }
}

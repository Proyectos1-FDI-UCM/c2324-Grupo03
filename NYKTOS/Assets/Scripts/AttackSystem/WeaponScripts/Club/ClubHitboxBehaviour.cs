using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHitboxBehaviour : WeaponBehaviour
{
    #region references
    private Transform parentTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
        Knockback(collision, parentTransform);
    }

    public IEnumerator DestroyMe(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    public void SetTransform(Transform t)
    {
        parentTransform = t;
    }
}

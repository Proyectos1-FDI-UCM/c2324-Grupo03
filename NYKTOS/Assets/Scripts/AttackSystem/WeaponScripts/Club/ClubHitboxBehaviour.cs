using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que controla la hitbox del garrote
/// </summary>
public class ClubHitboxBehaviour : WeaponBehaviour
{
    #region references
    private Transform parentTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision) //Detecta la colisión de la hitbox con otra entidad
    {
        if(collision.gameObject.layer != 7)
        {
            Damage(collision);
            Knockback(collision, parentTransform);
        }
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

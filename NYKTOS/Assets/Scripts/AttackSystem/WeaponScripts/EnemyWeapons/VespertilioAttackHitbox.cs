using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VespertilioAttackHitbox : MonoBehaviour
{
    #region properties
    public int attackDamage = 0;
    public AttackType attackType;
    #endregion

    #region references
    private Transform _myTransform;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.TryGetComponent(out HealthComponent health)) //QUITAR VIDA
        {
            health.Damage(attackDamage);
            //Javi ha hecho una corrección a este código para que sea más limpio. Dejo este de ejemplo :)
        }

        if (collision.gameObject.GetComponentInChildren<IKnockback>() != null)
        {

            IKnockback[] iknockbackChildren = collision.gameObject.GetComponentsInChildren<IKnockback>();

            for (int i = 0; i < iknockbackChildren.Length; i++)
            {
                iknockbackChildren[i].CallKnockback(_myTransform.position);
            }

        }

        if (attackType == AttackType.Fire && collision.gameObject.TryGetComponent(out SetOnFireDebuff setOnFire)) //DAÑO DE FUEGO
        {
            setOnFire.enabled = true;
        }

        else if (attackType == AttackType.Slow && collision.gameObject.TryGetComponent(out SlowDebuff slow)) //RALENTIZAR
        {
            slow.enabled = true;
        }
    }

    private void Awake()
    {
        _myTransform = transform;
    }
}

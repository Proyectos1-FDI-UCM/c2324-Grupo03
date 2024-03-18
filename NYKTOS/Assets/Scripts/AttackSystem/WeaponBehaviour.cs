using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    private int weaponDamage = 0;
    private AttackType attackType = 0;

    public void SetStats(int wD, AttackType aT)
    {
        weaponDamage = wD;
        attackType = aT;
    }

    /// <summary>
    /// Produce da�o si el collider cuenta con componente de vida.
    /// Tambi�n producir� el efecto que tenga el arma si el collider cuenta con los scripts de dicho efecto.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="attackDamage"></param>
    /// <param name="attackType"></param>
    public void Damage(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthComponent health)) //QUITAR VIDA
        {
            health.Damage(weaponDamage);
            //Javi ha hecho una correcci�n a este c�digo para que sea m�s limpio. Dejo este de ejemplo :)
        }

        if (attackType == AttackType.Fire && collision.gameObject.TryGetComponent(out SetOnFireDebuff setOnFire)) //DA�O DE FUEGO
        {
            setOnFire.enabled = true;
        }

        else if (attackType == AttackType.Slow && collision.gameObject.TryGetComponent(out SlowDebuff slow)) //RALENTIZAR
        {
            slow.enabled = true;
        }
    }

    /// <summary>
    /// Produce Knockback si el collider cuenta con un componente de knockback
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="_myTransform">
    /// Transform desde donde se aplica el knockback. Normalmente _myTransform
    /// </param>
    public void Knockback(Collider2D collision, Transform _myTransform)
    {
        if (collision.gameObject.GetComponentInChildren<IKnockback>() != null)
        {

            IKnockback[] iknockbackChildren = collision.gameObject.GetComponentsInChildren<IKnockback>();

            for (int i = 0; i < iknockbackChildren.Length; i++)
            {
                iknockbackChildren[i].CallKnockback(_myTransform.parent.position);
            }

        }
    }
}

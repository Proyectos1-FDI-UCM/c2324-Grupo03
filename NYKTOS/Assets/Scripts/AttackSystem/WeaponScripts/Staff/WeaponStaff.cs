using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStaff : Weapon
{
    #region parameters
    [SerializeField]
    private float _knockbackRadius = 2f;

    [SerializeField]
    private GameObject _knockbackArea;

    [SerializeField]
    private GameObject _bullet;
    #endregion

    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType, WeaponHandler weaponHandler)
    {
        GameObject current =
        Instantiate(_bullet, weaponHandler.transform.position + (Vector3)direction.normalized * 0.5f, Quaternion.identity);

        current.TryGetComponent<BulletComponent>(out BulletComponent bullet);

        bullet.SetStats(damage, attackType);
        bullet.SetDirection(direction);
    }

    #region secondaryuse
    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType, WeaponHandler weaponHandler)
    {
        weaponHandler.StartCoroutine(KnockbackArea(weaponHandler.transform));
    }

    IEnumerator KnockbackArea(Transform _myTransform)
    {
        GameObject current =
            Instantiate(_knockbackArea, _myTransform);

        if (current.TryGetComponent<KnockbackAreaStaff>(out KnockbackAreaStaff a))
        {
            a.SetKnockbackArea(_knockbackRadius);
        }

        yield return new WaitForSeconds(0.1f);

        Destroy(current);
    }
    #endregion
}

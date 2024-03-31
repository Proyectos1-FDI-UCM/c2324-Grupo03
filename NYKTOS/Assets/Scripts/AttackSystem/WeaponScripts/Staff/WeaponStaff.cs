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

    [SerializeField]
    private GameObject _staffPrefab;
    #endregion

    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        GameObject current =
        Instantiate(_bullet, transform.position + (Vector3)direction.normalized * 0.5f, Quaternion.identity);

        current.TryGetComponent<BulletComponent>(out BulletComponent bullet);

        bullet.SetStats(damage, attackType);
        bullet.SetDirection(direction);

        if (_staffPrefab != null)
        {
            StartCoroutine(InstantiateSprite(direction));
        }

    }

    #region secondaryuse
    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        StartCoroutine(KnockbackArea(transform));
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

    #region primaryuse
    IEnumerator InstantiateSprite(Vector2 direction)
    {
            GameObject sprite = Instantiate(_staffPrefab, transform.position + (Vector3)direction.normalized * 0.75f, Quaternion.Euler(0, 0, DirectionAngle(direction) - 90));
            sprite.transform.parent = transform;
            yield return new WaitForSeconds(0.3f);
            Destroy(sprite);
    }

    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    #endregion
}

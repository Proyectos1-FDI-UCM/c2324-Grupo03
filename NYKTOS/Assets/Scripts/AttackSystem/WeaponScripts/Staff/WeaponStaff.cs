using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStaff : Weapon
{
    #region references
    private Transform _myTransform;
    #endregion
    #region parameters
    [SerializeField]
    private float _knockbackRadius = 2f;

    [SerializeField]
    private GameObject _knockbackArea;
    #endregion

    public override void PrimaryUse(Vector2 direction)
    {
        
    }

    public override void SecondaryUse(Vector2 direction)
    {
        StartCoroutine(KnockbackArea());
    }

    IEnumerator KnockbackArea()
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

    private void Awake()
    {
        _myTransform = transform;
    }
}

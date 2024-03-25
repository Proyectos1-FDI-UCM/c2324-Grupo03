using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVespertilioAttack : Weapon
{
    #region parameters
    [SerializeField] private float _waitUntilAttacking = 0.2f;
    [SerializeField] private float _hitboxAppearingTime = 0.5f;
    #endregion

    [NonSerialized]
    bool startedCoroutine = false;


    [SerializeField]
    private GameObject _vespertilioAttackHitbox;
    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType, WeaponHandler weaponHandler)
    {
        if (!startedCoroutine)
        {
            weaponHandler.StartCoroutine(VespertilioAttack(direction, damage, attackType, weaponHandler.transform));
        }
        
    }
    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType, WeaponHandler weaponHandler)
    {

    }

    private IEnumerator VespertilioAttack(Vector2 direction, int damage, AttackType attackType, Transform _myTransform)
    {
        startedCoroutine = true;
        yield return new WaitForSeconds(_waitUntilAttacking);

        GameObject currentHitbox =
            Instantiate(_vespertilioAttackHitbox, 
            _myTransform.position + new Vector3(direction.normalized.x, direction.normalized.y, 0), 
            Quaternion.Euler(0, 0, DirectionAngle(direction)));

        currentHitbox.transform.parent = _myTransform;
        currentHitbox.GetComponent<VespertilioAttackHitbox>().SetStats(damage, attackType);

        yield return new WaitForSeconds(_hitboxAppearingTime);

        Destroy(currentHitbox);

        yield return new WaitForSeconds(1- (_waitUntilAttacking + _hitboxAppearingTime));
        startedCoroutine = false;
    }
    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void Awake()
    {
        startedCoroutine = false;
    }
}

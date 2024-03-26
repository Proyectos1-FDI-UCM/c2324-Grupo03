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
    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        if (!startedCoroutine)
        {
            StartCoroutine(VespertilioAttack(direction, damage, attackType));
        }
        
    }
    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType)
    {

    }

    private IEnumerator VespertilioAttack(Vector2 direction, int damage, AttackType attackType)
    {
        startedCoroutine = true;
        yield return new WaitForSeconds(_waitUntilAttacking);

        GameObject currentHitbox =
            Instantiate(_vespertilioAttackHitbox, 
            transform.position + new Vector3(direction.normalized.x, direction.normalized.y, 0), 
            Quaternion.Euler(0, 0, DirectionAngle(direction)));

        currentHitbox.transform.parent = transform;
        currentHitbox.GetComponent<VespertilioAttackHitbox>().SetStats(damage, attackType);
        currentHitbox.GetComponent<VespertilioAttackHitbox>().SetTransform(transform);

        yield return new WaitForSeconds(_hitboxAppearingTime);

        Destroy(currentHitbox);

        yield return new WaitForSeconds(1- (_waitUntilAttacking + _hitboxAppearingTime));
        startedCoroutine = false;
    }
    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}

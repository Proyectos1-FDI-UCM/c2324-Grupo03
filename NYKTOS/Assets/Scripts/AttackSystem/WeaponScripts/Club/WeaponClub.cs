using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponClub : Weapon
{
    #region references
    [SerializeField]
    private GameObject attackHitbox;
    #endregion

    #region parameters
    [SerializeField]
    float _hitboxDistanceFromPlayer = 1;
    [SerializeField]
    private float _timeBeforeDestroy = 0.2f;
    #endregion

    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        GameObject currentHitbox = 
            Instantiate(attackHitbox, transform.position + (Vector3) direction.normalized* _hitboxDistanceFromPlayer, Quaternion.Euler(0, 0, DirectionAngle(direction)));
        currentHitbox.transform.parent = transform;

        ClubHitboxBehaviour behaviour = currentHitbox.GetComponent<ClubHitboxBehaviour>();
        behaviour.SetStats(damage, attackType);
        behaviour.SetTransform(transform);
        StartCoroutine(behaviour.DestroyMe(_timeBeforeDestroy));
    }

    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType)
    {

    }
    
    
    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}

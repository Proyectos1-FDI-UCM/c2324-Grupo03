using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponClub : Weapon
{
    #region references
    [SerializeField]
    private GameObject attackHitbox;
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    float _hitboxDistanceFromPlayer = 1;
    [SerializeField]
    private float _timeBeforeDestroy = 0.2f;
    #endregion

    public override void PrimaryUse(Vector2 direction)
    {
        GameObject currentHitbox = 
            Instantiate(attackHitbox, _myTransform.position + (Vector3) direction.normalized* _hitboxDistanceFromPlayer, Quaternion.Euler(0, 0, DirectionAngle(direction)));
        currentHitbox.transform.parent = _myTransform;

        ClubHitboxBehaviour behaviour = currentHitbox.GetComponent<ClubHitboxBehaviour>();
        behaviour.SetStats(damage, attackType);
        StartCoroutine(behaviour.DestroyMe(_timeBeforeDestroy));
    }

    public override void SecondaryUse(Vector2 direction)
    {

    }
    
    
    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    private void Awake()
    {
        _myTransform = transform;
    }
}

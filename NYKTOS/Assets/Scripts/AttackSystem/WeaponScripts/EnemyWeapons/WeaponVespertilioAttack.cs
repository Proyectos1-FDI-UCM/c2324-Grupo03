using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVespertilioAttack : MonoBehaviour, IWeapon
{
    #region references
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField] private float _waitUntilAttacking = 0.2f;
    [SerializeField] private float _hitboxAppearingTime = 0.5f;
    
    #endregion

    #region weaponProperties
    [SerializeField] private AttackType attackType;
    [SerializeField] private int weaponDamage = 1;
    #endregion

    [SerializeField]
    private GameObject _vespertilioAttackHitbox;
    public void PrimaryUse(Vector2 direction)
    {
        StartCoroutine(VespertilioAttack(direction));
    }
    public void SecondaryUse(Vector2 direction)
    {

    }

    public void SetDamageType(AttackType damageType)
    {
        attackType = damageType;
    }



    private IEnumerator VespertilioAttack(Vector2 direction)
    {
        yield return new WaitForSeconds(_waitUntilAttacking);

        GameObject currentHitbox =
            Instantiate(_vespertilioAttackHitbox, 
            _myTransform.position + new Vector3(direction.normalized.x, direction.normalized.y, 0), 
            Quaternion.Euler(0, 0, DirectionAngle(direction)));
        currentHitbox.transform.parent = _myTransform;

        yield return new WaitForSeconds(_hitboxAppearingTime);

        Destroy(currentHitbox);

        yield return new WaitForSeconds(1- (_waitUntilAttacking + _hitboxAppearingTime));
    }
    private float DirectionAngle(Vector2 direction) //saca el angulo de la direccion dando por sentado que el modulo de la direccion es 1
    {
        float rad;


        //print(Mathf.Atan2(direction.y,direction.x)* Mathf.Rad2Deg);
        return (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void Awake()
    {
        _myTransform = transform;
    }
}

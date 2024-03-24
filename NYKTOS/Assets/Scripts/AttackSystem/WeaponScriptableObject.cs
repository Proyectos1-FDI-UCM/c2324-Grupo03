using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName { get { return _weaponName; } }
    [SerializeField]
    private string _weaponName;

    public int damage { get { return _damage; } }
    [SerializeField]
    private int _damage = 0;

    [SerializeField]
    private Weapon _weapon;

    public void PrimaryUse(Vector2 direction, AttackType _attackType, WeaponHandler weaponHandler)
    {
        if (_weapon != null)
        {
            _weapon.PrimaryUse(direction, _damage, _attackType, weaponHandler);
        }
    }
    public void SecondaryUse(Vector2 direction, AttackType _attackType, WeaponHandler weaponHandler)
    {
        if (_weapon != null)
        {
            _weapon.SecondaryUse(direction, _damage, _attackType, weaponHandler);
        }
    }
}

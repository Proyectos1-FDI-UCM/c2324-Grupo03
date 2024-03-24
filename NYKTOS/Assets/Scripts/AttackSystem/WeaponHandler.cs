using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType
{
    Default, Fire, Slow
}

public class WeaponHandler : MonoBehaviour
{
    [System.Serializable]
    struct weaponStruct
    {
        [SerializeField]
        public WeaponScriptableObject weapon;

        [SerializeField]
        public AttackType attackType;
    }

    [SerializeField] private weaponStruct[] weapons = new weaponStruct[1];
   
    
    public void CallPrimaryUse(int num, Vector2 direction)
    {
        weapons[num].weapon.PrimaryUse(direction, weapons[num].attackType, this);
    }

    public void CallSecondaryUse(int num, Vector2 direction)
    {
        weapons[num].weapon.SecondaryUse(direction, weapons[num].attackType, this);
    }

    public void SetWeapon(int num, WeaponScriptableObject setWeapon)
    {
        weapons[num].weapon = setWeapon;
    }

    public void SetDamageType(int num, AttackType attackType)
    {
        weapons[num].attackType = attackType;
    }
}

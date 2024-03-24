using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType
{
    Default, Fire, Slow
}

public class WeaponHandler : MonoBehaviour
{
   [SerializeField]
    private Weapon[] weapon = new Weapon[1]; //cantidad de armas que puede llevar a la vez
    
    
    public void CallPrimaryUse(int num, Vector2 direction)
    {
        weapon[num].PrimaryUse(direction);
    }

    public void CallSecondaryUse(int num, Vector2 direction)
    {
        weapon[num].SecondaryUse(direction);
    }

    public void SetWeapon(int num, Weapon setWeapon)
    {
        weapon[num] = setWeapon;
    }

    public void SetDamageType(int num, AttackType attackType)
    {
        weapon[num].SetDamageType(attackType);
    }
}

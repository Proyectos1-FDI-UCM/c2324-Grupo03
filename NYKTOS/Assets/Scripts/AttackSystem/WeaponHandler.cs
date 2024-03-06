using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType
{
    Default, Fire, Slow
}

public class WeaponHandler : MonoBehaviour
{
    
    private IWeapon[] weapon = new IWeapon[1]; //cantidad de armas que puede llevar a la vez
    
    
    public void CallPrimaryUse(int num, Vector2 direction)
    {
        weapon[num].PrimaryUse(direction);
    }

    public void CallSecondaryUse(int num, Vector2 direction)
    {
        weapon[num].SecondaryUse(direction);
    }

    public void SetWeapon(int num, IWeapon iweapon)
    {
        weapon[num] = iweapon;
    }
}

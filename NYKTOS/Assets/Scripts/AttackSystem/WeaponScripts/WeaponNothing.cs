using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNothing : Weapon
{
    public override void PrimaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        Debug.Log("PrimaryAttack");
    }

    public override void SecondaryUse(Vector2 direction, int damage, AttackType attackType)
    {
        Debug.Log("SecondaryAttack");
    }
}

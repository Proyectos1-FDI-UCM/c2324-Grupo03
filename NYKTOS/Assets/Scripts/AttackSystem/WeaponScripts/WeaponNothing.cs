using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNothing : MonoBehaviour, IWeapon
{
    public void PrimaryUse()
    {
        Debug.Log("PrimaryAttack");
    }

    public void SecondaryUse()
    {
        Debug.Log("SecondaryAttack");
    }
}

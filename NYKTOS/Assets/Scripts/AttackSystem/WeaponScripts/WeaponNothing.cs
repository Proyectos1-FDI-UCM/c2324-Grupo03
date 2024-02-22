using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNothing : MonoBehaviour, IWeapon
{
    public void PrimaryUse(Vector2 direction)
    {
        Debug.Log("PrimaryAttack");
    }

    public void SecondaryUse(Vector2 direction)
    {
        Debug.Log("SecondaryAttack");
    }
}

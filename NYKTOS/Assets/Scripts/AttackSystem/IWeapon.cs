using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    
    public abstract void PrimaryUse(Vector2 direction, int _damage, AttackType attackType);

    public abstract void SecondaryUse(Vector2 direction, int _damage, AttackType attackType);

}



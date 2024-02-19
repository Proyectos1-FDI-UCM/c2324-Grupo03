using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    
    private IWeapon[] weapon = new IWeapon[1]; //cantidad de armas que puede llevar a la vez
    
    
    public void CallPrimaryUse(int num)
    {
        weapon[num].PrimaryUse();
    }

    public void CallSecondaryUse(int num)
    {
        weapon[num].SecondaryUse();
    }

    private void Awake() //Borrar esto mas adelante !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        weapon[0] = GetComponent<WeaponNothing>();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData", order = 0)]
public class SaveData : ScriptableObject
{
    [SerializeField]
    private int _night = 0;
    private int Night 
    {
        get{return _night;}
    }

    private WeaponType _weaponType = WeaponType.Club;

    public void EvolveWeapon(WeaponType newType)
    {
        if (_weaponType == WeaponType.Club && newType != WeaponType.Club)
        {
            _weaponType = newType;
        }
    }

    public void AdvanceNight()
    {
        _night++;
    }

    public void Reset(){
        _night = 0;
    }
}

public enum WeaponType
{
    Club,
    Staff,
    Bow,
    Whip
}

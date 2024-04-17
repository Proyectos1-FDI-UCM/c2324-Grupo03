using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public enum AttackType
{
    Default, Fire, Slow
}

public class WeaponHandler : MonoBehaviour
{
    #region references
    [SerializeField] private WeaponSOEmmiter _weaponUpgrade;
    #endregion

    #region properties
    [System.Serializable]
    public struct weaponStruct
    {
        [SerializeField]
        public WeaponScriptableObject scriptableWeapon;

        [SerializeField]
        public AttackType attackType;
    }

    [SerializeField] 
    private weaponStruct weapons = new weaponStruct();
    public weaponStruct MyWeapons
    {
        get{return weapons;}
    }

    private GameObject instantiatedPrefab;

    //cooldowns
    private Cooldown primaryCooldown = new Cooldown(0);

    private Cooldown secondaryCooldown = new Cooldown(0);

    #endregion

    public void CallPrimaryUse(Vector2 direction)
    {
        if (!primaryCooldown.IsCooling())
        {
            CheckChildren();
            instantiatedPrefab.GetComponent<Weapon>().PrimaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType);

            primaryCooldown = new Cooldown(weapons.scriptableWeapon.primaryAttackCooldown);
            primaryCooldown.StartCooldown();
        }
        
    }

    public void CallSecondaryUse(Vector2 direction)
    {
        if(!secondaryCooldown.IsCooling())
        {
            CheckChildren();
            instantiatedPrefab.GetComponent<Weapon>().SecondaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType);
            secondaryCooldown = new Cooldown(weapons.scriptableWeapon.secondaryAttackCooldown);
            secondaryCooldown.StartCooldown();
        }
        
    }

    private void CheckChildren()
    {
        if (instantiatedPrefab == null)
        {
            instantiatedPrefab = Instantiate(weapons.scriptableWeapon.weaponPrefab, transform);
            instantiatedPrefab.name = weapons.scriptableWeapon.weaponPrefab.name;
        }
        else if (instantiatedPrefab.name != weapons.scriptableWeapon.weaponPrefab.name)
        {
            print("a");
            Destroy(instantiatedPrefab);
            instantiatedPrefab = Instantiate(weapons.scriptableWeapon.weaponPrefab, transform);
            instantiatedPrefab.name = weapons.scriptableWeapon.weaponPrefab.name;
        }
    }

    public void SetWeapon(WeaponScriptableObject setWeapon)
    {
        weapons.scriptableWeapon = setWeapon;
    }

    public void SetDamageType(AttackType attackType)
    {
        weapons.attackType = attackType;
    }

    private void Start()
    {
        if (_weaponUpgrade != null)
        _weaponUpgrade.Perform.AddListener(SetWeapon);
    }
}

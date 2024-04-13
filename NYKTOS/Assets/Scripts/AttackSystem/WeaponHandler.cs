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
    #endregion\

    #region properties
    [System.Serializable]
    struct weaponStruct
    {
        [SerializeField]
        public WeaponScriptableObject scriptableWeapon;

        [SerializeField]
        public AttackType attackType;
    }

    [SerializeField] private weaponStruct weapons = new weaponStruct();

    private GameObject instantiatedPrefab;


    private bool primaryOnCooldown = false;
    private bool secondaryOnCooldown = false;
    #endregion

    public void CallPrimaryUse(Vector2 direction)
    {
        if (!primaryOnCooldown)
        {
            CheckChildren();
            instantiatedPrefab.GetComponent<Weapon>().PrimaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType);
            StartCoroutine(PrimaryCooldown());
        }
        
    }

    public void CallSecondaryUse(Vector2 direction)
    {
        if (!secondaryOnCooldown)
        {
            CheckChildren();
            instantiatedPrefab.GetComponent<Weapon>().SecondaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType);
            StartCoroutine (SecondaryCooldown());
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

    private IEnumerator PrimaryCooldown()
    {
        primaryOnCooldown = true;
        yield return new WaitForSeconds(weapons.scriptableWeapon.primaryAttackCooldown);
        primaryOnCooldown = false;
    }

    private IEnumerator SecondaryCooldown()
    {
        secondaryOnCooldown = true;
        yield return new WaitForSeconds(weapons.scriptableWeapon.secondaryAttackCooldown);
        secondaryOnCooldown = false;
    }
}

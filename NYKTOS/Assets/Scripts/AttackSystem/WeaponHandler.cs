using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum AttackType
{
    Default, Fire, Slow
}

public class WeaponHandler : MonoBehaviour
{
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
    
    public void CallPrimaryUse(Vector2 direction)
    {
        CheckChildren();
        instantiatedPrefab.GetComponent<Weapon>().PrimaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType, this);
    }

    public void CallSecondaryUse(Vector2 direction)
    {
        CheckChildren();
        instantiatedPrefab.GetComponent<Weapon>().SecondaryUse(direction, weapons.scriptableWeapon.damage, weapons.attackType, this);
    }

    private void CheckChildren()
    {
        if (instantiatedPrefab == null)
        {
            instantiatedPrefab = Instantiate(weapons.scriptableWeapon.weaponPrefab, transform);
        }
        else if (instantiatedPrefab != weapons.scriptableWeapon.weaponPrefab)
        {
            Destroy(instantiatedPrefab );
            instantiatedPrefab = Instantiate(weapons.scriptableWeapon.weaponPrefab, transform);
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

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Tipo de ataque elemental que existe en el juego
/// </summary>
public enum AttackType
{
    Default, Fire, Slow
}

/// <summary>
/// Procesa la informacion del scriptable object de tipo weapon que tiene referenciado. Controla los cooldowns del arma, los prefabs que instancia, su daño...
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    #region references
    [SerializeField] private WeaponSOEmmiter _weaponUpgrade;
    #endregion

    #region properties
    [System.Serializable]
    struct WeaponStruct
    {
        [SerializeField]
        public WeaponScriptableObject scriptableWeapon;

        [SerializeField]
        public AttackType attackType;
    }

    [SerializeField] 
    private WeaponStruct weapons = new WeaponStruct();
    
    public WeaponScriptableObject Weapon
    {
        get{return weapons.scriptableWeapon;}
    }

    private GameObject instantiatedPrefab;

    //cooldowns
    private Cooldown primaryCooldown = new Cooldown(0);

    private Cooldown secondaryCooldown = new Cooldown(0);

    #endregion

    /// <summary>
    /// Procesa el ataque primario del arma y lo aplica hacia el vector direccion
    /// </summary>
    /// <param name="direction"></param>
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
    /// <summary>
    /// Procesa el ataque secundario del arma y lo aplica hacia el vector direccion
    /// </summary>
    /// <param name="direction"></param>
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

    /// <summary>
    /// Se generan gameobjects hijo en el jugador que contienen los scripts que logica del arma como tal.
    /// Si se ha cambiado de arma, se borra el hijo con la logica del arma anterior y se instancia uno nuevo.
    /// </summary>
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

    /// <summary>
    /// Cambia la referencia del arma que se le asigna
    /// </summary>
    /// <param name="setWeapon"></param>
    public void SetWeapon(WeaponScriptableObject setWeapon)
    {
        weapons.scriptableWeapon = setWeapon;
    }

    /// <summary>
    /// Cambia el tipo de daño del arma
    /// </summary>
    /// <param name="attackType"></param>
    public void SetDamageType(AttackType attackType)
    {
        weapons.attackType = attackType;
    }

    private void Start()
    {
        if (_weaponUpgrade != null)
        _weaponUpgrade.Perform.AddListener(SetWeapon);
    }

    void OnDestroy()
    {
        if(_weaponUpgrade != null)
        _weaponUpgrade.Perform.RemoveListener(SetWeapon);
    }
}

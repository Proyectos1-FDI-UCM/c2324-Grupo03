using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Scriptable Object que almacena propiedades del arma en cuestion. Si se quiere cambiar atributos del arma se deben cambiar desde aqui.
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    /// <summary>
    /// Nombre del arma
    /// </summary>
    public string weaponName { get { return _weaponName; } }
    [SerializeField]
    private string _weaponName;

    /// <summary>
    /// Daño del arma
    /// </summary>
    public int damage { get { return _damage; } }
    [SerializeField]
    private int _damage = 0;

    [SerializeField]
    private float _primaryAttackCooldown = 0f;
    /// <summary>
    /// Cooldown del ataque principal
    /// </summary>
    public float primaryAttackCooldown { get { return _primaryAttackCooldown; } }

    [SerializeField]
    private float _secondaryAttackCooldown = 0f;
    /// <summary>
    /// Cooldown del ataque secundario
    /// </summary>
    public float secondaryAttackCooldown { get { return _secondaryAttackCooldown; } }

    [SerializeField]
    private Sprite _weaponImage;
    /// <summary>
    /// Imagen para aplicar al HUD
    /// </summary>
    public Sprite weaponImage {  get { return _weaponImage; } }

    [SerializeField]
    private GameObject _weaponPrefab;
    /// <summary>
    /// GameObject con la logica interna del arma.
    /// </summary>
    public GameObject weaponPrefab {  get { return _weaponPrefab; } }

}

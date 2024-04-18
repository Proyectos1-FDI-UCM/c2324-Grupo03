using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName { get { return _weaponName; } }
    [SerializeField]
    private string _weaponName;

    public int damage { get { return _damage; } }
    [SerializeField]
    private int _damage = 0;

    [SerializeField]
    private float _primaryAttackCooldown = 0f;
    public float primaryAttackCooldown { get { return _primaryAttackCooldown; } }

    [SerializeField]
    private float _secondaryAttackCooldown = 0f;
    public float secondaryAttackCooldown { get { return _secondaryAttackCooldown; } }

    [SerializeField]
    private Sprite _weaponImage;
    public Sprite weaponImage {  get { return _weaponImage; } }

    [SerializeField]
    private GameObject _weaponPrefab;
    public GameObject weaponPrefab {  get { return _weaponPrefab; } }

}

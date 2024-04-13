using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;




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
    private Sprite _weaponImage;
    public Sprite weaponImage {  get { return _weaponImage; } }

    [SerializeField]
    private GameObject _weaponPrefab;
    public GameObject weaponPrefab {  get { return _weaponPrefab; } }

}

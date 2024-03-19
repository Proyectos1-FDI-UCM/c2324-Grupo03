using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariant : MonoBehaviour
{
    [SerializeField]
    GameObject _defaultSprite;
    [SerializeField]
    GameObject _magentaSprite;
    [SerializeField]
    GameObject _cyanSprite;

    #region debug
    [Header("DEBUG")]
    
    [SerializeField]
    bool debug = true;

    [SerializeField]
    AttackType _attackType = AttackType.Default;
    #endregion

    public void SetVariant(AttackType attack)
    {
        if(TryGetComponent<WeaponHandler>(out WeaponHandler weaponHandler))
        {
            weaponHandler.SetDamageType(0, attack);
        }

        if (attack == AttackType.Default)
        {
            Instantiate(_defaultSprite, transform);
        }
        else if (attack == AttackType.Slow)
        {
            Instantiate(_cyanSprite, transform);
        }
        else if (attack == AttackType.Fire)
        {
            Instantiate(_magentaSprite, transform);
        }
    }

    private void Start()
    {
        if (debug)
        {
            SetVariant(_attackType);
        }
    }
}

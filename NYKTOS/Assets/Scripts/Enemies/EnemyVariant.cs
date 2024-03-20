using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EnemyVariant : MonoBehaviour
{
    [SerializeField]
    SpriteLibraryAsset _magentaSprite;
    [SerializeField]
    SpriteLibraryAsset _cyanSprite;

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

        SpriteLibrary spriteLibrary = GetComponentInChildren<SpriteLibrary>();
        if (spriteLibrary != null)
        {
            if (attack == AttackType.Slow)
            {

                spriteLibrary.spriteLibraryAsset = _cyanSprite;
            }
            else if (attack == AttackType.Fire)
            {
                spriteLibrary.spriteLibraryAsset = _magentaSprite;
            }
        }
    }

    private void Awake()
    {
        if (debug)
        {
            SetVariant(_attackType);
        }
    }
}

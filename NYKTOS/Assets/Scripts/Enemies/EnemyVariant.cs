using UnityEngine;
using UnityEngine.U2D.Animation;

public class EnemyVariant : MonoBehaviour
{
    ///GUILLERMO Y MARIA

    /// <summary>
    /// Script que se encarga de cambiar el daño del enemigo junto con el sprite y el material de las particulas 
    /// </summary>

    #region references
    [Header("Sprites")] 
    [SerializeField]
    SpriteLibraryAsset _magentaSprite;
    [SerializeField]
    SpriteLibraryAsset _cyanSprite;

    [Header("Materials")]

    [SerializeField]
    Material _cyanMaterial;
    [SerializeField]
    Material _magentaMaterial;
    #endregion

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
            weaponHandler.SetDamageType(attack);
        }

        SpriteLibrary spriteLibrary = GetComponentInChildren<SpriteLibrary>();
        if (spriteLibrary != null)
        {
            if (attack == AttackType.Slow)
            {
                spriteLibrary.spriteLibraryAsset = _cyanSprite;
                ParticleSystemRenderer settings = GetComponentInChildren<ParticleSystemRenderer>();

                settings.material = _cyanMaterial;
            }
            else if (attack == AttackType.Fire)
            {
                spriteLibrary.spriteLibraryAsset = _magentaSprite;
                ParticleSystemRenderer settings = GetComponentInChildren<ParticleSystemRenderer>();

                settings.material = _magentaMaterial;
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

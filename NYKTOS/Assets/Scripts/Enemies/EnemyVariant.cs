using UnityEngine;
using UnityEngine.U2D.Animation;
/// <summary>
/// Clase que se encarga de cambiar el daño del enemigo y como consecuencia su sprite library y el material de las particulas 
/// </summary>
public class EnemyVariant : MonoBehaviour
{
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

    /// <summary>
    /// Método responsable de asignar el tipo de ataque, cambiar la library y el material de las particulas
    /// </summary>
    /// <param name="attack"> Es el tipo de ataque (normal, fire o slow)</param>
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

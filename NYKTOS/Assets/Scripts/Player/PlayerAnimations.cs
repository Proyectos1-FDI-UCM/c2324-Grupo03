using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimations : MonoBehaviour {
    //script para las animaciones de recibir daño, de muerte y de revivir de Hémera
    //Recordatorio para Maria: aun no se ha implementado nada para el revive


    #region references
    private SpriteLibrary spriteLibrary;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _explosionParticleSystem;
    #endregion

    #region Parameters Hurt
    [Header("Hurt")]
    [SerializeField] 
    private float _opacity = 0.7f;
    [SerializeField] 
    private float _opacityTime = 1.0f;

    [Space(10)]
    [Header("    Particles")]
    [SerializeField]
    private float _emisionHurt = 10f;
    [SerializeField]
    private float _durationHurt = 1f;
    [SerializeField]
    private float _speedHurt = 1f;
    [SerializeField]
    private float _lifeTimeHurt = 5f;
    [SerializeField]
    private float _maxParticlesHurt = 5f;
    #endregion

    #region Parameters Dead
    [Header("Dead")]
    [SerializeField] 
    private float _deadTime = 2f;
    [SerializeField]
    private SpriteLibraryAsset _deathskin;

    [Space(10)]
    [Header("    Particles")]
    [SerializeField]
    private float _emisionDead = 40f;
    [SerializeField]
    private float _durationDead = 2f;
    [SerializeField]
    private float _speedDead = 1f;
    [SerializeField]
    private float _lifeTimeDead = 5f;
    [SerializeField]
    private float _maxParticlesDead = 5f;
    #endregion

    #region Parameters Revive
    [Header("Revive")]
    [SerializeField] 
    private float _reviveTime = 2f;
    [SerializeField]
    private SpriteLibraryAsset _aliveskin;
    #endregion



    private void Start() {

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        _explosionParticleSystem = GetComponent<ParticleSystem>();
        _explosionParticleSystem.Stop();
    }

    #region Die
    public void StartDie() {
        StartCoroutine(DieAnimation());
        StartCoroutine(PlayerDeadParticles());

    }
    private IEnumerator DieAnimation() {
        _animator.Play("Die");

        yield return new WaitForSeconds(_deadTime);

        spriteLibrary.spriteLibraryAsset = _deathskin;
    }

    public IEnumerator PlayerDeadParticles() {

        var mainModule = _explosionParticleSystem.main;
        mainModule.startSpeed = _speedDead;
        mainModule.duration = _durationDead;
        var emissionModule = _explosionParticleSystem.emission;
        emissionModule.rateOverTime = _emisionDead;

        _explosionParticleSystem.Play();
        yield return new WaitForSeconds(_durationDead);
        _explosionParticleSystem.Stop();
    }


    #endregion

    #region Hurt
    public void StartHurt() {
        StartCoroutine(HurtAnimation());
        StartCoroutine(PlayerHurtParticles());
    }

    private IEnumerator HurtAnimation() {
        
        Color color = _spriteRenderer.color;
        float opacityAnterior = color.a;
        
        color.a = _opacity;

        _spriteRenderer.color = color;

        yield return new WaitForSeconds(_opacityTime);
        color.a = opacityAnterior;
        _spriteRenderer.color = color;
    }

    public IEnumerator PlayerHurtParticles() {

        var mainModule = _explosionParticleSystem.main;
        mainModule.startSpeed = _speedDead;

        mainModule.duration = _durationHurt;

        var emissionModule = _explosionParticleSystem.emission;
        emissionModule.rateOverTime = _emisionDead;

        _explosionParticleSystem.Play();

        yield return new WaitForSeconds(_durationHurt);
        _explosionParticleSystem.Stop();
    }

    #endregion


    #region Revive
    public void StartRevive() {
        StartCoroutine(ReviveAnim());
    }
    private IEnumerator ReviveAnim() {


        spriteLibrary.spriteLibraryAsset = _aliveskin;

        yield return new WaitForSeconds(_reviveTime);
    }
    #endregion
}

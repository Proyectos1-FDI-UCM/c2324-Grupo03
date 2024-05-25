using System.Collections;
using UnityEngine;

/// <summary>
/// Cambio del material/emision/etc de la particulas cuando el enemigo sea atacado
/// </summary>
public class EnemyParticleEffect : MonoBehaviour 
{

    #region references
    private SpriteRenderer _spriteRenderer;
    #region materials
    [Header("Materials")]
    [SerializeField]
    private Material _particleMaterialToChange;

    private Material _previousParticleMaterial;

    [SerializeField]
    private Material _spriteMaterialToChange;

    private Material _previousSpriteMaterial;

    private ParticleSystem explosionParticleSystem;

    #endregion

    #endregion

    #region ParametersBeforeChange
    //Anteriores
    [Header("Before")]
    [SerializeField]
    private float _emisionNormal = 10f;

    [SerializeField]
    private float _speedNormal = 1f;

    [SerializeField]
    private float _sizeNormal = 10f;

    [Header("Size")]
    [SerializeField]
    private float _sizeExplosion = 1f;

    #endregion

    #region ParametersAfterChange

    [Header("After")]
    [SerializeField]
    private float _emisionInicial = 10f;

    [SerializeField]
    private float _emisionFinal = 20f;

    [SerializeField]
    private float _speedInicial = 1f;

    [SerializeField]
    private float _speedFinal = 5f;

    #endregion

    /// <summary>
    /// Método que inicia las dos corrutinas
    /// </summary>
    public void PlayEffect() 
    {
        StartCoroutine(ChangeEnemyParticles());
        StartCoroutine(ChangeEnemySprites());
    }

    private void Start() 
    {
        _previousParticleMaterial = GetComponent<ParticleSystemRenderer>().material;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _previousSpriteMaterial = _spriteRenderer.material;
        explosionParticleSystem = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Corrutina responsable de cambiar las caracteristicas de la particula
    /// </summary>
    private IEnumerator ChangeEnemyParticles()
    {
        ParticleSystemRenderer settings = GetComponent<ParticleSystemRenderer>();
        settings.material = _particleMaterialToChange;

        var mainModule = explosionParticleSystem.main;
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedInicial, _speedFinal);

        mainModule.startSize = _sizeExplosion;

        var emissionModule = explosionParticleSystem.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionInicial, _emisionFinal);


        yield return new WaitForSeconds(0.4f);

        mainModule.startSize = _sizeNormal;
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedNormal);

        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionNormal);

        settings.material = _previousParticleMaterial;
    }

    /// <summary>
    /// Corrutina responsable de cambiar el color del sprite del enemigo
    /// </summary>
    private IEnumerator ChangeEnemySprites() 
    {
        int changes = 0, maxChanges = 4;
        while (changes < maxChanges) 
        {
            _spriteRenderer.material = (changes % 2 == 0) ? _spriteMaterialToChange : _previousSpriteMaterial;
            changes++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

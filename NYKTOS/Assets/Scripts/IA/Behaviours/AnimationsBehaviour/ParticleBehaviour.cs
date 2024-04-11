using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour, IBehaviour {

    /// <summary>
    /// Cambio del material de la particulas cuando sea atacado
    /// </summary>
 
    private SpriteRenderer _spriteRenderer;

    [Header("Materials")]
    [SerializeField]
    private Material _particleMaterialToChange;
    private Material _previousParticleMaterial;
    [SerializeField]
    private Material _spriteMaterialToChange;
    private Material _previousSpriteMaterial;

    [SerializeField]
    private ParticleSystem explosionParticleSystem;
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


    [Header("Changes")]
    [SerializeField]
    private float _emisionInicial = 10f;
    [SerializeField]
    private float _emisionFinal = 20f;
    [SerializeField]
    private float _speedInicial = 1f;
    [SerializeField]
    private float _speedFinal = 5f;


    public void PerformBehaviour() {
        StartCoroutine(ChangeParticles());

    }
    private void Start() {
        _previousParticleMaterial = GetComponentInParent<HealthComponent>().GetComponentInChildren<ParticleSystemRenderer>().material;
        _spriteRenderer = GetComponentInParent<HealthComponent>().GetComponentInChildren<SpriteRenderer>();
        _previousSpriteMaterial = _spriteRenderer.material;
    }

    private IEnumerator ChangeParticles() 
    {

        ParticleSystemRenderer settings = GetComponentInParent<HealthComponent>().GetComponentInChildren<ParticleSystemRenderer>();
        settings.material = _particleMaterialToChange;

        
        _spriteRenderer.material = _spriteMaterialToChange;

        var mainModule = explosionParticleSystem.main;
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedInicial, _speedFinal);

        mainModule.startSize = _sizeExplosion;

        var emissionModule = explosionParticleSystem.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionInicial, _emisionFinal);

       
        yield return new WaitForSeconds(0.6f);

        mainModule.startSize = _sizeNormal;
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedNormal);

        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionNormal);

        _spriteRenderer.material = _previousSpriteMaterial;

        settings.material = _previousParticleMaterial;
    }
}

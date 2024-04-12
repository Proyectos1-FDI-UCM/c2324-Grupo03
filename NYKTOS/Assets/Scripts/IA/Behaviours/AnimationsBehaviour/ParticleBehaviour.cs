using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleEffect : MonoBehaviour {

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


    public void PlayEffect() {
        StartCoroutine(ChangeParticles());
        Debug.Log("sprite");
        StartCoroutine(ChangeSprites());
    }

    private void Start() {
        _previousParticleMaterial = GetComponent<ParticleSystemRenderer>().material;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _previousSpriteMaterial = _spriteRenderer.material;
        explosionParticleSystem = GetComponent<ParticleSystem>();
    }

    private IEnumerator ChangeParticles() 
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
    private IEnumerator ChangeSprites() {

        int changes = 0, maxChanges=4;

        Debug.Log("sprite");
        while (changes< maxChanges) {

            _spriteRenderer.material = (changes % 2 == 0) ? _spriteMaterialToChange : _previousSpriteMaterial;

            changes++;

            yield return new WaitForSeconds(0.1f);
        }


        //_spriteRenderer.material = _previousSpriteMaterial;

 
    }

}

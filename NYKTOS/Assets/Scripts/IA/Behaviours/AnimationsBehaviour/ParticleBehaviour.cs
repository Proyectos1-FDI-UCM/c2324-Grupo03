using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour, IBehaviour {

    /// <summary>
    /// Cambio del material de la particulas cuando sea atacado
    /// </summary>
    [Header("Materials")]
    [SerializeField]
    private Material _materialToChange;

    private Material _previousMaterial;


    [SerializeField]
    private ParticleSystem explosionParticleSystem;
    //Anteriores
    [Header("Before")]
    [SerializeField]
    private float _emisionNormal = 10f;
    [SerializeField]
    private float _speedNormal = 1f;


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
        _previousMaterial = GetComponentInParent<HealthComponent>().GetComponentInChildren<ParticleSystemRenderer>().material;
    }

    private IEnumerator ChangeParticles() 
    {

        ParticleSystemRenderer settings = GetComponentInParent<HealthComponent>().GetComponentInChildren<ParticleSystemRenderer>();
        settings.material = _materialToChange;

        var mainModule = explosionParticleSystem.main;
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedInicial, _speedFinal);


        var emissionModule = explosionParticleSystem.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionInicial, _emisionFinal);

       
        yield return new WaitForSeconds(1f);

        
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(_speedNormal);

        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(_emisionNormal);


        settings.material = _previousMaterial;
    }
}

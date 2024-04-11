using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour, IBehaviour {

    /// <summary>
    /// Cambio del material de la particulas cuando sea atacado
    /// </summary>

    [SerializeField]
    private Material _materialToChange;

    private Material _previousMaterial;

    private enum AnimationType 
    {
        Change, BackToDefault
    }


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

        yield return new WaitForSeconds(1f);

        settings.material = _previousMaterial;
    }
}

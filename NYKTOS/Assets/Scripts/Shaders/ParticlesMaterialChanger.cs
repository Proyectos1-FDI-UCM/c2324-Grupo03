using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ParticlesMaterialChanger : MonoBehaviour
{
    //El componente de particulas los puse todos como hijos
    [SerializeField]
    private BoolEmitter inversionEffect;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material inversionMaterial;

    ParticleSystemRenderer currentRenderer;
    void Start() {

        currentRenderer = GetComponentInChildren<ParticleSystemRenderer>();
        currentRenderer.material = defaultMaterial;

        inversionEffect.Perform.AddListener(ChangeSprite);
    }
    private void ChangeSprite(bool swapCondition) {
        if (swapCondition) {
            currentRenderer.material = inversionMaterial;
        } else {
            currentRenderer.material = defaultMaterial;
        }
    }
}

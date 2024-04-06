using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnDestroy()
    {
        inversionEffect.Perform.RemoveListener(ChangeSprite);
    }

    private void ChangeSprite(bool swapCondition) {
        if (swapCondition) {
            currentRenderer.material = inversionMaterial;
        } else {
            currentRenderer.material = defaultMaterial;
        }
    }
}

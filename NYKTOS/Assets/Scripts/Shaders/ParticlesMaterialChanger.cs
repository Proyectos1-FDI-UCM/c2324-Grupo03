using UnityEngine;
/// <summary>
/// Clase respondable para que se cambie el material de las particulas como queramos, se ha utilizado principalmente para elegir si quieres o no que la particulas se vean durante la noche
/// </summary>
public class ParticlesMaterialChanger : MonoBehaviour
{
    //recordatorio para Maria: El componente de particulas estan en el hijo
    [SerializeField]
    private BoolEmitter inversionEffect;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material inversionMaterial;

    ParticleSystemRenderer currentRenderer;

    void Start() 
    {
        currentRenderer = GetComponentInChildren<ParticleSystemRenderer>();
        currentRenderer.material = defaultMaterial;
        inversionEffect.Perform.AddListener(ChangeMaterial);
    }

    void OnDestroy()
    {
        inversionEffect.Perform.RemoveListener(ChangeMaterial);
    }

    /// <summary>
    /// Cambia de material en funcion de si es de dia o de noche de las particulas
    /// </summary>
    /// <param name="swapCondition"></param>
    private void ChangeMaterial(bool swapCondition) {
        if (swapCondition) 
        {
            currentRenderer.material = inversionMaterial;
        } else {
            currentRenderer.material = defaultMaterial;
        }
    }
}

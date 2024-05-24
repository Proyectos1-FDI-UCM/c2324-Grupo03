using UnityEngine;

/// <summary>
/// Cambia su material en base a un evento de una instancia 
/// del scriptable object InversionEffect
/// 
/// Script de Iker y Marco
/// </summary>
[RequireComponent(typeof(Renderer))]
public class InversionPerformer : MonoBehaviour
{

    /// <summary>
    /// Instancia de scriptable object que lanza el evento
    /// </summary>
    [SerializeField] 
    private BoolEmitter inversionEffect;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material inversionMaterial;

    Renderer currentRenderer;

    void Start()
    {
        currentRenderer = GetComponent<Renderer>();
        currentRenderer.material = defaultMaterial;

        ///Suscribe el método ChangeMaterial al evento del scriptable object en inversionEffect
        inversionEffect.Perform.AddListener(ChangeMaterial);
    }

    void OnDestroy()
    {
        inversionEffect.Perform.RemoveListener(ChangeMaterial);
    }

    /// <summary>
    /// Dependiendo de un booleano, se cambiará el material al normal o al invertido, aplicable a todos los elementos que queramos que se inviertan, como los elementos del entorno.
    /// </summary>
    /// <param name="swapCondition"></param>
    private void ChangeMaterial(bool swapCondition)
    {
        if (swapCondition) 
        {
            currentRenderer.material = inversionMaterial;
        }
        else
        {
            currentRenderer.material = defaultMaterial;
        }
    }
}
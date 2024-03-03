using UnityEngine;

// Codigo de Iker :D
//Este codigo es para aplicar a un objeto por separado que tenga un renderer, aplicarle el material Inversion
[RequireComponent(typeof(Renderer))]
public class CambioMaterial : MonoBehaviour
{

    [SerializeField] 
    InversionEffect inversionEffect;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material swapMaterial;

    Renderer currentRenderer;

    void Start()
    {
        currentRenderer = GetComponent<Renderer>();
        currentRenderer.material = defaultMaterial;

        inversionEffect.InversionEvent.AddListener(ChangeMaterial);
    }

    private void ChangeMaterial(bool swapCondition)
    {
        if (swapCondition) 
        {
            currentRenderer.material = swapMaterial;
        }
        else
        {
            currentRenderer.material = defaultMaterial;
        }
    }
}
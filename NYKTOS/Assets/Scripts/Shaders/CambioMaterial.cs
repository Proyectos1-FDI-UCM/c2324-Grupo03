using UnityEngine;

// Codigo de Iker :D
//Este codigo es para aplicar a un objeto por separado que tenga un renderer, aplicarle el material Inversion
[RequireComponent(typeof(SpriteRenderer))]
public class CambioMaterial : MonoBehaviour
{
    /*
        Mierdas que voy haciendo: (~ Marco)

        - Cambiados nombres varios

        - Puesto defaultMaterial en privado y serializado

        - Eliminado timer

        - Eliminado update entero
    */

    [SerializeField]
    private Material defaultMaterial;

    SpriteRenderer currentSpriteRenderer;

    void Start()
    {
        currentSpriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteRenderer.material = defaultMaterial;

        //InversionEvent inversorEvent = InversionManager.Instance().getEvent();

        //// Suscribir un método al evento
        //inversorEvent += CambiarMaterial;
    }

    private void CambiarMaterial()
    {
        //currentSpriteRenderer = InversionManager.Instance().getCycleMaterial;
    }
}
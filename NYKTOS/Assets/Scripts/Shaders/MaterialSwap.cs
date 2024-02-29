using System;
using UnityEngine;

// Codigo de Iker :D
//Este codigo es para aplicar a un objeto por separado que tenga un renderer, aplicarle el material Inversion
[RequireComponent(typeof(Renderer))]
public class CambioMaterial : MonoBehaviour
{
    /*
        Mierdas que voy haciendo: (~ Marco)

        - Cambiados nombres varios

        - Puesto defaultMaterial en privado y serializado

        - Eliminado timer

        - Eliminado update entero
        
        - Añadido metodo change material

        - Suscrito changeMaterial a un evento de InversionManager

        */

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material swapMaterial;

    Renderer currentRenderer;

    void Start()
    {
        currentRenderer = GetComponent<Renderer>();
        currentRenderer.material = defaultMaterial;

        InversionManager.Instance.evento += ChangeMaterial;
    }

    private void ChangeMaterial()
    {
        if (InversionManager.Instance.getSwap) 
        {
            currentRenderer.material = swapMaterial;
        }
        else
        {
            currentRenderer.material = defaultMaterial;
        }
    }
}
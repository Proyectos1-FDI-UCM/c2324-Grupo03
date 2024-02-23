using UnityEngine;

public class CambioMaterialObjeto : MonoBehaviour
{
    public Material materialDia; // Material de día
    public Material materialInversionColores; // Material de inversión de colores

    private Renderer renderer; // Componente Renderer del objeto

    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("El objeto no tiene un componente Renderer.");
        }
    }

    public void CambiarMaterial()
    {
        if (renderer != null)
        {
            // Verificar si el material del objeto es el material de día
            if (renderer.sharedMaterial == materialDia)
            {
                // Cambiar al material de inversión de colores
                renderer.material = materialInversionColores;
            }
            else
            {
                // Restaurar al material de día
                renderer.material = materialDia;
            }
        }
    }
}


using UnityEngine;

public class CambioMaterialObjeto : MonoBehaviour
{
    public Material materialDia; // Material de d�a
    public Material materialInversionColores; // Material de inversi�n de colores

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
            // Verificar si el material del objeto es el material de d�a
            if (renderer.sharedMaterial == materialDia)
            {
                // Cambiar al material de inversi�n de colores
                renderer.material = materialInversionColores;
            }
            else
            {
                // Restaurar al material de d�a
                renderer.material = materialDia;
            }
        }
    }
}


using UnityEngine;

public class AreaIluminada : MonoBehaviour
{
    public Material materialInversionColores; // Asigna el material de inversi�n de colores en el Inspector

    // Almacena el material por defecto del objeto
    private Material materialPorDefecto;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha entrado en el �rea es afectado por la inversi�n de colores
        if (other.CompareTag("ObjetoAfectado"))
        {
            // Guarda una referencia al material por defecto del objeto
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                materialPorDefecto = renderer.material;
                // Cambia el material del objeto al material de inversi�n de colores
                if (materialInversionColores != null)
                {
                    renderer.material = materialInversionColores;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que ha salido del �rea es afectado por la inversi�n de colores
        if (other.CompareTag("ObjetoAfectado"))
        {
            // Restaura el material por defecto del objeto
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null && materialPorDefecto != null)
            {
                renderer.material = materialPorDefecto;
            }
        }
    }
}

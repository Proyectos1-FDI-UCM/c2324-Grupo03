using UnityEngine;

public class CambioMaterial : MonoBehaviour
{
    public Material nuevoMaterial; // El nuevo material que deseas aplicar al objeto
    private Material MaterialPorDefecto;
    [SerializeField]
    private bool Noche;
    private float Timer = 4f;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("El objeto no tiene un componente Renderer.");
            return;
        }
        MaterialPorDefecto = renderer.material;
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Noche = !Noche;
            Timer = 4f;
        }
        CambiarMaterial();
    }

    void CambiarMaterial()
    {
        // Asegúrate de tener un material nuevo para aplicar
        if (nuevoMaterial != null && Noche)
        {
            // Asigna el nuevo material al objeto
            renderer.material = nuevoMaterial;
        }
        else if (nuevoMaterial != null && !Noche)
        {
            renderer.material = MaterialPorDefecto;
        }
    }
}
using UnityEngine;

// Codigo de Iker :D
//Este codigo es para aplicar a un objeto por separado que tenga un renderer, aplicarle el material Inversion
public class CambioMaterial : MonoBehaviour
{
    public Material nuevoMaterial;
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
        if (nuevoMaterial != null && Noche)
        {
            renderer.material = nuevoMaterial;
        }
        else if (nuevoMaterial != null && !Noche)
        {
            renderer.material = MaterialPorDefecto;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo de Iker :D
//No funciona del todo bien, ya que al tomar el material por default de un solo objeto hace que haya errores de aplicacion del material en el resto de objetos 
public class CambioMaterialGlobal : MonoBehaviour
{
    public Material nuevoMaterial;
    private Material MaterialPorDefecto;
    [SerializeField]
    private bool Noche;
    private float Timer = 4f;
    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers == null || renderers.Length == 0)
        {
            Debug.LogError("El objeto no tiene un componente Renderer.");
            return;
        }
        MaterialPorDefecto = renderers[0].material;
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
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = nuevoMaterial;
            }
        }
        else if (nuevoMaterial != null && !Noche)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = MaterialPorDefecto;
            }
        }
    }
}

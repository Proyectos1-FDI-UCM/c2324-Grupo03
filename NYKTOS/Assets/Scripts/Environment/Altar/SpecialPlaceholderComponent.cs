using UnityEngine;

/// <summary>
/// Clase responsable del comportamiento de los cimientos especiales (los de colores)
/// Permite que se registren al ser instanciados
/// Se lanza un evento cuando se construye o destruye una defensa en ellos
/// </summary>
public class SpecialPlaceholderComponent : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _registerPlaceholder;

    [SerializeField]
    private BoolEmitter _placeholderBuilt;


    public void PlaceholderBuilt() => _placeholderBuilt.InvokePerform(true);
    public void PlaceholderDestroyed() => _placeholderBuilt.InvokePerform(false);

    void Start()
    {
        _registerPlaceholder.InvokePerform();
    }

}

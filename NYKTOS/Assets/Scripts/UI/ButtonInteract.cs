using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Activa / Desactiva la interacci�n de un bot�n en la UI
/// Se emplea para restringir las acciones del jugador en el tutorial
/// </summary>
public class ButtonInteract : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter _canInteract;

    private Button _button;

    private void CanInteract(bool value) => _button.interactable = value;

    void Start()
    {
        _button = GetComponent<Button>();
        _canInteract.Perform.AddListener(CanInteract);
    }

    private void OnDestroy()
    {
        _canInteract.Perform.RemoveListener(CanInteract);
    }
}

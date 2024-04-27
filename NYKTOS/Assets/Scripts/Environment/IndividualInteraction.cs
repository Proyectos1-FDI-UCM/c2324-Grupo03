using UnityEngine;

/// <summary>
/// Permite suscribirse a un evento de tipo booleano que cambia el estado de interacción/// 
/// </summary>
public class IndividualInteraction : MonoBehaviour
{
    [SerializeField]
    private BoolEmitter _canInteractEmitter;

    private BuildingStateMachine _state;

    private void CanInteract(bool value)
    {
        _state.isInteractable = value;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();

        _canInteractEmitter.Perform.AddListener(CanInteract);
    }

    void OnDestroy()
    {
        _canInteractEmitter.Perform.RemoveListener(CanInteract);
    }
}

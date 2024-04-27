using UnityEngine;

/// <summary>
/// Todos los edificios del juego con los que se puede interactuar (cimientos, nexo, estatua) tienen este script
/// Implementa la interfaz de interacción
/// </summary>
public class BuildingController : MonoBehaviour, IInteractable
{
    #region references
    
    #endregion
    public void Interact()
    {
        if (TryGetComponent(out IBuilding building)) building.OpenMenu();
    }
}

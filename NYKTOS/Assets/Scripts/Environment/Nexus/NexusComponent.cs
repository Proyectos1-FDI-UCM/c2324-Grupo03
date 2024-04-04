using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    #region emmiters
    [SerializeField]
    private GenericEmitter _playerRevive;

    [SerializeField]
    private BoolEmitter _nexusInteract;
    #endregion

    private BuildingStateMachine _state;

    // Solo afecta a si se abre o no el menú. El evento de revivir se emite siempre
    private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;

    public void OpenMenu()
    {
        _playerRevive.InvokePerform();
        if (_state.isInteractable)
        {
            MenuManager.Instance.OpenNexusMenu();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(gameObject);

        _nexusInteract.Perform.AddListener(CanInteract);
    }
}

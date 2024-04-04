using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    //[Andrea] Review

    #region references
    [SerializeField] private BoolEmitter _placeholderInteract;

    private BuildingStateMachine _state;
    #endregion

    private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;
    public void OpenMenu()
    {
        if
        (
            _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt 
            && _state.isInteractable
        )
        {
            MenuManager.Instance.OpenDefenseMenu();
            UpdateCurrentPlaceHolder();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    private void UpdateCurrentPlaceHolder()
    {
        BuildingManager.Instance.CurrentPlaceholder = gameObject;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();

        _placeholderInteract.Perform.AddListener(CanInteract);
    }
}

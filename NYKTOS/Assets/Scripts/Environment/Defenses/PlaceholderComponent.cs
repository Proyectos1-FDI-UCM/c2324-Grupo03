using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    //[Marco] Not optimal
    [SerializeField]
    private GameStateMachine _stateMachine;

    #region references
    private BuildingStateMachine _state;
    #endregion

    public void OpenMenu()
    {
        if
        (
            _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt && 
            _stateMachine.GetCurrentState == GlobalStateIdentifier.Day
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
    }
}

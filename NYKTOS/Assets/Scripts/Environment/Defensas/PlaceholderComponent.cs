using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    #region references
    [SerializeField]
    private GameObject _defenseMenu;

    [SerializeField]
    private PlayerInput _playerInput;
    private BuildingStateMachine _state;
    #endregion
    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            //_playerControls.Player.Disable();
            //_playerControls.UI.Enable();
            _playerInput.SwitchCurrentActionMap("UI");
            _defenseMenu.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        _defenseMenu.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }
}

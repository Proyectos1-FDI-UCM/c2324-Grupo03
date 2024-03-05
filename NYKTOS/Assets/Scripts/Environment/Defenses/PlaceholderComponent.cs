using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    #region references
    [SerializeField]
    private MenuManager _menuManager;

    [SerializeField]
    private PlayerController _player;
    private BuildingStateMachine _state;
    [SerializeField]
    private BuildingManager _buildingManager;
    #endregion

    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            _player.playerControls.Player.Disable();
            _player.playerControls.UI.Enable();
            _menuManager.OpenMenu(0);
            UpdateCurrentPlaceHolder();
        }
    }

    public void CloseMenu()
    {
        _menuManager.CloseMenu();
        _player.playerControls.UI.Disable();
        _player.playerControls.Player.Enable();
    }
    
    public void CloseMenu(InputAction.CallbackContext context)
    {
        CloseMenu();
    }

    private void UpdateCurrentPlaceHolder()
    {
        _buildingManager.CurrentPlaceholder = gameObject;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        _player.playerControls.UI.CloseMenu.performed += CloseMenu;
    }
}

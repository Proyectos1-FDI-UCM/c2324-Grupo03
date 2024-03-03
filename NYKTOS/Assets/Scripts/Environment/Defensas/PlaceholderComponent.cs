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
    private PlayerController _player;
    private BuildingStateMachine _state;
    #endregion
    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            _player.playerControls.Player.Disable();
            _player.playerControls.UI.Enable();
            _defenseMenu.SetActive(true);
        }
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        _defenseMenu.SetActive(false);
        _player.playerControls.UI.Disable();
        _player.playerControls.Player.Enable();
    }

    public void TryBuildDefense(InputAction.CallbackContext context) 
    {
        Debug.Log("hola hola " + gameObject.name);    
    
    }


    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();

        _player.playerControls.UI.CloseMenu.performed += CloseMenu;
        _player.playerControls.UI.Submit.started += TryBuildDefense;
    }
}

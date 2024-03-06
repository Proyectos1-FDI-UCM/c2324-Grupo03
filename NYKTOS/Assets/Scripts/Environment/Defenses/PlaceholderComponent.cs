using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    #region references
    [SerializeField]
    private MenuManager _menuManager;


    private BuildingStateMachine _state;
    [SerializeField]
    private BuildingManager _buildingManager;
    #endregion

    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            _menuManager.OpenMenu(0);
            UpdateCurrentPlaceHolder();
        }
    }

    public void CloseMenu() => _menuManager.CloseMenu();

    private void UpdateCurrentPlaceHolder()
    {
        _buildingManager.CurrentPlaceholder = gameObject;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }
    void Awake()
    {
        _buildingManager.IncreasePlaceholderNumber();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    #region references
    private BuildingStateMachine _state;
    #endregion

    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt && GameManager.Instance.State == GameState.Day)
        {
            MenuManager.Instance.OpenMenu(2);
            UpdateCurrentPlaceHolder();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseMenu();

    private void UpdateCurrentPlaceHolder()
    {
        BuildingManager.Instance.CurrentPlaceholder = gameObject;
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        BuildingManager.Instance.IncreasePlaceholderNumber();
    }
}

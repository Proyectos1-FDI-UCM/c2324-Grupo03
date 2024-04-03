using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    //[Andrea] Review
    
    #region references
    [SerializeField] private GenericEmmiter _tutorialDay;
    [SerializeField] private GenericEmmiter _day;
    [SerializeField] private GenericEmmiter _tutorialNight;
    [SerializeField] private GenericEmmiter _night;

    private BuildingStateMachine _state;
    #endregion

    private bool _isDay = false;
    
    private void IsDay() => _isDay = true;
    private void IsNight() => _isDay = false;

    public void OpenMenu()
    {
        if
        (
            _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt 
            && _isDay
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

        _tutorialDay.Perform.AddListener(IsDay);
        _day.Perform.AddListener(IsDay);

        _tutorialNight.Perform.AddListener(IsNight);
        _night.Perform.AddListener(IsNight);
    }
}

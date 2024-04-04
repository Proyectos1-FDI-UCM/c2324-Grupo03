using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    //[Andrea] Review
    #region emmiters
    [SerializeField]
    private GenericEmitter _playerRevive;

    [SerializeField] private GenericEmitter _day;
    [SerializeField] private GenericEmitter _night;
    [SerializeField] private GenericEmitter _tutorialDay;
    [SerializeField] private GenericEmitter _tutorialNight;
    #endregion

    private GlobalStateIdentifier _gameState;


    private void DayState() => _gameState = GlobalStateIdentifier.Day;
    private void NightState() => _gameState = GlobalStateIdentifier.Night;
    private void TutorialDayState() => _gameState = GlobalStateIdentifier.TutorialDay;
    private void TutorialNightState() => _gameState = GlobalStateIdentifier.TutorialNight;

    public void OpenMenu()
    {
        _playerRevive.InvokePerform();
        if (_gameState == GlobalStateIdentifier.Day || _gameState == GlobalStateIdentifier.TutorialDay) 
        {
            MenuManager.Instance.OpenNexusMenu();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(gameObject);

        _day.Perform.AddListener(DayState);
        _night.Perform.AddListener(NightState);
        _tutorialDay.Perform.AddListener(TutorialDayState);
        _tutorialNight.Perform.AddListener(TutorialNightState);
    }
}

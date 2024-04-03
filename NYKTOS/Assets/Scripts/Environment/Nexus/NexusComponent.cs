using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    //[Andrea] Review

    [SerializeField]
    private GameObject _player;

    #region emmiters
    [SerializeField]
    private GenericEmmiter _playerRevive;

    [SerializeField] private GenericEmmiter _day;
    [SerializeField] private GenericEmmiter _night;
    [SerializeField] private GenericEmmiter _tutorialDay;
    [SerializeField] private GenericEmmiter _tutorialNight;
    #endregion

    private GlobalStateIdentifier _gameState;


    private void DayState() => _gameState = GlobalStateIdentifier.Day;
    private void NightState() => _gameState = GlobalStateIdentifier.Night;
    private void TutorialDayState() => _gameState = GlobalStateIdentifier.TutorialDay;
    private void TutorialNightState() => _gameState = GlobalStateIdentifier.TutorialNight;

    public void OpenMenu()
    {
        if(_gameState == GlobalStateIdentifier.Day || _gameState == GlobalStateIdentifier.TutorialDay) 
        {
            MenuManager.Instance.OpenNexusMenu();
        }
        else if (PlayerStateMachine.playerState == PlayerState.Dead) 
        {
            _playerRevive.InvokePerform();
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

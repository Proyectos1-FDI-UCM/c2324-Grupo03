using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{

    //[Marco] Not optimal
    [SerializeField]
    private GameStateMachine _stateMachine;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GenericEmmiter _playerRevive;

    public void OpenMenu()
    {
        if(_stateMachine.GetCurrentState == GlobalStateIdentifier.Day) 
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
    }
}

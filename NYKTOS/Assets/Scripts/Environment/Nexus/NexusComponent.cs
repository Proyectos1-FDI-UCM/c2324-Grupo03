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
    private PlayerDeath _playerDeath;

    public void OpenMenu()
    {
        if(_stateMachine.GetCurrentState == GlobalStateIdentifier.Day) 
        {
            MenuManager.Instance.OpenNexusMenu();
        }
        else if (PlayerStateMachine.playerState == PlayerState.Dead) 
        {
            _playerDeath.Revive();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    private void Awake()
    {
        _playerDeath = _player.GetComponent<PlayerDeath>();
    }

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(gameObject);
    }
}

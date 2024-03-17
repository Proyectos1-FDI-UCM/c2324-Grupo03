using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{

    [SerializeField]
    private GameObject _player;
    private PlayerDeath _playerDeath;

    public void OpenMenu()
    {
        if(GameManager.Instance.State == GameState.Day) 
        {
            MenuManager.Instance.OpenMenu(1);
        }
        else if (PlayerStateMachine.playerState == PlayerState.Dead) 
        {
            _playerDeath.Revive();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseMenu();

    private void Awake()
    {
        _playerDeath = _player.GetComponent<PlayerDeath>();
    }

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(gameObject);
    }
}

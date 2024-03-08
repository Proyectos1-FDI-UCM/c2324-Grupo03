using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    #region parameters
    [SerializeField]
    private int _nightLength = 180;

    [SerializeField]
    private GameObject _player;
    private PlayerDeath _playerDeath;
    #endregion

    public void OpenMenu()
    {
        if(GameManager.Instance.State == GameState.Day) MenuManager.Instance.OpenMenu(1);
        else if(PlayerStateMachine.playerState == PlayerState.Dead) _playerDeath.Revive();
    }

    public void CloseMenu() => MenuManager.Instance.CloseMenu();

    public void StartNight()
    {
        MenuManager.Instance.CloseMenu();
        GameManager.Instance.UpdateGameState(GameState.Night);
        Invoke(nameof(EndNight), _nightLength);
    }
    public void EndNight()
    {
        GameManager.Instance.UpdateGameState(GameState.Day);
        _playerDeath.Revive();
    }

    private void Start()
    {
        BuildingManager.Instance.AddBuilding(this.gameObject);
        
        _playerDeath = _player.GetComponent<PlayerDeath>();
    }
}

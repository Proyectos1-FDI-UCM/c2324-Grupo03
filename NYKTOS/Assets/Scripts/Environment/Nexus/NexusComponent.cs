using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    #region parameters
    [SerializeField]
    private int _nightLength = 180;

    [SerializeField]
    private PlayerDeath _player;
    #endregion

    public void OpenMenu()
    {
        if(GameManager.Instance.State == GameState.Day) MenuManager.Instance.OpenMenu(1);
        else _player.Revive();
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
    }
}

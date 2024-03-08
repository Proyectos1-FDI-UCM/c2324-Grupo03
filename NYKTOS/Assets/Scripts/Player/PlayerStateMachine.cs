using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Idle, Attacking, OnKnockback, OnMenu, Dead
}

public class PlayerStateMachine : MonoBehaviour
{
    #region references
    private PlayerController _playerController;
    #endregion

    #region properties
    static private PlayerState _playerState = PlayerState.Idle;
    static public PlayerState playerState
    {
        get { return _playerState; }
    }
    #endregion

    #region simpleStateMachine

    public void SetState(PlayerState state)
    {
        _playerState = state;
    }

    public void SetIdleState()
    {
        _playerState = PlayerState.Idle;
        _playerController.CallMove(_playerController._inputMovement);
    }
    #endregion

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
}

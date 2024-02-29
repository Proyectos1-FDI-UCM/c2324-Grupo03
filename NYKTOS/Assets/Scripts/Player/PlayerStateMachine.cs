using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Idle, Attacking, OnKnockback
}

public class PlayerStateMachine : MonoBehaviour
{
    #region references
    private PlayerController _playerController;
    #endregion

    #region properties
    public PlayerState playerState
    {
        get {return _playerState; }
    }
    private PlayerState _playerState = PlayerState.Idle;
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

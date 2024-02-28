using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateMachine : MonoBehaviour
{
    #region references
    private PlayerController _playerController;
    #endregion

    #region properties
    /// <summary>
    /// Estado en el que se encuentra el jugador. 0=Idle/Running. 1=Attacking. 2=OnKnockback.
    /// </summary>
    private int _playerState = 0;
    public int playerState
    {
        get {return _playerState; }
    }
    #endregion

    #region simpleStateMachine
    public void SetState(int num)
    {
        if (num >= 0 && num <= 2) //rango de el numero de estados
        {
            _playerState = num;
        }
    }

    public void SetIdleState()
    {
        _playerState = 0;
        _playerController.CallMove(_playerController._inputMovement);
    }
    #endregion

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
}

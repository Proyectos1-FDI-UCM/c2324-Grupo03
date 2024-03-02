using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    #region references
    private PlayerStateMachine _playerState;
    #endregion


    [SerializeField]
    private SpriteRenderer _deathskin;

    [SerializeField]
    private SpriteRenderer _aliveskin;
    


    public void Death()
    {
       _aliveskin.enabled = false;
       _deathskin.enabled = true;
       _playerState.SetState(PlayerState.Dead);
    }

    private void Start()
    {
        _playerState = GetComponent<PlayerStateMachine>();
    }
}

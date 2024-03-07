using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references
    private PlayerStateMachine _playerState;
    private HealthComponent _health;
    #endregion


    [SerializeField]
    private SpriteRenderer _deathskin;

    [SerializeField]
    private SpriteRenderer _aliveskin;

    public bool alive = true;

    public void Death()
    {
       _aliveskin.enabled = false;
       _deathskin.enabled = true;
       alive = false;
       _playerState.SetState(PlayerState.Dead);
    }

    public void Revive()
    {
        _aliveskin.enabled = true;
        _deathskin.enabled = false;
        alive = true;
        _playerState.SetState(PlayerState.Idle);
        _health.MaxHealth();
    }

    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
    }
}

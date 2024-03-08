using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references
    private PlayerStateMachine _playerState;
    private HealthComponent _health;
    [SerializeField]
    private UIManager _UImanager;
    
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
        _UImanager.DeathScreenOn();
    }

    public void Revive()
    {
        _aliveskin.enabled = true;
        _deathskin.enabled = false;
        _playerState.SetState(PlayerState.Idle);
        _health.MaxHealth();
        _UImanager.DeathScreenOff();
    }

    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
    }
}

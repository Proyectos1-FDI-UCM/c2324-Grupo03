using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references

    [SerializeField]
    private GenericEmmiter _playerReviveEmmiter;

    private PlayerStateMachine _playerState;
    private HealthComponent _health;
    
    [SerializeField]
    private SpriteRenderer _deathskin;

    [SerializeField]
    private SpriteRenderer _aliveskin;

    #endregion

    public void Death()
    {
        //Debug.Log("He morio!");
        _aliveskin.enabled = false;
        _deathskin.enabled = true;
        _playerState.SetState(PlayerState.Dead);
        UIManager.Instance.DeathScreenOn();
    }

    public void Revive()
    {
        Debug.Log("He revivio!");
        _aliveskin.enabled = true;
        _deathskin.enabled = false;
        _health.MaxHealth();
        _playerState.SetState(PlayerState.Idle);

        // [Andrea] Not optimal?
        UIManager.Instance.DeathScreenOff();
    }
    
    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
     
        _playerReviveEmmiter.Perform.AddListener(Revive);

    }
}

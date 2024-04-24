using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references

    [SerializeField]
    private VoidEmitter _playerReviveEmitter;
    [SerializeField]
    private VoidEmitter _playerDeathEmitter;

    private PlayerAnimations _playerAnimations;

    private PlayerStateMachine _playerState;
    private HealthComponent _health;
    
    #endregion

    public void Death()
    {
        _playerState.SetState(PlayerState.Dead);
        UIManager.Instance.DeathScreenOn();

        _playerDeathEmitter.InvokePerform();
    }

    public void Revive()
    {
        _playerAnimations.StartRevive();
        _health.MaxHealth();
        _playerState.SetState(PlayerState.Idle);

        // [Andrea] Not optimal
        UIManager.Instance.DeathScreenOff();
    }
    
    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
        _playerReviveEmitter.Perform.AddListener(Revive);
        _playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }

    void OnDestroy()
    {
        _playerReviveEmitter.Perform.RemoveListener(Revive);
    }
}

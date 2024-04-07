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

    private PlayerStateMachine _playerState;
    private HealthComponent _health;
    
    [SerializeField]
    private SpriteLibraryAsset _deathskin;

    [SerializeField]
    private SpriteLibraryAsset _aliveskin;

    SpriteLibrary spriteLibrary;

    #endregion

    public void Death()
    {
        spriteLibrary.spriteLibraryAsset = _deathskin;
        _playerState.SetState(PlayerState.Dead);
        UIManager.Instance.DeathScreenOn();

        _playerDeathEmitter.InvokePerform();
    }

    public void Revive()
    {
        spriteLibrary.spriteLibraryAsset = _aliveskin;
        _health.MaxHealth();
        _playerState.SetState(PlayerState.Idle);

        // [Andrea] Not optimal
        UIManager.Instance.DeathScreenOff();
    }
    
    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
        spriteLibrary = GetComponentInChildren<SpriteLibrary>();
        _playerReviveEmitter.Perform.AddListener(Revive);
    }
}

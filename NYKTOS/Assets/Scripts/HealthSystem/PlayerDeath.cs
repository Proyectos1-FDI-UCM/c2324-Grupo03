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
        UIManager.Instance.DeathScreenOff();
    }

    private void DayRevive(GameState state)
    {
        if(state == GameState.Day)
        {
            _aliveskin.enabled = true;
            _deathskin.enabled = false;
            _health.MaxHealth();
            _playerState.SetState(PlayerState.Idle);
            UIManager.Instance.DeathScreenOff();
        }
    }

    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
     
        GameManager.Instance.GameStateChanged.AddListener(DayRevive);
        /* 
        
        Marco:
        
        DayRevive se ejecuta cada vez que haya un cambio de estado
        en el game manager

        */     
    }
}

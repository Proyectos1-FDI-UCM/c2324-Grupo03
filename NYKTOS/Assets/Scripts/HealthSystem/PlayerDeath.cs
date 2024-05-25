using UnityEngine;

/// <summary>
/// Script que controla la muerte del jugador
/// </summary>

public class PlayerDeath : MonoBehaviour, IDeath
{
    #region references

    [SerializeField]
    private VoidEmitter _playerReviveEmitter;
    [SerializeField]
    private VoidEmitter _playerDeathEmitter;

    private PlayerAnimations _playerAnimations; // para las animaciones de muerte y de revivir

    private PlayerStateMachine _playerState;
    private HealthComponent _health;

    #endregion

    void Start()
    {
        _health = GetComponent<HealthComponent>();
        _playerState = GetComponent<PlayerStateMachine>();
        _playerReviveEmitter.Perform.AddListener(Revive);
        _playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }

    public void Death() //Cambia es estado del jugador a muerto y activa la pantalla con las indicaciones para revivir
    {
        _playerState.SetState(PlayerState.Dead);
        UIManager.Instance.DeathScreenOn();

        _playerDeathEmitter.InvokePerform();
    }

    public void Revive() //Oculta la pantalla de muerte y cambia el estado del jugador a vivo con toda la vida recuperada.
    {
        _playerAnimations.StartRevive();
        _health.MaxHealth();
        _playerState.SetState(PlayerState.Idle);

        UIManager.Instance.DeathScreenOff();
    }
    
    

    void OnDestroy()
    {
        _playerReviveEmitter.Perform.RemoveListener(Revive);
    }
}

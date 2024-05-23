using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Script encargado de controlar al jugador
/// Define todas las acciones realizables por el jugador (de movimieno, ataque e interacción). 
/// Estos métodos se llaman desde el InputManager según el input del usuario
/// También incluye otros métodos que alteran el control, como el desplazamiento por knockback al recibir daño
/// </summary>
public class PlayerController : MonoBehaviour, IKnockback
{
    
    #region references
    private static Transform _myTransform;
    public static Transform playerTransform { get { return _myTransform; } }

    private BlinkComponent _blinkComponent;
    private RBMovement _playerMovement;
    private LookDirection _lookDirection;
    private WeaponHandler _weaponHandler;
    private PlayerDeath _playerDeath;

    [SerializeField]
    private Cooldown _BlinkCooldown;
    [SerializeField]
    private float _PrimaryUseSlowingCooldown;
    [SerializeField]
    private float _SecondaryUseSlowingCooldown;

    private PlayerStateMachine _playerState;
    #endregion

    #region properties
    public Vector2 _inputMovement
    {
        get { return _privateMovement; }
    }
    private Vector2 _privateMovement = Vector2.zero;
    
    private float _interactionRange;
    #endregion

    #region playerEmitters
    [Header("Player Emitters")]
    [SerializeField] VoidEmitter _playerMoved;
    [SerializeField] VoidEmitter _playerBlinked;
    [SerializeField] VoidEmitter _playerInteracted;
    [SerializeField] VoidEmitter _playerPrimaryAttacked;
    [SerializeField] VoidEmitter _playerSecondaryAttacked;
    #endregion

    #region actions

    #region movement
    public void Blink()
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && !_BlinkCooldown.IsCooling())
        {
            _playerBlinked?.InvokePerform();
            _blinkComponent.Blink();
            _BlinkCooldown.StartCooldown();
        }
    }

    public void Move(Vector2 direction)
    {
        _privateMovement = direction;

        if (PlayerStateMachine.playerState == PlayerState.Idle || PlayerStateMachine.playerState == PlayerState.Dead)
        {
            _playerMoved?.InvokePerform();
            CallMove(direction);
        }

    }

    public void CallMove(Vector2 vector)
    {
        _playerMovement.xAxisMovement(vector.x);
        _playerMovement.yAxisMovement(vector.y);
    }

    public void CallKnockback(Vector2 pushPosition) //cambia los estados del jugador y llama a callknockback
    {
        if(PlayerStateMachine.playerState == PlayerState.Idle ||  PlayerStateMachine.playerState == PlayerState.Attacking)
        {
            _playerState.SetState(PlayerState.OnKnockback);
            _playerMovement.Knockback(pushPosition);

            if (PlayerStateMachine.playerState == PlayerState.Attacking) //si el jugador esta atacando y recibe un golpe, se realiza knockback y se cancela el retorno a idle (es llamado mas adelante)
            {
                _playerState.CancelInvoke(nameof(_playerState.SetIdleState));
            }
            _playerState.Invoke(nameof(_playerState.SetIdleState), _playerMovement.knockBackTime);
            StartCoroutine(CallMoveOnNextFramePlusSeconds(GetComponent<SlowDebuff>().slowTime));

        }
        
    }

    private IEnumerator CallMoveOnNextFramePlusSeconds(float secs)
    {
        yield return new WaitForNextFrameUnit();
        yield return new WaitForSeconds(secs);
        CallMove(_privateMovement);
    }

    public void Look(Vector2 direction, bool isMouse)
    {   
        if (isMouse) direction = (direction - new Vector2(_myTransform.position.x, _myTransform.position.y)).normalized;
        //Debug.Log(direction);
        _lookDirection.SetLookDirection(direction);
    }
    #endregion

    #region combat
    public void PrimaryAttack()
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _playerPrimaryAttacked.InvokePerform();
            _weaponHandler.CallPrimaryUse(_lookDirection.lookDirection);


            _playerMovement.AddSpeed(-_playerMovement.movementSpeed /1.5f, _PrimaryUseSlowingCooldown);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _PrimaryUseSlowingCooldown);
        }
    }

    public void SecondaryAttack()
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _playerSecondaryAttacked.InvokePerform();
            _weaponHandler.CallSecondaryUse( _lookDirection.lookDirection);

            _playerMovement.AddSpeed(-_playerMovement.movementSpeed / 1.5f, _SecondaryUseSlowingCooldown);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _SecondaryUseSlowingCooldown);
        }
    }

    #endregion

    #region other actions
    public void Interact()
    {
        int maxColliders = 10;
        Collider2D[] hitColliders = new Collider2D[maxColliders];
        int numColliders = Physics2D.OverlapCircleNonAlloc(_myTransform.position, _interactionRange, hitColliders, 1 << 7);
        
        for (int i = 0; i < numColliders && (PlayerStateMachine.playerState == PlayerState.Idle  || PlayerStateMachine.playerState == PlayerState.Dead); i++)
        {
            if (hitColliders[i].gameObject.TryGetComponent(out IInteractable interactableObject))
            {
                _playerInteracted?.InvokePerform();
                interactableObject.Interact();
                // Desde el objeto, lanzar un evento para cambiar el estado del player a OnMenu o algo as�
            }
        }
    }
    #endregion
    #endregion

    void Start()
    {
        InputManager.Instance.RegisterPlayer(gameObject);

        _myTransform = transform;
        _playerMovement = GetComponent<RBMovement>();
        _blinkComponent = GetComponent<BlinkComponent>();
        _lookDirection = GetComponent<LookDirection>();
        _weaponHandler = GetComponent<WeaponHandler>();
        
        _playerState = GetComponent<PlayerStateMachine>();
        _playerDeath = GetComponent<PlayerDeath>();

        _interactionRange = GetComponentInChildren<CircleCollider2D>().radius;


    }
}

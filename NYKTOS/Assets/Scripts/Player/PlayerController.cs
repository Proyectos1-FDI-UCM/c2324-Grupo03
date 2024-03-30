using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;


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
    private Cooldown _PrimaryAttackCooldown;
    [SerializeField]
    private Cooldown _SecondaryAttackCooldown;

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

    #region actions

    #region movement
    public void Blink()
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && !_BlinkCooldown.IsCooling())
        {
            _blinkComponent.Blink();
            _BlinkCooldown.StartCooldown();
        }
    }

    public void Move(Vector2 direction)
    {
        _privateMovement = direction;

        if (PlayerStateMachine.playerState == PlayerState.Idle || PlayerStateMachine.playerState == PlayerState.Dead)
        {
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
        _lookDirection.SetLookDirection(direction);
    }
    #endregion

    #region combat
    public void PrimaryAttack()
    {
        if(PlayerStateMachine.playerState == PlayerState.Idle && !_PrimaryAttackCooldown.IsCooling()&& PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _weaponHandler.CallPrimaryUse(_lookDirection.lookDirection);
            _PrimaryAttackCooldown.StartCooldown();


            _playerMovement.AddSpeed(-_playerMovement.movementSpeed /1.5f, _PrimaryAttackCooldown.cooldownTime);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _PrimaryAttackCooldown.cooldownTime);
        }
    }

    public void SecondaryAttack()
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && !_SecondaryAttackCooldown.IsCooling() && PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _weaponHandler.CallSecondaryUse( _lookDirection.lookDirection);
            _SecondaryAttackCooldown.StartCooldown();

            _playerMovement.AddSpeed(-_playerMovement.movementSpeed / 1.5f, _SecondaryAttackCooldown.cooldownTime);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _SecondaryAttackCooldown.cooldownTime);
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
                interactableObject.Interact();
                // Desde el objeto, cambiar el estado del player a OnMenu o algo así
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

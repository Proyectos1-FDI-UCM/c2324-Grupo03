using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IKnockback
{
    
    #region references
    // Referencia a la clase creada a partir del ActionMap
    private PlayerControls _playerControls;

    private PlayerInput _playerInput;

    private string previousScheme;
    private const string gamepadScheme = "Gamepad";
    private const string mouseScheme = "Mouse";

    public PlayerControls playerControls { 
        get { return _playerControls; } 
        set { _playerControls = value; }
    }

    private static Transform _myTransform;
    public static Transform playerTransform { get { return _myTransform; } }
    private BlinkComponent _blinkComponent;
    private RBMovement _playerMovement;
    private LookDirection _lookDirection;
    private WeaponHandler _weaponHandler;
    private PlayerDeath _playerDeath;

    private Camera mainCamera;

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

    #region enableInput
    // activar/desactivar el input del jugador
    private void OnEnable()
    {
        _playerControls.Enable();

        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Move.canceled += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed += SecondaryAttack;
        _playerControls.Player.Interact.performed += Interact;
        _playerControls.Player.Pause.performed += PauseGame;

        _playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDisable()
    {
        _playerControls.Disable();

        _playerControls.Player.Move.performed -= Move;
        _playerControls.Player.Move.canceled -= Move;
        _playerControls.Player.Blink.performed -= Blink;
        _playerControls.Player.Look.performed -= Look;
        _playerControls.Player.PrimaryAttack.performed -= PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed -= SecondaryAttack;
        _playerControls.Player.Interact.performed -= Interact;
        _playerControls.Player.Pause.performed -= PauseGame;

        _playerInput.onControlsChanged -= OnControlsChanged;
    }
    #endregion

    #region actions

    #region movement
    public void Blink(InputAction.CallbackContext context)
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && !_BlinkCooldown.IsCooling())
        {
            _blinkComponent.Blink();
            _BlinkCooldown.StartCooldown();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _privateMovement = input;

        if (PlayerStateMachine.playerState == PlayerState.Idle || PlayerStateMachine.playerState == PlayerState.Dead)
        {
            CallMove(input);
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

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 input;
        if(previousScheme == gamepadScheme)
        {
            input = context.ReadValue<Vector2>();
        }
        else 
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //apaño chungo, cambiar luego
            Vector2 playerPosition = _myTransform.position;
            input = (mousePosition- playerPosition).normalized;
        }
       
        _lookDirection.SetLookDirection(input);
    }
    #endregion

    #region combat
    public void PrimaryAttack(InputAction.CallbackContext context)
    {
        if(PlayerStateMachine.playerState == PlayerState.Idle && !_PrimaryAttackCooldown.IsCooling()&& PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _weaponHandler.CallPrimaryUse(0, _lookDirection.lookDirection);
            _PrimaryAttackCooldown.StartCooldown();

            print(_playerMovement.movementSpeed / 2);

            _playerMovement.AddSpeed(-_playerMovement.movementSpeed /1.5f, _PrimaryAttackCooldown.cooldownTime);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _PrimaryAttackCooldown.cooldownTime);
        }
    }

    public void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (PlayerStateMachine.playerState == PlayerState.Idle && !_SecondaryAttackCooldown.IsCooling() && PlayerStateMachine.playerState != PlayerState.Dead)
        {
            _weaponHandler.CallSecondaryUse(0, _lookDirection.lookDirection);
            _SecondaryAttackCooldown.StartCooldown();

            _playerMovement.AddSpeed(-_playerMovement.movementSpeed / 1.5f, _SecondaryAttackCooldown.cooldownTime);
            CallMove(_privateMovement);

            _playerState.SetState(PlayerState.Attacking);
            _playerState.Invoke(nameof(_playerState.SetIdleState), _SecondaryAttackCooldown.cooldownTime);
        }
    }

    #endregion

    #region other actions
    public void Interact(InputAction.CallbackContext context)
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

    public void PauseGame(InputAction.CallbackContext context)
    {
        MenuManager.Instance.OpenPauseMenu();
        GameManager.Instance.Pause();
    }

    #endregion

    #region controls
    private void OnControlsChanged(PlayerInput input)
    {
        Debug.Log(_playerInput.currentControlScheme);
        if(_playerInput.currentControlScheme == gamepadScheme && previousScheme != gamepadScheme)
        {
            previousScheme = gamepadScheme;
            Cursor.visible = false;
        }
        else if (_playerInput.currentControlScheme != gamepadScheme)
        {
            previousScheme = _playerInput.currentControlScheme;
            Cursor.visible = true;
        }
    }
    #endregion

    #endregion

    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {      
        _myTransform = transform;
        _playerMovement = GetComponent<RBMovement>();
        _blinkComponent = GetComponent<BlinkComponent>();
        _lookDirection = GetComponent<LookDirection>();
        _weaponHandler = GetComponent<WeaponHandler>();
        mainCamera= Camera.main;
        _playerState = GetComponent<PlayerStateMachine>();
        _playerDeath = GetComponent<PlayerDeath>();

        _interactionRange = GetComponentInChildren<CircleCollider2D>().radius;

        previousScheme = _playerInput.currentControlScheme;
    }
}

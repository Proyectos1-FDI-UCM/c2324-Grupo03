using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.Timeline.TimelinePlaybackControls;


// Código de Andrea <3

public class PlayerController : MonoBehaviour, IKnockback
{
    
    #region references
    // Referencia a la clase creada a partir del ActionMap
    private PlayerControls _playerControls;
    private Transform _myTransform;
    private BlinkComponent _blinkComponent;
    private RBMovement _playerMovement;
    private LookDirection _lookDirection;
    private WeaponHandler _weaponHandler;

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

    [SerializeField]
    private float _interactionRange = 5;
    #endregion

    #region enableInput
    // activar/desactivar el input del jugador
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    #endregion

    #region movement
    public void Blink(InputAction.CallbackContext context)
    {
        if (context.performed && !_BlinkCooldown.IsCooling())
        {
            _blinkComponent.Blink();
            _BlinkCooldown.StartCooldown();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _privateMovement = input;

        if (_playerState.playerState == 0)
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
        if(_playerState.playerState == 0 ||  _playerState.playerState == 1)
        {
            _playerState.SetState(2);
            _playerMovement.Knockback(pushPosition);

            if (_playerState.playerState == 1) //si el jugador esta atacando y recibe un golpe, se realiza knockback y se cancela el retorno a idle (es llamado mas adelante)
            {
                _playerState.CancelInvoke("SetIdleState");
            }
            _playerState.Invoke("SetIdleState", _playerMovement.knockBackTime);
        }
        
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 input;
        if(context.control.device is Mouse)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //apaño chungo, cambiar luego
            Vector2 playerPosition = _playerMovement.transform.position;
            input = (mousePosition- playerPosition).normalized;
        }
        else
        {
            input = context.ReadValue<Vector2>();
        }
       
        _lookDirection.SetLookDirection(input);
    }
    #endregion

    #region combat
    public void PrimaryAttack(InputAction.CallbackContext context)
    {
        if(context.performed && !_PrimaryAttackCooldown.IsCooling() && _playerState.playerState == 0)
        {
            _weaponHandler.CallPrimaryUse(0, _lookDirection.lookDirection);
            _PrimaryAttackCooldown.StartCooldown();

            CallMove(Vector2.zero);
            _playerState.SetState(1);

            
            _playerState.Invoke("SetIdleState", _PrimaryAttackCooldown.cooldownTime);

        }
    }

    public void SecondaryAttack(InputAction.CallbackContext context)
    {
        // Realizar el ataque especial
    }

    #endregion

    #region interactions
    public void Interact(InputAction.CallbackContext context)
    {
        if(Physics.Raycast(_myTransform.position, _lookDirection.lookDirection, out RaycastHit hit, _interactionRange))
        {
            if(hit.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
            {
                interactableObject.Interact();
            }
        }
    }

    public void QuitInteraction() { }

    #endregion

    void Awake()
    {
        _playerControls = new PlayerControls();
        
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


        // No estoy segura de si esto va aquí o en el OnEnable()
        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Move.canceled += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed += SecondaryAttack;
        _playerControls.Player.Interact.performed += Interact;
    }
}

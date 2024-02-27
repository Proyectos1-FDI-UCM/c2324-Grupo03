using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;


// Código de Andrea <3

public class PlayerController : MonoBehaviour
{
    
    #region references
    // Referencia a la clase creada a partir del ActionMap
    private PlayerControls _playerControls;
    
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

   

    // private WeaponHandler _weaponHandler;
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
        
        _playerMovement.xAxisMovement(input.x);
        _playerMovement.yAxisMovement(input.y);
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

    public void PrimaryAttack(InputAction.CallbackContext context)
    {
        if(context.performed && !_PrimaryAttackCooldown.IsCooling())
        {
            _weaponHandler.CallPrimaryUse(0, _lookDirection.lookDirection);
            _PrimaryAttackCooldown.StartCooldown();
        }
    }

    public void SecondaryAttack(InputAction.CallbackContext context)
    {
        // Realizar el ataque especial
    }

    public void EnvInteraction(InputAction.CallbackContext context)
    {
        // Interactuar con el entorno
    }


    void Awake()
    {
        _playerControls = new PlayerControls();
        
    }

    void Start()
    {
        _playerMovement = GetComponent<RBMovement>();
        _blinkComponent = GetComponent<BlinkComponent>();
        _lookDirection = GetComponent<LookDirection>();
        _weaponHandler = GetComponent<WeaponHandler>();
        mainCamera= Camera.main;

        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Move.canceled += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed += SecondaryAttack;
        _playerControls.Player.Environment.performed += EnvInteraction;
    }
}

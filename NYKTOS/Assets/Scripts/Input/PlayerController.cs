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
        if (context.started && !_BlinkCooldown.IsCooling())
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

        if(context.canceled)
        {
            _playerMovement.xAxisMovement(0);
            _playerMovement.yAxisMovement(0);
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _lookDirection.SetLookDirection(input);
    }

    public void PrimaryAttack(InputAction.CallbackContext context)
    {
        if(context.started && !_PrimaryAttackCooldown.IsCooling())
        {
            _weaponHandler.CallPrimaryUse(0, _lookDirection.lookDirection);
            _PrimaryAttackCooldown.StartCooldown();
        }
    }

    public void SecondaryAttack(InputAction.CallbackContext context)
    {
        // Realizar el ataque especial
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

        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        
    }
}

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
        if (context.started)
        {
            _blinkComponent.Blink();
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
           // Falta script de mirar en la dirección 
    }

    public void PrimaryAttack(InputAction.CallbackContext context)
    {
        // Realizar el ataque simple
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

        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Blink.performed += Blink;
    }
}

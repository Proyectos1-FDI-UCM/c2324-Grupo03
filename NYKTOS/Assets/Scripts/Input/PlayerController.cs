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

    private RBMovement _playerMovement;
    // private WeaponHandler _weaponHandler;
    #endregion

    public void Blink(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMovement.Blink();
        }
        //_playerMovement.Blink();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _playerMovement.xAxisMovement(input.x);
        _playerMovement.yAxisMovement(input.y);
    }

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

    void Awake()
    {
        _playerControls = new PlayerControls();

    }

    void Start()
    {
        _playerMovement = GetComponent<RBMovement>();

        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Blink.performed += Blink;
    }

    
    void Update()
    {
        
    }
}

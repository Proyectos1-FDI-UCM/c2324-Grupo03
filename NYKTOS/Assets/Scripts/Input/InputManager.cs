using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    #region references
    private PlayerController _player;
    private PlayerInput _playerInput;

    private Camera mainCamera;


    // Referencia a la clase creada a partir del ActionMap
    private PlayerControls _playerControls;
    public PlayerControls playerControls
    {
        get { return _playerControls; }
    }

    private const string gamepadScheme = "Gamepad";
    private const string mouseScheme = "Keyboard&Mouse";
    private string _currentScheme;
    #endregion

    //Se llama desde el player controller
    public void RegisterPlayer(GameObject player)
    {
        _player = player.GetComponent<PlayerController>();
        _playerInput = player.GetComponent<PlayerInput>();

        _playerInput.onControlsChanged += OnControlsChanged;
    }

    #region player actions
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _player.Move(input);
    }

    public void Blink(InputAction.CallbackContext context) => _player.Blink();

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _player.Look(input);
        //Vector2 input;
        //if (_currentScheme == gamepadScheme)
        //{
        //    input = context.ReadValue<Vector2>();
        //}
        //else
        //{
        //    Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //apaño chungo, cambiar luego
        //    Vector2 playerPosition = _myTransform.position;
        //    input = (mousePosition - playerPosition).normalized;
        //}
    }

    public void PrimaryAttack(InputAction.CallbackContext context) => _player.PrimaryAttack();

    public void SecondaryAttack(InputAction.CallbackContext context) => _player.SecondaryAttack();

    public void Interact(InputAction.CallbackContext context) => _player.Interact();

    #endregion

    public void PauseGame(InputAction.CallbackContext context)
    {
        MenuManager.Instance.OpenPauseMenu();
        GameManager.Instance.Pause();
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        MenuManager.Instance.CloseMenu();
    }

    #region controls
    private void OnControlsChanged(PlayerInput input)
    {
        Debug.Log(_playerInput.currentControlScheme);
        if (_playerInput.currentControlScheme == gamepadScheme && _currentScheme != gamepadScheme)
        {
            _currentScheme = gamepadScheme;
            Cursor.visible = false;
        }
        else if (_playerInput.currentControlScheme != gamepadScheme)
        {
            _currentScheme = _playerInput.currentControlScheme;
            Cursor.visible = true;
        }
    }

    public void SwitchToUIControls()
    {
        playerControls.Player.Disable();
        // Lanzar evento de cambio de estado
        //_playerStateMachine.SetState(PlayerState.OnMenu);
        playerControls.UI.Enable();
        _player.CallMove(Vector2.zero);
    }

    public void SwitchToPlayerControls()
    {
        // Lanzar evento cambio de estado
        //_playerStateMachine.SetState(PlayerState.Idle);
        playerControls.UI.Disable();
        playerControls.Player.Enable();
    }
    #endregion

    #region enable/disable
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

        _playerControls.UI.CloseMenu.performed += CloseMenu;

        
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
    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        _playerControls = new PlayerControls();
    }

    void Start()
    {
        _currentScheme = _playerInput.currentControlScheme;
        mainCamera = Camera.main;
    }


}

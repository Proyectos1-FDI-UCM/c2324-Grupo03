using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        _currentScheme = _playerInput.currentControlScheme;

        _playerInput.onControlsChanged += OnControlsChanged;
    }


    #region player actions
    private void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _player.Move(input);
    }

    private void Blink(InputAction.CallbackContext context) => _player.Blink();

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        bool isMouse = false;
        if (_currentScheme == mouseScheme)
        {
            isMouse = true;
            input = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        //Debug.Log(_currentScheme);
        //Debug.Log(_playerInput.currentControlScheme);
        _player.Look(input, isMouse);

       
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

    private void PrimaryAttack(InputAction.CallbackContext context) => _player.PrimaryAttack();

    private void SecondaryAttack(InputAction.CallbackContext context) => _player.SecondaryAttack();

    private void Interact(InputAction.CallbackContext context) => _player.Interact();

    #endregion

    private void PauseGame(InputAction.CallbackContext context)
    {
        MenuManager.Instance.OpenPauseMenu();
        GameManager.Instance.Pause();
    }

    private void CloseMenu(InputAction.CallbackContext context)
    {
        MenuManager.Instance.CloseMenu();
    }

    #region controls
    private void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("cambio de controles");
        Debug.Log(_playerInput.currentControlScheme);

        if (_playerInput.currentControlScheme == gamepadScheme && _currentScheme != gamepadScheme)
        {
            //Debug.Log(_playerInput.currentControlScheme);
            _currentScheme = gamepadScheme;
            Cursor.visible = false;
        }
        else if (_playerInput.currentControlScheme == mouseScheme && _currentScheme != mouseScheme)
        {
            //Debug.Log(_playerInput.currentControlScheme);
            _currentScheme = mouseScheme;
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
        mainCamera = Camera.main;
    }


}

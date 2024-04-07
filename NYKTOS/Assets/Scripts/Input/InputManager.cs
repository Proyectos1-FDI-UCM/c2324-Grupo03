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

    [SerializeField]
    private VoidEmitter _stateChanged;

    [SerializeField]
    private BoolEmitter _pauseEmitter;
    [SerializeField]
    private VoidEmitter _pauseMenuEmitter;
    [SerializeField]
    private VoidEmitter _closeMenusEmitter;

    private PlayerController _player;
    private PlayerInput _playerInput;
    //private PlayerStateMachine _playerState;

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
        //_playerState = player.GetComponent<PlayerStateMachine>();

        ControlsStart();  
    }

    private void ControlsStart()
    {
        _currentScheme = _playerInput.currentControlScheme;

        OnControlsChanged(_playerInput);
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
        Vector2 input;
        bool isMouse = false;
        if (_currentScheme == gamepadScheme)
        {
            input = context.ReadValue<Vector2>();
        }
        else
        {
            isMouse = true;
            input = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        _player.Look(input, isMouse);
    }

    private void PrimaryAttack(InputAction.CallbackContext context) => _player.PrimaryAttack();

    private void SecondaryAttack(InputAction.CallbackContext context) => _player.SecondaryAttack();

    private void Interact(InputAction.CallbackContext context) => _player.Interact();

    //private void Interact(InputAction.CallbackContext context)
    //{
    //    _player.Interact();
    //    Debug.Log("Interactuando");
    //}
    #endregion

    private void PauseGame(InputAction.CallbackContext context)
    {
        _pauseEmitter.InvokePerform(true);
        _pauseMenuEmitter.InvokePerform();
    }

    private void CloseMenu(InputAction.CallbackContext context)
    {
        //[Marco] Deprecated
        //MenuManager.Instance.CloseMenu();

        //esto es lo guay esto se queda
        _pauseEmitter.InvokePerform(false);
        _closeMenusEmitter.InvokePerform();
    }

    #region controls
    private void OnControlsChanged(PlayerInput input)
    {
        if (_playerInput.currentControlScheme == gamepadScheme && _currentScheme != gamepadScheme)
        {
            _currentScheme = gamepadScheme;
            Cursor.visible = false;
        }
        else if (_playerInput.currentControlScheme == mouseScheme && _currentScheme != mouseScheme)
        {
            _currentScheme = mouseScheme;
            Cursor.visible = true;
        }
    }

    public void SwitchToUIControls()
    {
        playerControls.Player.Disable();
        // Lanzar evento de cambio de estado
        //_playerState.SetState(PlayerState.OnMenu);
        playerControls.UI.Enable();
        _player.CallMove(Vector2.zero);
    }

    public void SwitchToPlayerControls()
    {
        // Lanzar evento cambio de estado
        //_playerState.SetState(PlayerState.Idle);
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

        if (_playerControls != null)
        {
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
        _stateChanged.Perform.AddListener(Start);
    }

    void OnDestroy()
    {
        _stateChanged.Perform.RemoveListener(Start);
    }
}   

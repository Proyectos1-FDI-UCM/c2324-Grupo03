using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gestiona todo el input del jugador
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    #region references
    [Header("Emitters")]
    [SerializeField]
    private VoidEmitter _stateChanged;
    [SerializeField]
    private BoolEmitter _enablePlayerInput;
    [SerializeField]
    private BoolEmitter _enableUIInput;
    [SerializeField]
    private BoolEmitter _enableDialogueInput;

    [SerializeField]
    private BoolEmitter _pauseEmitter;
    [SerializeField]
    private VoidEmitter _pauseMenuEmitter;
    [SerializeField]
    private VoidEmitter _closeMenusEmitter;

    [SerializeField]
    private VoidEmitter _mapEmitter;

    [SerializeField]
    private VoidEmitter _resumeDialogueEmitter;


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

    private const string playerActionMap = "Player";
    private const string UIactionMap = "UI";
    private string _currentActionMap;

    #endregion

    private void OnStateLoad()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Se llama desde el script player controller para registrar al jugador
    /// Inicializa los controles
    /// </summary>
    /// <param name="player">Jugador</param>
    public void RegisterPlayer(GameObject player)
    {
        _player = player.GetComponent<PlayerController>();
        _playerInput = player.GetComponent<PlayerInput>();

        ControlsStart();
    }

    /// <summary>
    /// Registra el esquema de control del jugador
    /// Se suscribe al evento de cambio de controles
    /// </summary>
    private void ControlsStart()
    {
        _currentScheme = _playerInput.currentControlScheme;

        OnControlsChanged(_playerInput);
        _playerInput.onControlsChanged += OnControlsChanged;

        SwitchToPlayerControls();
    }

    #region player actions
    /// <summary>
    /// En esta región están los métodos que recogen el input del mapa de acciones del jugador
    /// </summary>
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

    private void OpenCloseMap(InputAction.CallbackContext context)
    {
        _mapEmitter.InvokePerform();
    }
    #endregion



    private void NextDialogue(InputAction.CallbackContext context)
    {
        _resumeDialogueEmitter.InvokePerform();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        _pauseEmitter.InvokePerform(true);
        _pauseMenuEmitter.InvokePerform();
    }

    private void CloseMenu(InputAction.CallbackContext context)
    {
        _pauseEmitter.InvokePerform(false);
        _closeMenusEmitter.InvokePerform();
    }

    #region controls
    /// <summary>
    /// Guarda el esquema de control actual, y en función de su valor hace visible o no el cursor
    /// </summary>
    /// <param name="input">Componente de input del jugador</param>
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

    // Estos métodos activan un mapa de acción y desactivan el resto
    public void SwitchToUIControls()
    {
        playerControls.Player.Disable();
        playerControls.Dialogues.Disable();

        // Lanzar evento de cambio de estado
        //_playerState.SetState(PlayerState.OnMenu);
        playerControls.UI.Enable();
        _player?.CallMove(Vector2.zero);

        _currentActionMap = UIactionMap;
    }

    public void SwitchToPlayerControls()
    {
        // Lanzar evento cambio de estado
        //_playerState.SetState(PlayerState.Idle);
        playerControls.UI.Disable();
        playerControls.Dialogues.Disable();
        playerControls.Player.Enable();

        _currentActionMap = playerActionMap;
    }

    public void SwitchToDialogueControls()
    {
        playerControls.Player.Disable();
        playerControls.UI.Disable();
        playerControls.Dialogues.Enable();
    }
    public void EnableDialogueInput(bool value)
    {
        _playerControls.Disable();
        if (value) _playerControls.Dialogues.Enable();
        else
        {
            if (_currentActionMap == playerActionMap)
            {
                _playerControls.Player.Enable();
            }
            else if (_currentActionMap == UIactionMap) _playerControls.UI.Enable();
        }
    }

    // Estos métodos activan o desactivan un mapa de acción, independientemente del resto de mapas activos
    public void EnablePlayerInput(bool enable)
    {
        if (enable) _playerControls.Player.Enable();
        else _playerControls.Player.Disable();
    }

    public void EnableDisableManager(bool enable)
    {
        this.enabled = enable;
    }

    public void EnableUIInput(bool enable)
    {
        if (enable) _playerControls.UI.Enable();
        else _playerControls.UI.Disable();
    }
    #endregion

    #region enable/disable
    private void OnEnable()
    {
        // Suscripción a los eventos de input
        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Move.canceled += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed += SecondaryAttack;
        _playerControls.Player.Interact.performed += Interact;
        _playerControls.Player.Map.performed += OpenCloseMap;
        
        _playerControls.Dialogues.NextDialogue.performed += NextDialogue;

        _playerControls.Player.Pause.performed += PauseGame;

        _playerControls.UI.CloseMenu.performed += CloseMenu;
        _playerControls.UI.Cancel.performed += CloseMenu;
    }

    private void OnDisable()
    {
        _playerControls?.Disable();

        // Desuscripción de los eventos de input
        if (_playerControls != null)
        {
            _playerControls.Player.Move.performed -= Move;
            _playerControls.Player.Move.canceled -= Move;
            _playerControls.Player.Blink.performed -= Blink;
            _playerControls.Player.Look.performed -= Look;
            _playerControls.Player.PrimaryAttack.performed -= PrimaryAttack;
            _playerControls.Player.SecondaryAttack.performed -= SecondaryAttack;
            _playerControls.Player.Interact.performed -= Interact;
            _playerControls.Player.Map.performed -= OpenCloseMap;

            _playerControls.Dialogues.NextDialogue.performed -= NextDialogue;

            _playerControls.Player.Pause.performed -= PauseGame;
        }
    }
    
    #endregion
    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _playerControls = new PlayerControls();
        }
    }

    void Start()
    {
        OnStateLoad();
        _stateChanged.Perform.AddListener(OnStateLoad);
        _enablePlayerInput.Perform.AddListener(EnableDisableManager);
        _enableUIInput.Perform.AddListener(EnableUIInput);
        _enablePlayerInput.Perform.AddListener(EnablePlayerInput);

        _enableDialogueInput.Perform.AddListener(EnableDialogueInput);
    }

    void OnDestroy()
    {
        _stateChanged.Perform.RemoveListener(OnStateLoad);
        _enablePlayerInput.Perform.RemoveListener(EnableDisableManager);
        _enableUIInput.Perform.RemoveListener(EnableUIInput);
        _enablePlayerInput.Perform.AddListener(EnablePlayerInput);


        _enableDialogueInput.Perform.RemoveListener(EnableDialogueInput);
    }

}   

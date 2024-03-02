using System;
using UnityEngine;
using UnityEngine.InputSystem;


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
    #endregion

    #region parameters
    [SerializeField]
    private float _interactionRange = 5f;
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

        if (_playerState.playerState == PlayerState.Idle)
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
        if(_playerState.playerState == PlayerState.Idle ||  _playerState.playerState == PlayerState.Attacking)
        {
            _playerState.SetState(PlayerState.OnKnockback);
            _playerMovement.Knockback(pushPosition);

            if (_playerState.playerState == PlayerState.Attacking) //si el jugador esta atacando y recibe un golpe, se realiza knockback y se cancela el retorno a idle (es llamado mas adelante)
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
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //apa�o chungo, cambiar luego
            Vector2 playerPosition = _myTransform.position;
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
            _playerState.SetState(PlayerState.Attacking);

            
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
        //Debug.Log("Interactuando");

        int maxColliders = 40;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(_myTransform.position, _interactionRange, hitColliders);


        for(int i = 0; i < numColliders && _playerState.playerState == PlayerState.Idle; i++)
        {
            // Hacer producto escalar entre el vector lookDirection y el del player-edificio. Devuelve el coseno del �ngulo que forman
            // Si el producto est� entre [- raiz(3)/4, +raiz(3)/4] == �ngulo entre [-15�, +15�] == cono de 30� --> interactuar con el objeto
            
            Vector3 targetDir = (hitColliders[i].transform.position - _myTransform.position).normalized;
            float dotProduct = Vector3.Dot(_lookDirection.lookDirection, targetDir);
            Debug.Log($"collider n� {i}:" + dotProduct);

            if ((dotProduct > - Math.Sqrt(3)/4 || dotProduct < Math.Sqrt(3)/4) 
                && hitColliders[i].gameObject.TryGetComponent(out IInteractable interactableObject))
            {
                Debug.Log("edificio encontrado");
                interactableObject.Interact();
                // Desde el objeto, cambiar el estado del player a OnMenu o algo as�
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

        // No estoy segura de si esto va aqu� o en el OnEnable()
        _playerControls.Player.Move.performed += Move;
        _playerControls.Player.Move.canceled += Move;
        _playerControls.Player.Blink.performed += Blink;
        _playerControls.Player.Look.performed += Look;
        _playerControls.Player.PrimaryAttack.performed += PrimaryAttack;
        _playerControls.Player.SecondaryAttack.performed += SecondaryAttack;
        _playerControls.Player.Interact.performed += Interact;
    }
}

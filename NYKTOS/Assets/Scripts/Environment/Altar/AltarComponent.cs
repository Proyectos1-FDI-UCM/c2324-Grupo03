using UnityEngine;

/// <summary>
/// Componente que tienen los altares. Determina su tipo (magenta, amarillo, cian)
/// Almacena el número de cimientos especiales asociados a esta estructura (nº total y construidos)
/// </summary>
public class AltarComponent : MonoBehaviour
{
    public enum altarType
    {
        yellow, magenta, cyan
    }

    #region references
    private BuildingStateMachine _state;
    private LightAreaComponent _light;
    #endregion

    #region emitters
    [Header("Emitters")]
    [SerializeField] private VoidEmitter _registerPlaceholder;

    // Se invoca cuando se construye un ph (true) y cuando se destruye (false)
    [SerializeField] private BoolEmitter _placeholderBuilt;

    //Sprites
    [SerializeField] private VoidEmitter _altarSprite;

    [SerializeField]
    private VoidEmitter _altarActivated;

    [SerializeField]
    private VoidEmitter _NexusTutorial;
    #endregion

    #region parameters
    [SerializeField]
    private float _inactiveLightRadius = 1f;    
    [SerializeField]
    private float _activeLightRadius = 10f;
    #endregion

    #region properties
    // Cuando se instancian los ph especiales, lanzan un evento para registrar su numero total que se guarda en _totalPlaceholders
    private int _totalPlaceholders = 0;

    // Cada vez que uno de ellos es construido/destruido, lanza un evento para sumarse/restarse
    private int _currentPlaceholders = 0;

    public int CurrentPlaceholders {  get { return _currentPlaceholders; } }


    [SerializeField]
    private altarType _type;

    public altarType type
    {
        get { return _type; }
    }
    #endregion

    // Registra un cimiento
    private void RegisterPlaceholder()
    {
        _totalPlaceholders++;
    }

    // Actualiza la cuenta de cimientos construidos y el estado del altar
    private void PlaceholderCount(bool value)
    {
        if(value)
        {
            _currentPlaceholders++;
            ChangeState();
        }
        else if(_currentPlaceholders > 0)
        {
            _currentPlaceholders--;
            ChangeState();
            
        }        
    }

    // Actualiza el estado, iluminación y apariencia del altar en función del nº de cimientos construidos
    private void ChangeState()
    {
        if (_currentPlaceholders == _totalPlaceholders)
        {
            _state.SetState(BuildingStateMachine.BuildingState.Built);
            _light.lightRadius = _activeLightRadius;
            _light.UpdateLightarea();

            _altarActivated.InvokePerform();
            _NexusTutorial.InvokePerform();
        }
        else if (_state.buildingState == BuildingStateMachine.BuildingState.Built)
        {
            _state.SetState(BuildingStateMachine.BuildingState.NotBuilt);
            _light.lightRadius = _inactiveLightRadius;
            _light.UpdateLightarea();            
        }

        _altarSprite.InvokePerform();
    }

    private void Awake()
    {
        _registerPlaceholder.Perform.AddListener(RegisterPlaceholder);
        _placeholderBuilt.Perform.AddListener(PlaceholderCount);
    }
    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        _light = GetComponentInChildren<LightAreaComponent>();    
    }

    void OnDestroy()
    {
        _registerPlaceholder.Perform.RemoveListener(RegisterPlaceholder);
        _placeholderBuilt.Perform.RemoveListener(PlaceholderCount);
    }
}

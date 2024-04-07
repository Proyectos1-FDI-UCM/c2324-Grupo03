using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour
{
    public enum altarType
    {
        yellow, magenta, cyan
    }

    // [Andrea]

    #region references
    private BuildingStateMachine _state;
    #endregion

    #region emitters
    [Header("Emitters")]
    [SerializeField] private VoidEmitter _registerPlaceholder;

    // Se invoca cuando se construye un ph (true) y cuando se destruye (false)
    [SerializeField] private BoolEmitter _placeholderBuilt;

    #endregion

    #region properties
    // Cuando se instancian los pc especiales, lanzan un evento para registrar su numero total que se guarda en _totalPlaceholders
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

    private void RegisterPlaceholder()
    {
        _totalPlaceholders++;
    }

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

    private void ChangeState()
    {
        // Actualizar apariencia en funcion de numero de _currentPlaceholders

        if (_currentPlaceholders == _totalPlaceholders)
        {
            _state.SetState(BuildingStateMachine.BuildingState.Built);
            Debug.Log("Altar activado");
            // Lanzar evento que active comportamiento especial
        }
        else if (_state.buildingState == BuildingStateMachine.BuildingState.Built)
        {
            _state.SetState(BuildingStateMachine.BuildingState.NotBuilt);
            Debug.Log("Altar desactivado");
            // Lanzar evento que desactive comportamiento especial
            
        }

        
    }

    #region no borrar esto aun
    // De momento esto no hace falta
    //private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;

    //public void OpenMenu()
    //{

    //    if
    //    (
    //        _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt && 
    //        _state.isInteractable)
    //    {
    //        MenuManager.Instance.OpenAltarMenu();
    //    }
    //    else if (_state.isInteractable) 
    //    { 
    //        // Comportamiento en función del tipo de altar. ESTO VA A CAMBIAR
    //        // Ahora actuan todos los altares por igual. Cuando se activa uno, lanzar evento de mejorar armas
    //        switch(_type)
    //        {
    //            case altarType.yellow:
    //                MenuManager.Instance.OpenWeaponUpgradeMenu();
    //                break; 

    //            case altarType.magenta:
    //                MenuManager.Instance.OpenWeaponEffectMenu();
    //                break;

    //            case altarType.cyan:
    //                MenuManager.Instance.OpenWeaponEffectMenu();
    //                break;
    //        }
    //    } 

    //}

    // Cambiar a emitter
    //public void CloseMenu() => MenuManager.Instance.CloseAllMenus();
    #endregion

    private void Awake()
    {
        _registerPlaceholder.Perform.AddListener(RegisterPlaceholder);
        _placeholderBuilt.Perform.AddListener(PlaceholderCount);
    }
    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        BuildingManager.Instance.AddBuilding(gameObject); 
        
    }

    void OnDestroy()
    {
        _registerPlaceholder.Perform.RemoveListener(RegisterPlaceholder);
        _placeholderBuilt.Perform.RemoveListener(PlaceholderCount);
    }
}

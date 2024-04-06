using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour, IBuilding
{
    // [Andrea] Review
    // 

    #region references
    // Evento genérico compartido por todos los altares
    [SerializeField] private BoolEmitter _altarInteract;

    private BuildingStateMachine _state;
    #endregion

    public enum altarType
    {
        yellow, magenta, cyan
    }

    [SerializeField]
    private altarType _type;

    public altarType type
    {
        get { return _type; }
    }

    private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;
    public void OpenMenu()
    {
        if
        (
            _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt && 
            _state.isInteractable)
        {
            MenuManager.Instance.OpenAltarMenu();
        }
        else if (_state.isInteractable) 
        { 
            // Comportamiento en función del tipo de altar. ESTO VA A CAMBIAR
            // Ahora actuan todos los altares por igual. Cuando se activa uno, lanzar evento de mejorar armas
            switch(_type)
            {
                case altarType.yellow:
                    MenuManager.Instance.OpenWeaponUpgradeMenu();
                    break; 

                case altarType.magenta:
                    MenuManager.Instance.OpenWeaponEffectMenu();
                    break;

                case altarType.cyan:
                    MenuManager.Instance.OpenWeaponEffectMenu();
                    break;
            }
        } 
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        BuildingManager.Instance.AddBuilding(gameObject);

        _altarInteract.Perform.AddListener(CanInteract); 
    }

    void OnDestroy()
    {
        _altarInteract.Perform.RemoveListener(CanInteract); 
    }
}

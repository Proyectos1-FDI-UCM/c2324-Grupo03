using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour, IBuilding
{
    // [Andrea] Review
    // Cambiar GenericEmmiter a BoolEmmiter

    #region references
    // Evento genérico compartido por todos los altares
    [SerializeField] private GenericEmitter _altarInteract;

    private BuildingStateMachine _state;
    #endregion

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
            // Activar men� potenciar armas
            // No tengo claro como hacerlo para distinguir el tipo de placeholder
        } 
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        BuildingManager.Instance.AddBuilding(this.gameObject);

        //_altarInteract.Perform.AddListener(CanInteract)
    }
}

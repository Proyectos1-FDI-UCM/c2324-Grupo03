using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour, IBuilding
{
    // [Andrea] Review

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
            // Comportamiento en función del tipo de altar
            switch(_type)
            {
                case altarType.yellow:
                    break; 

                case altarType.magenta:
                    break;

                case altarType.cyan:
                    break;
            }
        } 
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        BuildingManager.Instance.AddBuilding(this.gameObject);

        _altarInteract.Perform.AddListener(CanInteract); 
    }
}

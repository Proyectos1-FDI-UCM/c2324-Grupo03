using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceholderComponent : MonoBehaviour, IBuilding
{
    #region references
    private BuildingStateMachine _state;
    #endregion
    #region properties
    [SerializeField]
    private placeholderType _type;
    public placeholderType type { get { return _type; } }
    #endregion

    #region emitters
    [SerializeField] private BoolEmitter _placeholderInteract;
    [SerializeField] private VoidEmitter _defenseMenu;
    [SerializeField] private PhTypeEmitter _phTypeEmitter;

    [SerializeField]
    private VoidEmitter TutorialConfirm;
    #endregion

    private void CanInteract(bool canInteract) => _state.isInteractable = canInteract;
    public void OpenMenu()
    {
        TutorialConfirm.InvokePerform();
        if
        (
            _state.buildingState == BuildingStateMachine.BuildingState.NotBuilt 
            && _state.isInteractable
        )
        {
            UpdateCurrentPlaceHolder();
            UpdateDefenseMenu();
            _defenseMenu.InvokePerform();
        }
    }

    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    private void UpdateCurrentPlaceHolder()
    {
        BuildingManager.Instance.CurrentPlaceholder = gameObject;
    }

    private void UpdateDefenseMenu()
    {
        Debug.Log("cambio de color");
        Debug.Log(_type);
        _phTypeEmitter.InvokePerform(_type);
    }

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();

        _placeholderInteract.Perform.AddListener(CanInteract);
    }

    void OnDestroy()
    {
        _placeholderInteract.Perform.RemoveListener(CanInteract);
    }
}

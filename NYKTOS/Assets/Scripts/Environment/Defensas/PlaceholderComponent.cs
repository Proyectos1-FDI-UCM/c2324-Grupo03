using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderComponent : MonoBehaviour, IInteractable
{
    #region references
    [SerializeField]
    private Canvas _defenseMenu;

    private PlayerControls _playerControls;
    private BuildingStateMachine _state;
    #endregion

    public void Interact()
    {
        Debug.Log("hola soy un placeholder");
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            Debug.Log("Estoy destruido");
            _playerControls.Player.Disable();
            _playerControls.UI.Enable();
            _defenseMenu.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _playerControls = new PlayerControls();
    }

    // Update is called once per frame
    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }
}

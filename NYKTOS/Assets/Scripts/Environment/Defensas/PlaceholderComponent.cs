using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderComponent : MonoBehaviour, IInteractable
{
    #region references
    [SerializeField]
    private Canvas _defenseMenu;

    private PlayerControls _playerControls;
    #endregion

    public void Interact()
    {
        Debug.Log("hola soy un placeholder");
        _playerControls.Player.Disable();
        _playerControls.UI.Enable();
        _defenseMenu.enabled = true;
    }

    public void QuitInteraction()
    {
        _playerControls.UI.Disable();
        _playerControls.Player.Enable();
        _defenseMenu.enabled = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _playerControls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

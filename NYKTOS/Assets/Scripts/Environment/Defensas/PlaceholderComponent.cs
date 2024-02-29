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
        _playerControls.Player.Disable();
        _defenseMenu.enabled = true;
    }

    public void QuitInteraction()
    {
        _playerControls.Player.Enable();
        _defenseMenu.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

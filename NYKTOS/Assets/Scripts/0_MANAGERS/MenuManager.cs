using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject[] _menuList;

    [SerializeField]
    private Button[] _firstSelected;

    [SerializeField]
    private PlayerController _player;
    #endregion

    private int _menuId = -1;

    public void OpenMenu(int newId)
    {
        if (newId >= 0)
        {
            foreach (GameObject menu in _menuList) menu.SetActive(false);
            _menuId = newId;
            _menuList[_menuId].SetActive(true);
            _firstSelected[_menuId].Select();

            _player.playerControls.Player.Disable();
            _player.playerControls.UI.Enable();
        }
    }

    public void CloseMenu()
    {
        _menuList[_menuId].SetActive(false);
        _menuId = -1;

        _player.playerControls.UI.Disable();
        _player.playerControls.Player.Enable();
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        CloseMenu();
    }

    private void Start()
    {
        _player.playerControls.UI.CloseMenu.performed += CloseMenu;
    }
}

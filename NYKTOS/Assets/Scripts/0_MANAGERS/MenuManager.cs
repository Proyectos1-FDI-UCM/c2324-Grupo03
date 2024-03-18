using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Listado de los menús del juego. ASIGNAR EN ESTE ORDEN (sujeto a cambios)
    /// 
    /// 0 - PAUSA
    /// 1 - Nexo
    /// 2 - Defensas (placeholder)
    /// 3 - Reparación altares (altar destruido)
    /// 4 - Evolucionar arma (altar reparado)
    /// 5 - Efecto especial arma (altar reparado)
    /// </summary>


    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get { return _instance; }
    }

    #region references
    [SerializeField]
    private GameObject[] _menuList;

    [SerializeField]
    private Button[] _firstSelected;

    [SerializeField]
    private GameObject _player;

    private PlayerController _playerController;
    private PlayerStateMachine _playerStateMachine;
    #endregion

    // Menú activo actualmente. Por si hiciese falta acceder a él desde fuera
    private int _menuId = -1;

    public void OpenMenu(int newId)
    {
        if (newId >= 0)
        {
            foreach (GameObject menu in _menuList) menu.SetActive(false);
            _menuId = newId;
            _menuList[_menuId].SetActive(true);
            _firstSelected[_menuId].Select();

            _playerStateMachine.SetState(PlayerState.OnMenu);
            _playerController.playerControls.Player.Disable();
            _playerController.playerControls.UI.Enable();
        }
    }

    public void CloseMenu()
    {
        foreach (GameObject menu in _menuList) menu.SetActive(false);
        _menuId = -1;

        _playerStateMachine.SetState(PlayerState.Idle);
        _playerController.playerControls.UI.Disable();
        _playerController.playerControls.Player.Enable();
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        CloseMenu();
        GameManager.Instance.Resume();
    }


    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerStateMachine = _player.GetComponent<PlayerStateMachine>();

        _playerController.playerControls.UI.CloseMenu.performed += CloseMenu;
    }
}

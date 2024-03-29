using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get { return _instance; }
    }

    #region references
    [SerializeField]
    private GameObject _player;

    private PlayerController _playerController;
    private PlayerStateMachine _playerStateMachine;

    #region menu refs
    [Header("Menu References")]
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _nexusMenu;

    [SerializeField]
    private GameObject _altarMenu;

    [SerializeField]
    private GameObject _defenseMenu;

    [SerializeField]
    private GameObject _weaponUpgradeMenu;

    [SerializeField]
    private GameObject _weaponEffectMenu;

    // Si los settings son solo el sonido, podemos poner un slider en el menu de pausa y ya
    //[SerializeField] 
    //private GameObject _settingsMenu;
    #endregion

    #region button refs
    [Header("Button references")]
    [SerializeField]
    private Button _pauseButton;

    [SerializeField]
    private Button _nexusButton;

    [SerializeField]
    private Button _altarButton;

    [SerializeField]
    private Button _defenseButton;

    [SerializeField]
    private Button _weaponUpgradeButton;

    [SerializeField]
    private Button _weaponEffectButton;

    //[SerializeField]
    //private Button _settingsButton;

    #endregion

    #endregion

    #region properties
    private GameObject _previousMenu;

    private GameObject[] _menuList = new GameObject[6];
    #endregion

    #region menuActions

    public void Resume()
    {
        GameManager.Instance.Resume();
    }

    public void ExitGame()
    {
        GameManager.Instance.Quit();
    }

    public void StartNight()
    {
        GameManager.Instance.UpdateGameState(GameState.Night);
    }

    public void BuildAltar()
    {
        Debug.Log("TODO: Boton de reconstruir altar");
    }

    public void BuildBeacon()
    {
        BuildingManager.Instance.BuildBeacon();
    }

    public void BuildWall()
    {
        BuildingManager.Instance.BuildWall();
    }

    public void BuildTurret()
    {
        BuildingManager.Instance.BuildTurret();
    }

    #endregion

    #region methods

    public void ChangeStateFromMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.Night);
    }

    private void SwitchToUIControls()
    {
        _playerController.playerControls.Player.Disable();
        _playerStateMachine.SetState(PlayerState.OnMenu);
        _playerController.playerControls.UI.Enable();
        _playerController.CallMove(Vector2.zero);
    }
    private void SwitchToPlayerControls()
    {
        _playerStateMachine.SetState(PlayerState.Idle);
        _playerController.playerControls.UI.Disable();
        _playerController.playerControls.Player.Enable();
    }

    private void RegisterMenus()
    {
        _menuList[0] = _pauseMenu;
        _menuList[1] = _nexusMenu;
        _menuList[2] = _altarMenu;
        _menuList[3] = _defenseMenu;
        _menuList[4] = _weaponUpgradeMenu;
        _menuList[5] = _weaponEffectMenu;

    }

    #region open menus
    private void OpenMenu(GameObject menu, Button button)
    {
        menu.SetActive(true);
        button.Select();
        SwitchToUIControls();
    }

    public void OpenPauseMenu() => OpenMenu(_pauseMenu, _pauseButton);
    public void OpenNexusMenu() => OpenMenu(_nexusMenu, _nexusButton);
    public void OpenAltarMenu() => OpenMenu(_altarMenu, _altarButton);
    public void OpenDefenseMenu() => OpenMenu(_defenseMenu, _defenseButton);
    public void OpenWeaponUpgradeMenu() => OpenMenu(_weaponUpgradeMenu, _weaponUpgradeButton);
    public void OpenWeaponEffectMenu() => OpenMenu(_weaponEffectMenu, _weaponEffectButton);

    #endregion

    #region close menus
    public void CloseAllMenus()
    {
        foreach (GameObject menu in _menuList) menu.SetActive(false);

        SwitchToPlayerControls();
        Resume();
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        CloseAllMenus(); 
    }
    #endregion
    #endregion

    void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerStateMachine = _player.GetComponent<PlayerStateMachine>();

        RegisterMenus();

        _playerController.playerControls.UI.CloseMenu.performed += CloseMenu;
    }
}

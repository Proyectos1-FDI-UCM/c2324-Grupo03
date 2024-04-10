using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    // [Andrea]
    // - Cambiar el registro de menus a eventos
    // - Destruir MenuManager al salir al menú principal (puede que haga falta crear un MainMenuManager)

    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get { return _instance; }
    }

    #region references

    [Header("Emitters")]

    [SerializeField]
    private VoidEmitter _closeMenusEmitter;

    [SerializeField]
    private VoidEmitter _pauseMenuEmitter;
    [SerializeField]
    private VoidEmitter _nexusMenuEmitter;
    [SerializeField]
    private VoidEmitter _defenseMenuEmitter;
    [SerializeField]
    private VoidEmitter _weaponUpgradeMenuEmitter;
    [SerializeField]
    private VoidEmitter _weaponEffectMenuEmitter;

    [SerializeField]
    private VoidEmitter _beaconBuildEmitter;
    [SerializeField]
    private VoidEmitter _wallBuildEmitter;
    [SerializeField]
    private VoidEmitter _turretBuildEmitter;

    #region menu refs
    [Header("Menu References")]
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _nexusMenu;

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
    private GameObject[] _menuList = new GameObject[5];
    #endregion

    #region events
    [SerializeField]
    UnityEvent _menuOpened;

    [SerializeField]
    UnityEvent _menuClosed;
    #endregion

    #region menuActions

    public void ExitGame()
    {
        PlayClosedSound();
        Application.Quit();
    }

    public void BuildBeacon()
    {
        PlayOpenedSound();
        BuildingManager.Instance.BuildBeacon();

    }

    public void BuildWall()
    {
        PlayOpenedSound();
        BuildingManager.Instance.BuildWall();
    }

    public void BuildTurret()
    {
        PlayOpenedSound();
        BuildingManager.Instance.BuildTurret();
    }

    #endregion

    #region methods

    private void SwitchToUIControls()
    {
        InputManager.Instance.SwitchToUIControls();
    }
    private void SwitchToPlayerControls()
    {
        InputManager.Instance.SwitchToPlayerControls();
    }

    private void RegisterMenus()
    {
        _menuList[0] = _pauseMenu;
        _menuList[1] = _nexusMenu;
        _menuList[2] = _defenseMenu;
        _menuList[3] = _weaponUpgradeMenu;
        _menuList[4] = _weaponEffectMenu;

    }

    #region open menus
    private void OpenMenu(GameObject menu, Button button)
    {
        PlayOpenedSound();
        menu.SetActive(true);
        button.Select();
        SwitchToUIControls();
    }

    public void OpenPauseMenu() => OpenMenu(_pauseMenu, _pauseButton);
    public void OpenNexusMenu() => OpenMenu(_nexusMenu, _nexusButton);
    public void OpenDefenseMenu() => OpenMenu(_defenseMenu, _defenseButton);
    public void OpenWeaponUpgradeMenu() => OpenMenu(_weaponUpgradeMenu, _weaponUpgradeButton);
    public void OpenWeaponEffectMenu() => OpenMenu(_weaponEffectMenu, _weaponEffectButton);

    #endregion

    #region close menus
    public void CloseAllMenus()
    {
        PlayClosedSound();
        foreach (GameObject menu in _menuList) menu.SetActive(false);

        SwitchToPlayerControls();
    }

    public void CloseMenu()
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

    void Start()
    {
        RegisterMenus();

        _closeMenusEmitter.Perform.AddListener(CloseAllMenus);

        _pauseMenuEmitter.Perform.AddListener(OpenPauseMenu);
        _nexusMenuEmitter.Perform.AddListener(OpenNexusMenu);
        _defenseMenuEmitter.Perform.AddListener(OpenDefenseMenu);
        _weaponUpgradeMenuEmitter.Perform.AddListener(OpenWeaponUpgradeMenu);
        _weaponEffectMenuEmitter.Perform.AddListener(OpenWeaponEffectMenu);

        _beaconBuildEmitter.Perform.AddListener(BuildBeacon);
        _wallBuildEmitter.Perform.AddListener(BuildWall);
        _turretBuildEmitter.Perform.AddListener(BuildTurret);
    }

    void OnDestroy()
    {
        _closeMenusEmitter.Perform.RemoveListener(CloseAllMenus);

        _pauseMenuEmitter.Perform.RemoveListener(OpenPauseMenu);
        _nexusMenuEmitter.Perform.RemoveListener(OpenNexusMenu);
        _defenseMenuEmitter.Perform.RemoveListener(OpenDefenseMenu);
        _weaponUpgradeMenuEmitter.Perform.RemoveListener(OpenWeaponUpgradeMenu);
        _weaponEffectMenuEmitter.Perform.RemoveListener(OpenWeaponEffectMenu);

        _beaconBuildEmitter.Perform.RemoveListener(BuildBeacon);
        _wallBuildEmitter.Perform.RemoveListener(BuildWall);
        _turretBuildEmitter.Perform.RemoveListener(BuildTurret);
    }

    #region EventsMethods
    private void PlayOpenedSound()
    {
        _menuOpened?.Invoke();
    }
    private void PlayClosedSound()
    {
        _menuClosed?.Invoke();
    }
    #endregion
}

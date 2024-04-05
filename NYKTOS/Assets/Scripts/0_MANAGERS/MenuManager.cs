using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{

    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get { return _instance; }
    }

    #region references
    [SerializeField]
    private VoidEmitter _pauseGame;


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

    #region events
    [SerializeField]
    UnityEvent _menuOpened;

    [SerializeField]
    UnityEvent _menuClosed;
    #endregion

    #region menuActions

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        PlayClosedSound();
        Application.Quit();
    }

    public void BuildAltar()
    {
        PlayOpenedSound();
        Debug.Log("TODO: Boton de reconstruir altar");
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
        _menuList[2] = _altarMenu;
        _menuList[3] = _defenseMenu;
        _menuList[4] = _weaponUpgradeMenu;
        _menuList[5] = _weaponEffectMenu;

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
    public void OpenAltarMenu() => OpenMenu(_altarMenu, _altarButton);
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
        Resume();
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

    private void Start()
    {
        RegisterMenus();

        _pauseGame.Perform.AddListener(OpenPauseMenu);
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

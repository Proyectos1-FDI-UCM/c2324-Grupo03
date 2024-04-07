using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour, IBuilding
{
    #region references
    private BuildingStateMachine _state;

    [SerializeField]
    private VoidEmitter _altarActivated;

    [SerializeField]
    private VoidEmitter _weaponUpgraded;

    [SerializeField]
    private VoidEmitter _weaponUpgradeMenu;
    #endregion

    private void EnableWeaponUpgrade()
    {
        // Cuando se mejore el arma, el estado cambia a Built y no permitirá mejorar más veces
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            _state.isInteractable = true;
        }
    }

    private void DisableWeaponUpgrade()
    {
        _state.SetState(BuildingStateMachine.BuildingState.Built);
        _state.isInteractable = false;
    }

    public void OpenMenu()
    {
        if( _state.isInteractable)
        {
            _weaponUpgradeMenu.InvokePerform();
        }
    }

    // Esto no se usa así que da igual que no vaya por emitter
    public void CloseMenu() => MenuManager.Instance.CloseAllMenus();

    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
        _state.isInteractable = false;

        _altarActivated.Perform.AddListener(EnableWeaponUpgrade);
        _weaponUpgraded.Perform.AddListener(DisableWeaponUpgrade);
    }

    void OnDestroy()
    {
        _altarActivated.Perform.RemoveListener(EnableWeaponUpgrade);
        _weaponUpgraded.Perform.RemoveListener(DisableWeaponUpgrade);
    }
}

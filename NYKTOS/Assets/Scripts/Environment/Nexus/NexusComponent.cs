using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusComponent : MonoBehaviour, IBuilding
{
    #region references
    [SerializeField]
    private MenuManager _menuManager;


    private BuildingStateMachine _state;
    [SerializeField]
    private BuildingManager _buildingManager;
    #endregion

    public void OpenMenu()
    {
        _menuManager.OpenMenu(1);
    }

    public void CloseMenu() => _menuManager.CloseMenu();


    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

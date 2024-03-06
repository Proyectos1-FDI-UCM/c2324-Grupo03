using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour, IBuilding
{
    // Codigo de Iker y Andrea :D

    //SI ESTA CONSTRUIDO, ATRAE ENEMIGOS, ACTIVA VIDA Y NO SE PUEDE INTERACTUAR CON EL HASTA QUE SEA DESTRUIDO (ALTARHEALTHCOMPONENT)
    //SI ESTA DESTRUIDO, YA NO ATRAE ENEMIGOS, DESACTIVA EL FACTOR VIDA SE PUEDE INTERACTUAR CON EL (ALTARDESTROYCOMPONENT)

    #region references
    private BuildingStateMachine _state;

    [SerializeField]
    private MenuManager _menuManager;
    #endregion

    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            Debug.Log("hola soy un altar sin reparar");
            // Activar men� construcci�n
        }
        else { } // Activar men� potenciar armas
    }

    public void CloseMenu() => _menuManager.CloseMenu();


    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }
}

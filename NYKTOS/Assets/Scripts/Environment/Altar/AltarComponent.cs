using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltarComponent : MonoBehaviour, IBuilding
{
    // Codigo de Iker y Andrea :D

    //VA ACTUALIZANDO EL ESTADO EN EL QUE SE ENCUENTRA EL ALTAR
    //PORCENTAJE DE VIDA EN INT
    //CONSTRUCCION AUTOMATICA CON UN BOOLEANO
    //SI ESTA CONSTRUIDO, ATRAE ENEMIGOS, ACTIVA VIDA Y NO SE PUEDE INTERACTUAR CON EL HASTA QUE SEA DESTRUIDO (ALTARHEALTHCOMPONENT)
    //SI ESTA DESTRUIDO, YA NO ATRAE ENEMIGOS, DESACTIVA EL FACTOR VIDA SE PUEDE INTERACTUAR CON EL (ALTARDESTROYCOMPONENT)

    #region references
    private BuildingStateMachine _state;
    #endregion

    public void OpenMenu()
    {
        if(_state.buildingState == BuildingStateMachine.BuildingState.NotBuilt)
        {
            Debug.Log("hola soy un altar");
            // Activar menú construcción
        }
        else { } // Activar menú potenciar armas
    }

    public void CloseMenu(InputAction.CallbackContext context) { }


    void Start()
    {
        _state = GetComponent<BuildingStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

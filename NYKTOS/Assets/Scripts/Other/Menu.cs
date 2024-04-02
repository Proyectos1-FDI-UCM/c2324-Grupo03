using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //[Marco] Not optimal
    [SerializeField]
    private GameStateMachine _stateMachine;

    public void Iniciar()
    {
        _stateMachine.SetState(GlobalStateIdentifier.Day);
    }

    public void Salir()
    {
        Application.Quit();
    }

}

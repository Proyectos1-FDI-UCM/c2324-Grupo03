using System;
using System.Collections.Generic;
using UnityEngine;

// Nota: a lo mejor esto debería ser un scriptable singleton
[CreateAssetMenu(fileName = "Game State Machine", menuName = "GameStateMachine/State Machine")]
public class GameStateMachine : ScriptableObject
{
    private CustomState _currentState;
    public GlobalStateIdentifier GetCurrentState
    {
        get{return _currentState.StateIdentifier;}
    }

    public void ResetCurrentState() => _currentState = null;

    public void SetState(CustomState newState)
    {
        Debug.LogError("[STATE MACHINE] Iniciada orden de cambio del estado (" + _currentState + ") al estado (" + newState + ")");

        if(_currentState != null)
        {
            Debug.LogError("[STATE MACHINE] Existe el estado previo (" + _currentState + "), finalizando estado previo");
            _currentState.StateEndSignal.AddListener(() => StateHasFinalised(_currentState, newState));
            _currentState.StateExit();
        }
        else
        {
            Debug.LogError("[STATE MACHINE] No hay estado previo, cargando estado (" + newState + ")");
            newState.StateLoad();
        }
        _currentState = newState;
    }

    private void StateHasFinalised(CustomState oldstate, CustomState newState)
    {
        oldstate.StateEndSignal.RemoveAllListeners();
        Debug.LogError("[STATE MACHINE] El estado (" + oldstate.name + ") ha finalizado");
        newState.StateLoad();
    }

    /// <summary>
    /// [Deprecated] Este método es código duplicado. 
    /// <para>No borrar hasta identificar donde hay llamadas al mismo en eventos y cambiarlo por el de arriba</para>
    /// </summary>
    /// <param name="_state"></param>
    public void SetStateTo(CustomState _state)
    {
        Debug.LogError("[STATE MACHINE] DEPRECATED Orden EXTERNA de cambio a " + _state);
        SetState(_state);
    }

    /// <summary>
    /// [Deprecated] este método es obsoleto y susceptible a errores
    /// </summary>
    public void SetStateToDay()
    {
        Debug.LogError("[STATE MACHINE] DEPRECATED Orden EXTERNA de cambio a DAY");
        //SetState(GlobalStateIdentifier.Day);
    }

    /// <summary>
    /// [Deprecated] este método es obsoleto y susceptible a errores
    /// </summary>
    public void SetStateToNight()
    {
        Debug.LogError("[STATE MACHINE] DEPRECATED Orden EXTERNA de cambio a NIGHT");
        //SetState(GlobalStateIdentifier.Night);
    }

    /// <summary>
    /// [Deprecated] este método es obsoleto y susceptible a errores
    /// </summary>
    public void SetStateToQuit()
    {
        Debug.LogError("[STATE MACHINE] DEPRECATED Orden EXTERNA de cambio a QUIT");
        //SetState(GlobalStateIdentifier.Quit);
    }

    /// <summary>
    /// [Deprecated] este método es obsoleto y susceptible a errores
    /// </summary>
    public void SetStateToLose()
    {
        Debug.LogError("[STATE MACHINE] DEPRECATED Orden EXTERNA de cambio a LOSE");
        //SetState(GlobalStateIdentifier.LoseSceneLoad);
    }
}
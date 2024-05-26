using System;
using System.Collections.Generic;
using UnityEngine;

// Nota: a lo mejor esto debería ser un scriptable singleton
/// <summary>
/// Maquina de estados global que gestiona todos los estados de juego.
/// 
/// <para>
/// Almacena el estado en curso y provee los metodos para cambiar de estado
/// </para>
/// </summary>
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
        Debug.Log("[STATE MACHINE] Iniciada orden de cambio del estado (" + _currentState + ") al estado (" + newState + ")");

        if(_currentState != null)
        {
            Debug.Log("[STATE MACHINE] Existe el estado previo (" + _currentState + "), finalizando estado previo");
            _currentState.StateEndSignal.AddListener(() => StateHasFinalised(_currentState, newState));
            _currentState.StateExit();
        }
        else
        {
            Debug.Log("[STATE MACHINE] No hay estado previo, cargando estado (" + newState + ")");
            _currentState = newState;
            _currentState.StateLoad();
        }
    }

    private void StateHasFinalised(CustomState oldstate, CustomState newState)
    {
        Debug.Log("[STATE MACHINE] El estado (" + oldstate.name + ") ha finalizado");
        _currentState = newState;
        _currentState.StateLoad();
        oldstate.StateEndSignal.RemoveAllListeners();
    }
}
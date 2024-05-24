using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se encarga de cambiar de estados y reproducir sus comportamientos.
/// </summary>
public class StateHandler : MonoBehaviour
{
    [SerializeField]
    private State _currentState;
    public State currentState { get { return _currentState; } }
    private State nextState;

    private void Update()
    {
        _currentState.OnUpdateState();

        if (_currentState.CheckConditions(ref nextState))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        _currentState.OnExitState();
        _currentState = nextState;
        _currentState.OnEnterState();
    }
}

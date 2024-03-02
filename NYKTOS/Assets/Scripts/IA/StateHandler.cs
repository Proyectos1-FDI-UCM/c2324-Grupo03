using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    private State nextState;

    private void Update()
    {
        currentState.OnUpdateState();

        if (currentState.CheckConditions(ref nextState))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        currentState.OnExitState();
        currentState = nextState;
        currentState.OnEnterState();
    }
}

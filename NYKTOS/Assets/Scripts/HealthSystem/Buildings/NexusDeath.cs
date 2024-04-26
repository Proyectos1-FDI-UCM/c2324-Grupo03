using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDeath : MonoBehaviour, IDeath
{
    [SerializeField]
    private GameStateMachine _gameStateMachine;

    [SerializeField]
    private CustomState _loseState;

    public void Death()
    {
        //Cambiar sprite
        // Lanzar evento de perder
        _gameStateMachine.SetState(_loseState);
    }
}

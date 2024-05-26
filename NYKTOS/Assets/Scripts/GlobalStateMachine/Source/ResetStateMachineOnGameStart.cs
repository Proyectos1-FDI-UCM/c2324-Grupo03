using UnityEngine;

/// <summary>
/// Resetea la maquina de estados al iniciar la partida en caso de que por 
/// alguna razón de guarde un estado residual.
/// </summary>
public class ResetStateMachineOnGameStart : MonoBehaviour
{
    public GameStateMachine _gameStateMachine;
    private static bool _gameStarted = false;

    void Start()
    {
        if (!_gameStarted)
        {
            if (_gameStateMachine != null)
            {
                _gameStateMachine.ResetCurrentState();
            }
            _gameStarted = true;
        }
    }
}

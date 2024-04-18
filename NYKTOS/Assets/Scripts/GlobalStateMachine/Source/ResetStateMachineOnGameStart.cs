using UnityEngine;

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

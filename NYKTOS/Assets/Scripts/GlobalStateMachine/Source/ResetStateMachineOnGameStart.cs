using UnityEngine;

public class ResetStateMachineOnGameStart : MonoBehaviour
{
    public GameStateMachine _scriptableObject;
    private static bool _gameStarted = false;

    void Start()
    {
        if (!_gameStarted)
        {
            if (_scriptableObject != null)
            {
                _scriptableObject.ResetCurrentState();
            }
            _gameStarted = true;
        }
    }
}

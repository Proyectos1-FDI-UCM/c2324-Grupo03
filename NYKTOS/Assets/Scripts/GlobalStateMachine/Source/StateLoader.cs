using UnityEngine;

public class StateLoader : MonoBehaviour
{
    [SerializeField]
    private CustomState _state;

    [SerializeField]
    private GameStateMachine _gameStateMachine;

    void Update()
    {
        _gameStateMachine?.SetState(_state);
        gameObject.SetActive(false);
    }
}
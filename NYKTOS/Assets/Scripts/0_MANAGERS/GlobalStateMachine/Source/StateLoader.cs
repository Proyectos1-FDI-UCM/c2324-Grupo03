using UnityEngine;

public class StateLoader : MonoBehaviour
{
    [SerializeField]
    private GameStateMachine _gameStateMachine;

    [SerializeField]
    private GlobalStateIdentifier _identifier;

    void Start()
    {
        _gameStateMachine?.SetState(_identifier);
    }
}
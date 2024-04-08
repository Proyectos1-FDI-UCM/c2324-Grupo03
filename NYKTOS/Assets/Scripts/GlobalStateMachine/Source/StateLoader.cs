using UnityEngine;

public class StateLoader : MonoBehaviour
{
    [SerializeField]
    private GameStateMachine _gameStateMachine;

    [SerializeField]
    private GlobalStateIdentifier _identifier;

    void Update()
    {
        Debug.LogError("INITIAL STATE LOAD");
        _gameStateMachine?.SetState(_identifier);
        gameObject.SetActive(false);
    }
}
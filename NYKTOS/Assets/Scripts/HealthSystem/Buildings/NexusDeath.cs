using UnityEngine;

/// <summary>
/// Cuando el Nexo es destruido, se llama al método Death(), que cambia el estado del juego
/// </summary>
public class NexusDeath : MonoBehaviour, IDeath
{
    [SerializeField]
    private GameStateMachine _gameStateMachine;

    [SerializeField]
    private CustomState _loseState;

    public void Death()
    {
        _gameStateMachine.SetState(_loseState);
    }
}

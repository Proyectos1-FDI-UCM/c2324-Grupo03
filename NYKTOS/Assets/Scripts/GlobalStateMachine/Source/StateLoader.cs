using UnityEngine;

/// <summary>
/// Carga el estado inicial de una escena.
/// 
/// De esta forma es sencillo asociar un estado inicial al empezar cada escena
/// y separar un poco la transicion de escenas y el cambio de estados
/// </summary>
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
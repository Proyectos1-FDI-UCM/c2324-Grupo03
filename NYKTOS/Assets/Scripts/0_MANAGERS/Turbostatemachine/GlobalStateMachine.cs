using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : ScriptableObject
{
    [SerializeField]
    private List<CustomState> _stateList = new List<CustomState>();
    private Dictionary<GlobalStateIdentifier, CustomState> _stateDictionary = 
        new Dictionary<GlobalStateIdentifier, CustomState>();

    public bool TryGetState(GlobalStateIdentifier identifier, out CustomState state)
        => _stateDictionary.TryGetValue(identifier, out state);
    

    void OnValidate()
    {
        _stateDictionary.Clear();

        // Iterar sobre la lista de estados
        foreach (var state in _stateList)
        {
            // Si el identificador del estado no existe aún en el diccionario, agregarlo
            if (!_stateDictionary.ContainsKey(state.StateIdentifier))
            {
                _stateDictionary.Add(state.StateIdentifier, state);
            }
        }
    }
}

public enum GlobalStateIdentifier
{
    None,
    Load,
    Save,
    MainMenu,
    TutorialDay,
    TutorialNight,
    Day,
    Night,
    Lose,
    Win
}
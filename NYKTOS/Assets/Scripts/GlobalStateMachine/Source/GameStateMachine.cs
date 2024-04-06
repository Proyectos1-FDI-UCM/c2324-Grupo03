using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global State Machine", menuName = "GlobalStateMachine/State Machine")]
public class GameStateMachine : ScriptableObject
{
    [SerializeField]
    private List<CustomState> _stateList = new List<CustomState>();
    private Dictionary<GlobalStateIdentifier, CustomState> _stateDictionary = 
        new Dictionary<GlobalStateIdentifier, CustomState>();

    private CustomState _currentState;
    public GlobalStateIdentifier GetCurrentState
    {
        get{return _currentState.StateIdentifier;}
    }

    public bool TryGetState(GlobalStateIdentifier identifier, out CustomState state)
        => _stateDictionary.TryGetValue(identifier, out state);
    
    
    public void SetState(GlobalStateIdentifier identifier)
    {
        if(_stateDictionary.TryGetValue(identifier, out CustomState newState))
        {
            if(_currentState != null)
            {
                _currentState.StateEndSignal.AddListener(newState.StateLoad);
                _currentState.StateExit();
            }
            else
            {
                newState.StateLoad();
            }
        }
    }

    public void SetStateToDay()
    {
        SetState(GlobalStateIdentifier.Day);
    }

    public void SetStateToNight()
    {
        SetState(GlobalStateIdentifier.Night);
    }

    public void SetStateToQuit()
    {
        SetState(GlobalStateIdentifier.Quit);
    }

    void OnValidate()
    {
        _stateDictionary.Clear();

        // Iterar sobre la lista de estados
        foreach (var state in _stateList)
        {
            if(state != null)
            {
                // Si el identificador del estado no existe aún en el diccionario, agregarlo
                if (!_stateDictionary.ContainsKey(state.StateIdentifier) && state.StateIdentifier != GlobalStateIdentifier.None)
                {
                    _stateDictionary.Add(state.StateIdentifier, state);
                }
            }
        }

        /*
        //Cosa de debugeos

        String debugtext = "";

        foreach(var item in _stateDictionary)
        {
            debugtext = debugtext + item + "\n";
        }
        Debug.Log(debugtext);
        */
    }
}

[Serializable]
public enum GlobalStateIdentifier
{
    None,
    Quit,
    Load,
    MainMenu,
    TutorialDay,
    TutorialNight,
    Day,
    Night,
    Lose,
    Win
}
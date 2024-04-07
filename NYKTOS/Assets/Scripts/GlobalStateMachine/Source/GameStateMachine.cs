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

    public void ResetCurrentState() => _currentState = null;

    public bool TryGetState(GlobalStateIdentifier identifier, out CustomState state)
        => _stateDictionary.TryGetValue(identifier, out state);
    
    
    public void SetState(GlobalStateIdentifier identifier)
    {
        //Debug.Log("SET STATE ORDER FROM " + _currentState + " TO " + identifier);
        if(_stateDictionary.TryGetValue(identifier, out CustomState newState))
        {
            if(_currentState != null)
            {
                _currentState.StateEndSignal.AddListener(() => StateHasFinalised(_currentState, newState));
                _currentState.StateExit();
            }
            else
            {
                newState.StateLoad();
            }
            _currentState = newState;
        }
    }

    private void StateHasFinalised(CustomState oldstate, CustomState newState)
    {
        oldstate.StateEndSignal.RemoveAllListeners();
        //Debug.Log(oldstate.name + " " + "FIN DE ESTADO");
        newState.StateLoad();
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

    public void SetStateToLose()
    {
        SetState(GlobalStateIdentifier.Lose);
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
    Win,
    MenuSceneLoad,
    TutorialSceneLoad,
    MaingameSceneLoad
}
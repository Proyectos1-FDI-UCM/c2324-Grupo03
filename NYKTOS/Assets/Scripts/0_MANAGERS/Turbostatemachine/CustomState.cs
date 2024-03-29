using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New State", menuName = "GlobalStateMachine/State")]
public class CustomState : ScriptableObject
{
    [SerializeField]
    private GlobalStateIdentifier _stateIdentifier = GlobalStateIdentifier.None;
    public GlobalStateIdentifier StateIdentifier {get{return _stateIdentifier;}}

    [SerializeField] 
    private UnityEvent _onEnterTransition;
    public UnityEvent OnEnterTransition{get{return _onEnterTransition;}}

    [SerializeField] 
    private UnityEvent _onExitTransition;
    public UnityEvent OnExitTransition{get{return _onExitTransition;}}

    [SerializeField] 
    private UnityEvent _onStateEnter;
    public UnityEvent OnStateEnter{get{return _onStateEnter;}}

    public void OnStateEnterTransition()
    {
        _onEnterTransition.Invoke();
    }

    public void OnStateExitTransition()
    {
        _onExitTransition.Invoke();
    }

    public void OnState()
    {
        _onStateEnter.Invoke();
    }
}
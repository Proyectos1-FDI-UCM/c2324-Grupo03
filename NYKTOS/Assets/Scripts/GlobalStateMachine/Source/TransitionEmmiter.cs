using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Transition Emmiter", menuName = "GlobalStateMachine/Transition Emmiter")]
public class TransitionEmmiter : ScriptableObject
{
    [SerializeField]
    private UnityEvent _startTransition;
    public UnityEvent StartTransition{get{return _startTransition;}}

    public void InvokeStartTransition()
    {
        _startTransition.Invoke();
    }
}
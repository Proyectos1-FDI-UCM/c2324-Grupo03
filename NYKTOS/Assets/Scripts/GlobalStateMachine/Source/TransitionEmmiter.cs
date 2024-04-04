using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Transition Emitter", menuName = "GlobalStateMachine/Transition Emitter")]
public class TransitionEmitter : ScriptableObject
{
    // [Marco] BORRAR
    [SerializeField]
    private UnityEvent _startTransition;
    public UnityEvent StartTransition{get{return _startTransition;}}

    public void InvokeStartTransition()
    {
        _startTransition.Invoke();
    }
}
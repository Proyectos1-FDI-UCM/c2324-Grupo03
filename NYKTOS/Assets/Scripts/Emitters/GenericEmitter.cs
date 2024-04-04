using UnityEngine;
using UnityEngine.Events;

public class GenericEmitter<T> : ScriptableObject
{
    private UnityEvent<T> _perform = new UnityEvent<T>();
    public UnityEvent<T> Perform{get{return _perform;}}

    [ContextMenu("InvokePerform")] 
    public void InvokePerform(T eventData)
    {
        _perform.Invoke(eventData);
    }
}

public class GenericEmitter : ScriptableObject
{
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    [ContextMenu("InvokePerform")] 
    public void InvokePerform()
    {
        _perform.Invoke();
    }
}

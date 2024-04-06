using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEmitter<T> : ScriptableObject
{
    [SerializeField]
    private UnityEvent<T> _perform = new UnityEvent<T>();
    public UnityEvent<T> Perform{get{return _perform;}}

    public void InvokePerform(T eventData)
    {
        _perform.Invoke(eventData);
    }
}

public abstract class GenericEmitter : ScriptableObject
{
    [SerializeField]
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    public void InvokePerform()
    {
        _perform.Invoke();
    }
}

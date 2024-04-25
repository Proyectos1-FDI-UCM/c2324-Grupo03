using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEmitter<T> : ScriptableObject
{
    [SerializeField]
    private bool debug = false;

    [SerializeField]
    private UnityEvent<T> _perform = new UnityEvent<T>();
    public UnityEvent<T> Perform{get{return _perform;}}

    public void InvokePerform(T eventData)
    {
        if (debug) 
        {
            Debug.LogError("[EMITTER] (" + name + ") Emitter lanzado");
        }
        
        _perform.Invoke(eventData);
    }
}

public abstract class GenericEmitter : ScriptableObject
{
    [SerializeField]
    private bool debug = false;

    [SerializeField]
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    public void InvokePerform()
    {
        if (debug) 
        {
            Debug.LogError("[EMITTER] (" + name + ") Emitter lanzado");
        }

        _perform.Invoke();
    }
}

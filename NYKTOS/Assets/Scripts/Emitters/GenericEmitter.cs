using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Int Emitter", menuName = "Emitter/Void")]
public class VoidEmitter  : GenericEmitter {}
[CreateAssetMenu(fileName = "New Bool Emitter", menuName = "Emitter/Bool")]
public class BoolEmitter  : GenericEmitter<bool> {}
[CreateAssetMenu(fileName = "New Int Emitter", menuName = "Emitter/Int")]
public class IntEmitter : GenericEmitter<int> {}
[CreateAssetMenu(fileName = "New Spawndata Emitter", menuName = "Emitter/Spawndata")]
public class SpawnDataEmitter  : GenericEmitter<Dictionary<SpawnerRegion, Enemy[]>> {}

public class GenericEmitter<T> : ScriptableObject
{
    private UnityEvent<T> _perform = new UnityEvent<T>();
    public UnityEvent<T> Perform{get{return _perform;}}

    public void InvokePerform(T eventData)
    {
        _perform.Invoke(eventData);
    }
}
public class GenericEmitter : ScriptableObject
{
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    public void InvokePerform()
    {
        _perform.Invoke();
    }
}

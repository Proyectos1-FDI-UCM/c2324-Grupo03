using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Emmiter", menuName = "Emmiter/Generic")]
public class GenericEmmiter : ScriptableObject
{
    private UnityEvent _perform = new UnityEvent();
    public UnityEvent Perform{get{return _perform;}}

    public void InvokePerform()
    {
        _perform.Invoke();
    }
}

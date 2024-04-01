using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Collaborator Event", menuName = "Emmiter/Collaborator")]
public class CollaboratorEmmiter: ScriptableObject
{
    private UnityEvent _workStart = new UnityEvent();
    public UnityEvent WorkStart{get{return _workStart;}}

    private UnityEvent _workCompleted = new UnityEvent();
    public UnityEvent WorkCompleted{get{return _workCompleted;}}

    private int _subscribedWorkers = 0;

    public void AddWorker()
    {
        _subscribedWorkers++;
    }

    public void DeleteWorker()
    {
        if (_subscribedWorkers > 0)
        {
            _subscribedWorkers--;

            if(_subscribedWorkers <= 0)
            {   
                _subscribedWorkers = 0;
                _workCompleted.Invoke();
            }
        }
    }

    public void InvokeWorkStart()
    {
        _subscribedWorkers = 0;
        _workStart.Invoke();
    }
}
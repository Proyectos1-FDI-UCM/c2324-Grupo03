using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New State", menuName = "GlobalStateMachine/State")]
public class CustomState : ScriptableObject
{
    [SerializeField]
    private GlobalStateIdentifier _stateIdentifier = GlobalStateIdentifier.None;
    public GlobalStateIdentifier StateIdentifier {get{return _stateIdentifier;}}

    [SerializeField] 
    private UnityEvent _onStateInstantLoad;
    public UnityEvent OnStateInstantLoad{get{return _onStateInstantLoad;}}

    [SerializeField]
    private List<CollaboratorEvent> _onStateLoadCollaborators = new List<CollaboratorEvent>();
    private int _pendingLoadCount;

    [SerializeField] 
    private UnityEvent _onStateEnter;
    public UnityEvent OnStateEnter{get{return _onStateEnter;}}

    [SerializeField] 
    private UnityEvent _onStateInstantExit;
    public UnityEvent OnStateInstantExit{get{return _onStateInstantExit;}}

    [SerializeField] 
    private List<CollaboratorEvent> _onStateExitCollaborators = new List<CollaboratorEvent>();
    private int _pendingExitCount;

    private UnityEvent _stateEndSignal = new UnityEvent();
    public UnityEvent StateEndSignal{get{return _stateEndSignal;}}

    public void StateLoad()
    {
        ConsumeCollaboratorList(_pendingLoadCount, _onStateLoadCollaborators, _onStateInstantLoad, _onStateEnter);
    }

    public void StateExit()
    {
        ConsumeCollaboratorList(_pendingExitCount, _onStateExitCollaborators, _onStateInstantExit, _stateEndSignal);
    }

    private void ConsumeCollaboratorList
    (
        int pendingCount, 
        List<CollaboratorEvent> collaboratorList, 
        UnityEvent instantEvent,
        UnityEvent collaboratorEndEvent)
    {
        if (collaboratorList.Count > 0)
        {
            pendingCount = collaboratorList.Count;
            ExecuteCollaboratorEvents(collaboratorList, () => TryComplete(ref pendingCount, collaboratorEndEvent));
        }
        else
        {
            collaboratorEndEvent.Invoke();
        }
        
        instantEvent.Invoke();
    }

    private void ExecuteCollaboratorEvents(List<CollaboratorEvent> collaboratorEvents, UnityAction action)
    {

        foreach (var item in collaboratorEvents)
        {
            item.InvokeWorkStart();

            UnityAction completionAction = null;
            completionAction = () =>
            {
                item.WorkCompleted.RemoveListener(completionAction); // Desuscribirse una vez que se complete
                action.Invoke();
            };
            
            item.WorkCompleted.AddListener(completionAction);
        }

    }

    private void TryComplete(ref int pendingCount, UnityEvent targetEvent)
    {
        pendingCount--;
        if (pendingCount == 0)
        {
            targetEvent.Invoke();
        }
    }
}
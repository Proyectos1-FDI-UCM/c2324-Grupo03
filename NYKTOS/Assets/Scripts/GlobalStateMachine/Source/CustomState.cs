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
    private UnityEvent _onStateInstantLoad;
    public UnityEvent OnStateInstantLoad{get{return _onStateInstantLoad;}}

    [SerializeField]
    private List<CollaboratorEvent> _onStateLoadCollaborators = new List<CollaboratorEvent>();

    [SerializeField] 
    private UnityEvent _onStateEnter;
    public UnityEvent OnStateEnter{get{return _onStateEnter;}}

    [SerializeField] 
    private UnityEvent _onStateInstantExit;
    public UnityEvent OnStateInstantExit{get{return _onStateInstantExit;}}

    [SerializeField] 
    private List<CollaboratorEvent> _onStateExitCollaborators = new List<CollaboratorEvent>();

    private UnityEvent _stateEndSignal = new UnityEvent();
    public UnityEvent StateEndSignal{get{return _stateEndSignal;}}

    private int _currentPendingCount = 0;

    public void StateLoad()
    {
        Debug.Log(name + " STATELOAD");
        ConsumeCollaboratorList(_onStateLoadCollaborators, _onStateInstantLoad, _onStateEnter);
    }

    public void StateExit()
    {
        Debug.Log(name + " STATEXIT");

        ConsumeCollaboratorList(_onStateExitCollaborators, _onStateInstantExit, _stateEndSignal);
    }

    private void ConsumeCollaboratorList
    (
        List<CollaboratorEvent> collaboratorList, 
        UnityEvent instantEvent,
        UnityEvent collaboratorEndEvent)
    {
        if (collaboratorList.Count > 0)
        {
            _currentPendingCount = collaboratorList.Count;
            
            foreach (var item in collaboratorList)
            {
                item.InvokeWorkStart();

                UnityAction completionAction = null;
                completionAction = () =>
                {
                    item.WorkCompleted.RemoveListener(completionAction);
                    TryComplete(collaboratorEndEvent);
                };
                item.WorkCompleted.AddListener(completionAction);
            }
        }
        else
        {
            Debug.Log(name + " COLLABORADOR CON 0 ITEMS");
            collaboratorEndEvent.Invoke();
        }
        instantEvent.Invoke();
    }

    private void TryComplete(UnityEvent targetEvent)
    {
        _currentPendingCount--;
        Debug.Log(name + " TRYCOMPLETE COMPLETED, QUEDAN " + _currentPendingCount);
        if (_currentPendingCount == 0)
        {
            Debug.Log(name + " FIN DE COLABORADOR");
            targetEvent.Invoke();
        }
    }
}
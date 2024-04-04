using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New State", menuName = "GlobalStateMachine/State")]
public class CustomState : ScriptableObject
{
    [SerializeField]
    private SceneAsset _changeToScene;

    [SerializeField]
    private GlobalStateIdentifier _stateIdentifier = GlobalStateIdentifier.None;
    public GlobalStateIdentifier StateIdentifier {get{return _stateIdentifier;}}

    [Header("Collaborator Events")]

    [SerializeField]
    private List<CollaboratorEvent> _onStateLoad = new List<CollaboratorEvent>();
    private int _pendingLoadCount;

    [SerializeField] 
    private List<CollaboratorEvent> _onStateExit = new List<CollaboratorEvent>();
    private int _pendingExitCount;

    [Header("Events")]

    [SerializeField] 
    private UnityEvent _onStateInstantLoad;
    public UnityEvent OnStateInstantLoad{get{return _onStateInstantLoad;}}

    [SerializeField] 
    private UnityEvent _onStateEnter;
    public UnityEvent OnStateEnter{get{return _onStateEnter;}}

    [SerializeField] 
    private UnityEvent _onStateInstantExit;
    public UnityEvent OnStateInstantExit{get{return _onStateInstantExit;}}

    private UnityEvent _stateEndSignal = new UnityEvent();
    public UnityEvent StateEndSignal{get{return _stateEndSignal;}}

    public void StateLoad()
    {
        if (_changeToScene != null)
        {
            StartStateLoad();
        }
        else
        {
            SceneManager.LoadScene(_changeToScene.name);
            SceneManager.sceneLoaded += SceneLoadCompleted;
        }
    }

    private void SceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneLoadCompleted;
        StartStateLoad();
    }

    private void ConsumeCollaboratorList
    (
        int pendingCount, 
        List<CollaboratorEvent> collaboratorList, 
        UnityEvent instantEvent,
        UnityEvent collaboratorEndEvent)
    {
        pendingCount = collaboratorList.Count;
        instantEvent.Invoke();
        ExecuteCollaboratorEvents(collaboratorList, () => TryComplete(ref pendingCount, collaboratorEndEvent));
    }

    private void StartStateLoad()
    {
        ConsumeCollaboratorList(_pendingLoadCount, _onStateLoad, _onStateInstantLoad, _onStateEnter);
    }

    public void StateExit()
    {
        ConsumeCollaboratorList(_pendingExitCount, _onStateExit, _onStateInstantExit, _stateEndSignal);
    }

    private void ExecuteCollaboratorEvents(List<CollaboratorEvent> collaboratorEvents, UnityAction action)
    {
        foreach (var item in collaboratorEvents)
        {
            item.InvokeWorkStart();
            item.WorkCompleted.AddListener(action);
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

    [ContextMenu("DeleteScene")]
    private void DeleteScene()
    {
        _changeToScene = null;
    }
}
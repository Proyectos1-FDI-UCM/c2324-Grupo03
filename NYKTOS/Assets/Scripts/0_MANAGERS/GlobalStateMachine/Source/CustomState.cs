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
    private List<CollaboratorEmmiter> _onStateLoad = new List<CollaboratorEmmiter>();
    private int _pendingLoadCount;

    [SerializeField] 
    private List<CollaboratorEmmiter> _onStateExit = new List<CollaboratorEmmiter>();
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

    public void StateExit()
    {
        _pendingExitCount = _onStateExit.Count;
        _onStateInstantExit.Invoke();
        ExecuteEmmiters(_onStateExit, () => TryComplete(ref _pendingExitCount, _stateEndSignal));
    }

    private void SceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneLoadCompleted;
        StartStateLoad();
    }

    private void StartStateLoad()
    {
        _pendingLoadCount = _onStateLoad.Count;
        _onStateInstantLoad.Invoke();
        ExecuteEmmiters(_onStateLoad, () => TryComplete(ref _pendingLoadCount, _onStateEnter));
    }

    private void ExecuteEmmiters(List<CollaboratorEmmiter> emmiters, UnityAction action)
    {
        foreach (var emmiter in emmiters)
        {
            emmiter.InvokeWorkStart();
            emmiter.WorkCompleted.AddListener(action);
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

    void OnValidate()
    {
        if(_changeToScene!=null)
        {
            Debug.Log(_changeToScene.name);

            Debug.Log(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New State", menuName = "GlobalStateMachine/State")]
public class CustomState : ScriptableObject
{
    [SerializeField]
    private VoidEmitter _stateChanged;

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
        _stateChanged?.Perform.Invoke();
        Debug.LogError("[CUSTOM STATE] (" + name + ") Inicializada fase de carga");
        ConsumeCollaboratorList(_onStateLoadCollaborators, _onStateInstantLoad, _onStateEnter);
    }

    public void StateExit()
    {
        Debug.Log("[CUSTOM STATE] (" + name + ") Inicializada fase de salida");
        ConsumeCollaboratorList(_onStateExitCollaborators, _onStateInstantExit, _stateEndSignal);
    }

    private void ConsumeCollaboratorList
    (
        List<CollaboratorEvent> collaboratorList, 
        UnityEvent instantEvent,
        UnityEvent collaboratorEndEvent
    )
    {
        string debugPhase = (instantEvent == _onStateInstantExit) ? "Exit" : "Load";
        string debugNext = (collaboratorEndEvent == _stateEndSignal) ? "StateEndSignal" : "StateEnter";

        Debug.Log("[CUSTOM STATE] ("+ name + ") (" + debugPhase + ") Consumiendo colaboradores");

        instantEvent.Invoke();

        if (collaboratorList.Count > 0)
        {
            Debug.LogError("[CUSTOM STATE] ("+ name + ") (" + debugPhase + ") Numero de colaboradores: " + collaboratorList.Count);

            _currentPendingCount = collaboratorList.Count;
            
            foreach (var item in collaboratorList)
            {
                item.InvokeWorkStart();
                Debug.Log("[CUSTOM STATE] ("+ name + ") (" + debugPhase + ") Iniciado colaborador (" + item.name + ")");
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
            Debug.LogError("[CUSTOM STATE] ("+ name + ") (" + debugPhase + ") No hay colaboradores. Iniciando (" + debugNext + ") - " 
                + collaboratorEndEvent.GetPersistentEventCount());

            collaboratorEndEvent.Invoke();
        }
    }

    private void TryComplete(UnityEvent targetEvent)
    {
        string debugNext = (targetEvent == _stateEndSignal) ? "StateEndSignal" : "StateEnter";

        _currentPendingCount--;
        Debug.LogError
        (
            "[CUSTOM STATE] ("+ name + ") Se ha completado un colaborador para poder iniciar (" + debugNext +"). Quedan: " 
                + _currentPendingCount
        );
        if (_currentPendingCount == 0)
        {
            Debug.LogError("[CUSTOM STATE] ("+ name + ") Colaboradores agotados. Iniciando (" + debugNext +") - " 
                + targetEvent.GetPersistentEventCount());
            targetEvent.Invoke();
        }
    }
}

[System.Serializable]
public enum GlobalStateIdentifier
{
    None,
    Quit,
    Load,
    MainMenu,
    TutorialDay,
    TutorialNight,
    Day,
    Night,
    Lose,
    Win,
    MenuSceneLoad,
    TutorialSceneLoad,
    MaingameSceneLoad,
    WinSceneLoad,
    LoseSceneLoad
}
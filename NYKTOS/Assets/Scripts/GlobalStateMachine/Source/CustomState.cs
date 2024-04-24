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
        Debug.Log("[CUSTOM STATE] (" + name + ") Inicializada fase de carga");
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
        UnityEvent collaboratorEndEvent)
    {
        Debug.Log("[CUSTOM STATE] ("+ name + ") (" + instantEvent + ") Consumiendo colaboradores. Instantaneo: (" + instantEvent + ")" );

        instantEvent.Invoke();

        if (collaboratorList.Count > 0)
        {
            Debug.Log("[CUSTOM STATE] ("+ name + ") (" + instantEvent + ") Numero de colaboradores: " + collaboratorList.Count);

            _currentPendingCount = collaboratorList.Count;
            
            foreach (var item in collaboratorList)
            {
                item.InvokeWorkStart();
                Debug.Log("[CUSTOM STATE] ("+ name + ") (" + instantEvent + ") Iniciado colaborador (" + item.name + ")");
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
            Debug.Log("[CUSTOM STATE] ("+ name + ") (" + instantEvent + ") No hay colaboradores. Iniciando (" + collaboratorEndEvent + ") - " 
                + collaboratorEndEvent.GetPersistentEventCount());

            collaboratorEndEvent.Invoke();
        }
    }

    private void TryComplete(UnityEvent targetEvent)
    {
        _currentPendingCount--;
        Debug.Log
        (
            "[CUSTOM STATE] ("+ name + ") Se ha completado un colaborador para poder iniciar (" + targetEvent +"). Quedan: " 
                + _currentPendingCount
        );
        if (_currentPendingCount == 0)
        {
            Debug.Log("[CUSTOM STATE] ("+ name + ") Colaboradores agotados. Iniciando (" + targetEvent +") - " 
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
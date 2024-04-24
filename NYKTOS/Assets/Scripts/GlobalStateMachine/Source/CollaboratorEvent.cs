using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Collaborator Event", menuName = "Collaborator Event")]
public class CollaboratorEvent: ScriptableObject
{
    private UnityEvent _workStart = new UnityEvent();
    public UnityEvent WorkStart{get{return _workStart;}}

    private UnityEvent _workCompleted = new UnityEvent();
    public UnityEvent WorkCompleted{get{return _workCompleted;}}

    private int _subscribedWorkers = 0;

    public void AddWorker()
    {
        _subscribedWorkers++;
        Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + "Trabajador añadido. Total: " + _subscribedWorkers);
    }

    public void DeleteWorker()
    {
        if (_subscribedWorkers > 0)
        {
            _subscribedWorkers--;

            Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + "Trabajador finalizado. Restantes: " + _subscribedWorkers);

            if(_subscribedWorkers <= 0)
            {   
                Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + " Evento completado, lanzando señal de fin de trabajo");
                _subscribedWorkers = 0;
                _workCompleted.Invoke();
            }
        }
        else
        {
            Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + " ERROR, RUTA INCORRECTA");
            _subscribedWorkers = 0;
            _workCompleted.Invoke();
        }
    }

    public void InvokeWorkStart()
    {
        Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + " Iniciando evento colaborador");
        _subscribedWorkers = 0;
        _workStart.Invoke();
    }
}
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Evento que al recibir señales de fin de trabajo de todos los elementos
/// suscritos al evento manda un evento de fin de trabajo colaborativo.
/// </summary>
[CreateAssetMenu(fileName = "New Collaborator Event", menuName = "Collaborator Event")]
public class CollaboratorEvent: ScriptableObject
{
    private UnityEvent _workStart = new UnityEvent();
    public UnityEvent WorkStart{get{return _workStart;}}

    private UnityEvent _workCompleted = new UnityEvent();
    public UnityEvent WorkCompleted{get{return _workCompleted;}}

    private int _subscribedWorkers = 0;

    /// <summary>
    /// Lo llaman los suscriptores (CollaboratorWorker) del evento al lanzarse el evento workstart.
    /// 
    /// Añade 1 a la lista de trabajadores suscritos
    /// </summary>
    public void AddWorker()
    {
        _subscribedWorkers++;
        Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + "Trabajador añadido. Total: " + _subscribedWorkers);
    }

    /// <summary>
    /// Los workers lo llaman al terminar un trabajo.
    /// 
    /// Si el numero de trabajadores restantes es 0 se lanza el metodo workCompleted
    /// </summary>
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

    /// <summary>
    /// Inicia evento 
    /// </summary>
    public void InvokeWorkStart()
    {
        Debug.Log("[COLLABORATOR EVENT] (" + name + ")" + " Iniciando evento colaborador");
        _subscribedWorkers = 0;
        _workStart.Invoke();
    }
}
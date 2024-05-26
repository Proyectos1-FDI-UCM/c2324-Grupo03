using System.Collections;
using UnityEngine;

/// <summary>
/// Trabajador de collaboratorEvent. Todos los scripts que utilizen un collaborator event deben de heradar
/// de esta clase abstracta (con una excepcion)
/// </summary>
public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEvent _collaboratorEvent;

    /// <summary>
    /// Cuando el collaborator event se inicia este es el metodo suscrito al evento de inicio
    /// </summary>
    private void StartWorker()
    {
        _collaboratorEvent.AddWorker();
        StartCoroutine(WorkerCoroutine());
    }

    /// <summary>
    /// Corrutina de inicio de trabajo, solo existe para asegurar de que al finalizar el trabajo
    /// se borre a si misma del collaborator event
    /// </summary>
    /// <returns></returns>
    private IEnumerator WorkerCoroutine()
    {
        yield return Perform();

        // Safeguard
        yield return new WaitForSeconds(0.1f);
        
        _collaboratorEvent.DeleteWorker();
    }

    /// <summary>
    /// Corrutina abstracta donde se ejecuta el codigo del worker
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator Perform();

    /// <summary>
    /// Utilizado en caso de ser necesario ampliar el awake
    /// </summary>
    protected virtual void WorkerAwake(){}

    void Awake()
    {
        _collaboratorEvent.WorkStart?.AddListener(StartWorker);
        WorkerAwake();
    }

    [ExecuteInEditMode]
    void OnDestroy()
    {
        _collaboratorEvent.WorkStart?.RemoveListener(StartWorker);
    }
}
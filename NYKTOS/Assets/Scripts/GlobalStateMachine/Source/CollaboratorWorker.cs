using System.Collections;
using UnityEngine;

public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEvent _collaboratorEvent;

    private void StartWorker()
    {
        _collaboratorEvent.AddWorker();
        StartCoroutine(WorkerCoroutine());
    }

    private IEnumerator WorkerCoroutine()
    {
        yield return Perform();

        // Safeguard
        yield return new WaitForSeconds(0.1f);
        
        _collaboratorEvent.DeleteWorker();
    }

    protected abstract IEnumerator Perform();

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
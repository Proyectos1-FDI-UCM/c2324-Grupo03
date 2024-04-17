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
        _collaboratorEvent.DeleteWorker();
    }

    protected abstract IEnumerator Perform();

    protected abstract void WorkerAwake();

    protected abstract void WorkerOnDestroy();

    void Awake()
    {
        WorkerAwake();
        _collaboratorEvent.WorkStart?.AddListener(StartWorker);
    }

    [ExecuteInEditMode]
    void OnDestroy()
    {
        WorkerOnDestroy();
        _collaboratorEvent.WorkStart?.RemoveListener(StartWorker);
    }
}
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

    void Awake()
    {
        _collaboratorEvent.WorkStart.AddListener(StartWorker);
    }

    void OnDestroy()
    {
        _collaboratorEvent.WorkStart.RemoveListener(StartWorker);
    }
}
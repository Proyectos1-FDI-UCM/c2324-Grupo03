using System.Collections;
using UnityEngine;

public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEvent _emitter;

    void Awake()
    {
        _emitter.WorkStart.AddListener(StartWorker);
    }

    private void StartWorker()
    {
        _emitter.AddWorker();
        Perform();
        StartCoroutine(WorkerCorroutine());
    }

    private IEnumerator WorkerCorroutine()
    {
        yield return Perform();
        _emitter.DeleteWorker();
    }

    protected abstract IEnumerator Perform();
}
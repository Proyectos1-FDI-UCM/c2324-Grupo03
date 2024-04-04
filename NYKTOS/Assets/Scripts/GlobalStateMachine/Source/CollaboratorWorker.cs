using System.Collections;
using UnityEngine;

public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEvent _emmiter;

    void Awake()
    {
        _emmiter.WorkStart.AddListener(StartWorker);
    }

    private void StartWorker()
    {
        _emmiter.AddWorker();
        Perform();
        StartCoroutine(WorkerCorroutine());
    }

    private IEnumerator WorkerCorroutine()
    {
        yield return Perform();
        _emmiter.DeleteWorker();
    }

    protected abstract IEnumerator Perform();
}
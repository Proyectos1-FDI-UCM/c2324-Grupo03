using System.Collections;
using UnityEngine;

public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEmmiter _emmiter;

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

    public void StopWorker()
    {
        _emmiter.DeleteWorker();
    }

    private IEnumerator WorkerCorroutine()
    {
        yield return Perform();
        StopWorker();
    }

    protected abstract IEnumerator Perform();
}
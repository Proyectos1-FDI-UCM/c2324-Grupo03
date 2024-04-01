using UnityEngine;

public abstract class CollaboratorWorker : MonoBehaviour
{
    [SerializeField]
    private CollaboratorEmmiter _emmiter;

    void Awake()
    {
        _emmiter.WorkStart.AddListener(StartWorker);
    }

    public void StartWorker()
    {
        _emmiter.AddWorker();
        Perform();
    }

    public void StopWorker()
    {
        _emmiter.DeleteWorker();
    }

    protected abstract void Perform();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSaver : CollaboratorWorker
{
    private static ProgressSaver _instance;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    protected override IEnumerator Perform()
    {
        _placeholderData.SavePlaceholderData();
        yield return null;
    }

    protected override void WorkerAwake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    protected override void WorkerOnDestroy(){}
}

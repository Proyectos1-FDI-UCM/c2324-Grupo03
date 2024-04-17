using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressLoader : CollaboratorWorker
{
    private static ProgressLoader _instance;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    [SerializeField]
    private CollaboratorEvent _placeholderLoadEvent;

    private bool _workCompletedCondition = false;

    protected override IEnumerator Perform()
    {
        //_placeholderData.LoadPlaceholderData();
        yield return null;
        //PlayerController.playerTransform.gameObject.GetComponent<WeaponHandler>().MyWeapons;

        _workCompletedCondition = false;
        _placeholderLoadEvent.InvokeWorkStart();
        _placeholderLoadEvent.WorkCompleted.AddListener(TriggerWorkCompleted);

        while(!_workCompletedCondition)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    private void TriggerWorkCompleted()
    {
        _workCompletedCondition = true;
        _placeholderLoadEvent.WorkCompleted.RemoveListener(TriggerWorkCompleted);
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

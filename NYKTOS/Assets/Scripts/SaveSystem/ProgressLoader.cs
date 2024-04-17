using System.Collections;
using UnityEngine;

public class ProgressLoader : CollaboratorWorker
{
    [SerializeField]
    private float _waitTime = 5.0f;

    private static ProgressLoader _instance;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private CollaboratorEvent _placeholderLoadEvent;

    private bool _newGameFlag = false;
    public void SetNewGameFlag(bool val)
    {
        _newGameFlag = val;
    }

    private bool _workCompletedCondition = false;

    protected override IEnumerator Perform()
    {
        if (!_newGameFlag)
        {
            yield return new WaitForSeconds(_waitTime);

            ProgressData loadedData = ProgressData.Load();
            
            yield return null;

            _placeholderData.CurrentPlaceholders = loadedData.PlaceholderData;
            _playerInventory.Amarillo = loadedData.Yellow;
            _playerInventory.Magenta = loadedData.Magenta;
            _playerInventory.Cian = loadedData.Cyan;

            PlayerController
                .playerTransform
                .gameObject
                .GetComponent<WeaponHandler>()
                .SetWeapon(loadedData.Weapon);

            _workCompletedCondition = false;
            _placeholderLoadEvent.InvokeWorkStart();
            _placeholderLoadEvent.WorkCompleted.AddListener(TriggerWorkCompleted);

            yield return null;

            while(!_workCompletedCondition)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            _newGameFlag = false;
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

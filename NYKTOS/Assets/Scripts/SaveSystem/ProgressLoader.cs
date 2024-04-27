using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProgressLoader : CollaboratorWorker
{
    [SerializeField]
    private float _waitTime = 5.0f;

    private static ProgressLoader _instance;

    [SerializeField]
    private NightProgressTracker _nightProgressTracker;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private CollaboratorEvent _placeholderLoadEvent;

    [SerializeField]
    private UnityEvent _upgradeWeapon = new UnityEvent();
    public UnityEvent UpgradeWeapon { get { return _upgradeWeapon; } }

    private static bool _newGameFlag = false;
    public static void ActivateNewGameFlag()
    {
        _newGameFlag = true;
    }

    private bool _workCompletedCondition = false;

    protected override IEnumerator Perform()
    {
        if (!_newGameFlag)
        {
            yield return new WaitForSeconds(_waitTime);

            ProgressData loadedData = ProgressData.Load();
            
            yield return null;

            if (loadedData != null)
            {
                _nightProgressTracker.Night = loadedData.Night;
                _placeholderData.CurrentPlaceholders = loadedData.PlaceholderData;
                _playerInventory.Amarillo = loadedData.Yellow;
                _playerInventory.Magenta = loadedData.Magenta;
                _playerInventory.Cian = loadedData.Cyan;

                if (loadedData.UpgradedWeapon)
                {
                    _upgradeWeapon.Invoke();
                }

                ControlCinemachine.OneTimeCinematic = loadedData.CinematicPlayed;

                _workCompletedCondition = false;
                _placeholderLoadEvent.InvokeWorkStart();
                _placeholderLoadEvent.WorkCompleted.AddListener(TriggerWorkCompleted);

                yield return null;

                while(!_workCompletedCondition)
                {
                    yield return new WaitForSeconds(0.1f);
                }
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
}

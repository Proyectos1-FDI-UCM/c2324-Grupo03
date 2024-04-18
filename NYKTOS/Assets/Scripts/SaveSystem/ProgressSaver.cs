using System.Collections;
using UnityEngine;

public class ProgressSaver : CollaboratorWorker
{
    [SerializeField]
    private float _waitTime = 5.0f;

    private static ProgressSaver _instance;

    [SerializeField]
    private NightProgressTracker _nightProgress;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private WeaponScriptableObject _defaultWeapon;

    protected override IEnumerator Perform()
    {
        yield return new WaitForSeconds(_waitTime);

        bool upgradedCheck =
        (
            PlayerController
                .playerTransform
                .gameObject
                .GetComponent<WeaponHandler>()
                .Weapon
            != 
            _defaultWeapon
        );

        ProgressData.Save
        (
            _nightProgress.Night,
            _placeholderData.CurrentPlaceholders, 
            _playerInventory.Amarillo, 
            _playerInventory.Magenta, 
            _playerInventory.Cian,
            upgradedCheck,
            ControlCinemachine.OneTimeCinematic
        );
        
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
}

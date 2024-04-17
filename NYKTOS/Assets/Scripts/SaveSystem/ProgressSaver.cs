using System.Collections;
using UnityEngine;

public class ProgressSaver : CollaboratorWorker
{
    [SerializeField]
    private float _waitTime = 5.0f;

    private static ProgressSaver _instance;

    [SerializeField]
    private PlaceholderSaveData _placeholderData;

    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private CollaboratorEvent _placeholderSaveEvent;


    protected override IEnumerator Perform()
    {
        yield return new WaitForSeconds(_waitTime);

        ProgressData.Save
        (
            _placeholderData.CurrentPlaceholders, 
            _playerInventory.Amarillo, 
            _playerInventory.Magenta, 
            _playerInventory.Cian,
            PlayerController
                .playerTransform
                .gameObject
                .GetComponent<WeaponHandler>()
                .Weapon
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

    protected override void WorkerOnDestroy(){}
}

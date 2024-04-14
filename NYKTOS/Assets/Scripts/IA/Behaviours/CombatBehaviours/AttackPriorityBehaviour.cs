using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPriority : MonoBehaviour, IBehaviour
{
    #region references
    private WeaponHandler _weaponHandler;
    private Transform _myTransform;
    private EnemyPriorityComponent _enemyPriorityComponent;

    private float time = 1;
    #endregion

    Vector2 direction;
    [SerializeField] private GameObject waitCondition;
    public void PerformBehaviour()
    {
        if (_enemyPriorityComponent.priorityPath.corners.Length > 0)
        {
            StartCoroutine(Attack());
        }


    }

    private void Awake()
    {
        _weaponHandler = GetComponentInParent<WeaponHandler>();
        _myTransform = transform;
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();

        time = waitCondition.GetComponent<WaitCondition>().waitTime;
    }

    IEnumerator Attack()
    {
        direction = _enemyPriorityComponent._nearestPriorityObject.transform.position - _myTransform.position;

        yield return new WaitForSeconds(time);

        _weaponHandler.CallPrimaryUse(direction);

    }
}

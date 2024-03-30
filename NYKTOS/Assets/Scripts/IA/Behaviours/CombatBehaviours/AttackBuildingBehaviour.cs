using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuildingBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private WeaponHandler _weaponHandler;
    private Transform _myTransform;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion
    public void PerformBehaviour()
    {
        if (_enemyPriorityComponent.toNearestBuildingPath.corners.Length >0)
        _weaponHandler.CallPrimaryUse(_enemyPriorityComponent._nearestBuildingObject.transform.position - _myTransform.position);
    }

    private void Awake()
    {
        _weaponHandler = GetComponentInParent<WeaponHandler>();
        _myTransform = transform;
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();
    }
}

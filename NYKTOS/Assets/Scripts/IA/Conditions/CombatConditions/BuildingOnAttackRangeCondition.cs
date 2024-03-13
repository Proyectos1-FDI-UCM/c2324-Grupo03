using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOnAttackRangeCondition : MonoBehaviour, ICondition
{
    #region references
    private Transform _myTransform;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion

    [SerializeField] private float attackRange = 1.5f;
    public bool Validate(GameObject game)
    {
        if (_enemyPriorityComponent.toNearestBuildingPath.corners.Length > 0) 
        {
            float distanceToBuilding = Vector3.Magnitude(_enemyPriorityComponent.toNearestBuildingPath.corners[_enemyPriorityComponent.toNearestBuildingPath.corners.Length - 1] - _myTransform.position);

            return distanceToBuilding <= attackRange;
        }

        else return false;
        
    }

    private void Awake()
    {
        _myTransform = transform;
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();
    }
}

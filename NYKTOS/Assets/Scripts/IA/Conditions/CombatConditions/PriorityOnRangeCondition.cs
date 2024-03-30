using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityOnRangeCondition : MonoBehaviour, ICondition
{
    #region references
    private Transform _myTransform;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion

    [SerializeField] private float attackRange = 1.5f;
    public bool Validate(GameObject game)
    {
        if (_enemyPriorityComponent.priorityPath.corners.Length > 0)
        {
            float distanceToBuilding = Vector3.Magnitude(_enemyPriorityComponent.priorityPath.corners[_enemyPriorityComponent.priorityPath.corners.Length-1] - _myTransform.position);

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

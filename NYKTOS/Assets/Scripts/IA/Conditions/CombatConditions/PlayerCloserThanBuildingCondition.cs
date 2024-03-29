using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PlayerCloserThanBuildingCondition : MonoBehaviour, ICondition
{
    #region references
    private EnemyPriorityComponent _enemyPriorityComponent;
    private Transform _myTransform;
    #endregion

    public bool Validate(GameObject game)
    {
        if (_enemyPriorityComponent.toNearestBuildingPath != null && _enemyPriorityComponent.toNearestBuildingPath.corners.Length > 0)
        {
            Vector3 _nearestBuilding = _enemyPriorityComponent.toNearestBuildingPath.corners[_enemyPriorityComponent.toNearestBuildingPath.corners.Length - 1];

            float distanceToPlayer = Vector3.Magnitude(PlayerController.playerTransform.position - _myTransform.position);
            float distanceToNearestBuilding = Vector3.Magnitude(_nearestBuilding - _myTransform.position);
            return distanceToPlayer <= distanceToNearestBuilding;
        }
        else return false;
       
    }

    private void Start()
    {
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();
        _myTransform = transform;
    }
}

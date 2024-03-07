using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNearestBuildingBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private Transform _myTransform;
    private RBMovement _rbMovement;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion

    #region parameters
    [SerializeField] float _entityReactionTime = 0.4f;
    #endregion

    #region properties
    private Vector2 direction = Vector2.zero;
    #endregion

    public void PerformBehaviour()
    {

        if (_enemyPriorityComponent.toNearestBuildingPath.corners.Length > 1) //calculo de camino a tomar
        {
            direction = (_enemyPriorityComponent.toNearestBuildingPath.corners[1] - _myTransform.position).normalized;

            if (!IsInvoking(nameof(Move)))
                Invoke(nameof(Move), _entityReactionTime);

            //debug
            for (int i = 0; i < _enemyPriorityComponent.toNearestBuildingPath.corners.Length - 1; i++)
            {
                Debug.DrawLine(_enemyPriorityComponent.toNearestBuildingPath.corners[i], _enemyPriorityComponent.toNearestBuildingPath.corners[i + 1], Color.red);

            }

        }


    }

    private void Move()
    {
        _rbMovement.OrthogonalMovement(direction);
    }

    private void Start()
    {
        _myTransform = transform;
        _rbMovement = GetComponentInParent<RBMovement>();
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();
    }
}

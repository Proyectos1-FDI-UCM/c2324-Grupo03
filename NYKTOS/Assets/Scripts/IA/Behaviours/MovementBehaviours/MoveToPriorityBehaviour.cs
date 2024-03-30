using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPriorityBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private Transform _myTransform;
    private RBMovement _rbMovement;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion

    #region properties
    private Vector2 direction = Vector2.zero;
    #endregion

    bool started = false;
    public void PerformBehaviour()
    {

        if (_enemyPriorityComponent.priorityPath.corners.Length > 1) //calculo de camino a tomar
        {
            direction = (_enemyPriorityComponent.priorityPath.corners[1] - _myTransform.position).normalized;

            _rbMovement.OrthogonalMovement(direction);

        }
        else
        {
            _rbMovement.OrthogonalMovement(Vector2.zero);
        }
    }

    private void Start()
    {
        _myTransform = transform;
        _rbMovement = GetComponentInParent<RBMovement>();
        _enemyPriorityComponent = GetComponentInParent<EnemyPriorityComponent>();
    }
}


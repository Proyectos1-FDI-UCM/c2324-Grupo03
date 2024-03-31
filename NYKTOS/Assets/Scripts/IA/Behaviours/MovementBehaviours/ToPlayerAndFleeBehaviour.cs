using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPlayerAndFleeBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private Transform _myTransform;
    private RBMovement _rbMovement;
    private EnemyPriorityComponent _enemyPriorityComponent;
    #endregion

    #region parameters
    [SerializeField] private float _minDistanceToPlayer = 3;
    [SerializeField] float _maxDistanceToPlayer = 6;
    [SerializeField] bool gizmos = true;
    #endregion

    #region properties
    private Vector2 direction = Vector2.zero;
    #endregion

    public void PerformBehaviour()
    {

        if (_enemyPriorityComponent.toPlayerPath.corners.Length > 1) //calculo de camino a tomar
        {
            float distance = (PlayerController.playerTransform.position - _myTransform.position).magnitude;
            direction = (_enemyPriorityComponent.toPlayerPath.corners[1] - _myTransform.position).normalized;

            if (distance > _minDistanceToPlayer && distance > _maxDistanceToPlayer)
            {
                print("gotoplayer");
                _rbMovement.OrthogonalMovement(direction);
            }
            else if (distance > _minDistanceToPlayer && distance <= _maxDistanceToPlayer)
            {
                print("stop");
                _rbMovement.OrthogonalMovement(Vector2.zero);
            }
            else
            {
                print("flee");
                _rbMovement.OrthogonalMovement(-direction);
            }
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

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_myTransform.position, _minDistanceToPlayer);
            Gizmos.DrawWireSphere(_myTransform.position, _maxDistanceToPlayer);
        }
    }
}

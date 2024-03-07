using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private NavMeshPath _path;
    private Transform _myTransform;
    private RBMovement _rbMovement;
    #endregion

    #region parameters
    [SerializeField] float _entityReactionTime = 0.4f;
    #endregion

    #region properties
    private Vector2 direction = Vector2.zero;
    #endregion

    public void PerformBehaviour()
    {
        
        if (NavMesh.CalculatePath(transform.position, PlayerController.playerTransform.position, NavMesh.AllAreas, _path) && _path.corners.Length > 1) //calculo de camino a tomar
        {
            direction = (_path.corners[1] - _myTransform.position).normalized;

            if (!IsInvoking(nameof(Move)))
                Invoke(nameof(Move), _entityReactionTime);

            //debug
            for (int i = 0; i < _path.corners.Length - 1; i++)
            {
                Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red);

            }

        } 


    }

    private void Move()
    {
        _rbMovement.OrthogonalMovement(direction);
    }

    private void Start()
    {
        _path = new NavMeshPath();
        _myTransform = transform;
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}

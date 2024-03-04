using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerBehaviour : MonoBehaviour, IBehaviour
{
    #region references
    private NavMeshPath _path;
    [SerializeField]
    private Transform _targetTransform;
    private Transform _myTransform;
    private RBMovement _rbMovement;
    #endregion

    #region properties
    private Vector2 direction = Vector2.zero;
    #endregion

    public void PerformBehaviour()
    {
        
        NavMesh.CalculatePath(transform.position, _targetTransform.position, NavMesh.AllAreas,_path); //calculo de camino a tomar

        direction = (_path.corners[1] - _myTransform.position).normalized;
        
        _rbMovement.xAxisMovement(direction.x);
        _rbMovement.yAxisMovement(direction.y);

        //debug
        for (int i =0; i<_path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i+1], Color.red);
            
        }
        
    }

    private void Start()
    {
        _path = new NavMeshPath();
        _myTransform = transform;
        _rbMovement = GetComponentInParent<RBMovement>();
    }
}

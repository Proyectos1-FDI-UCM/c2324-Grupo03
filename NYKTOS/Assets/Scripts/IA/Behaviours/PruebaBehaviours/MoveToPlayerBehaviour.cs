using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerBehaviour : MonoBehaviour, IBehaviour
{
    private NavMeshPath _path;
    private Vector2 _position = Vector2.zero;
    private Transform _myTransform;
    
    public void PerformBehaviour()
    {
        _path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, _position, NavMesh.AllAreas,_path);

        for (int i =0; i<_path.corners.Length - 1; i++)
        {
            Debug.DrawLine(_path.corners[i], _path.corners[i+1], Color.red);
        }
        
    }

    private void Start()
    {
        _myTransform = transform;
    }
}

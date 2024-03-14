using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyPriorityComponent : MonoBehaviour
{
    //la funcion de este script es de calcular el camino del jugador y el del edificio mas cercano
    #region properties
    private Transform _playerTransform { get { return PlayerController.playerTransform; } }
    private List<GameObject> _buildingArray { get { return BuildingManager.Instance.buildingArray; } }  

    private Transform _myTransform;


    private NavMeshPath _toPlayerPath;
    public NavMeshPath toPlayerPath { get { return _toPlayerPath; } }

    private NavMeshPath _toNearestBuildingPath;
    public NavMeshPath toNearestBuildingPath { get { return _toNearestBuildingPath; } }

    #endregion
    
    private void Update()
    {
        //PLAYER PATH CALCULATION
        NavMesh.CalculatePath(_myTransform.position, PlayerController.playerTransform.position, NavMesh.AllAreas, _toPlayerPath);

        //NEAREST BUILDING PATH CALCULATION
        _toNearestBuildingPath = CalculateNearestBuildingPath();

    }

    private NavMeshPath CalculateNearestBuildingPath()
    {
        //primera posicion de edificio es la mas cercana
        NavMeshPath nearest = new NavMeshPath();

        if (_buildingArray.Count > 0 && NavMesh.CalculatePath(_myTransform.position, _buildingArray[0].transform.position, NavMesh.AllAreas, nearest))
        {
            float distanceToNearest =0;

            for (int i =0; i<nearest.corners.Length-1; i++)
            {
                distanceToNearest = distanceToNearest + Vector3.Magnitude(nearest.corners[i + 1] - nearest.corners[i]);
            }

            for (int i = 1; i < _buildingArray.Count; i++) //recorrido por todos los caminos de todos los edificios para encontrar el mas cercano
            {
                NavMeshPath current = new NavMeshPath();

                //se calcula el camino del edificio 
                if (NavMesh.CalculatePath(_myTransform.position, _buildingArray[i].transform.position, NavMesh.AllAreas, current))
                {
                    float distanceToCurrent = 0;
                    for (int j = 0; j < current.corners.Length - 1; j++)
                    {
                        distanceToCurrent = distanceToCurrent + Vector3.Magnitude(current.corners[j + 1] - current.corners[j]);
                    }

                    if (distanceToCurrent < distanceToNearest)
                    {
                        nearest = current;
                        distanceToNearest = distanceToCurrent;
                    }
                }
            }
        }
         
        

        return nearest;
    }

    private void Awake()
    {
        _toPlayerPath = new NavMeshPath();
        _myTransform = transform;
    }

}

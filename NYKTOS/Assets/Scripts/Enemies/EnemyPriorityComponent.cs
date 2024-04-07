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
    private List<GameObject> _buildingArray { get { return BuildingManager.Instance.buildingArray; } }  

    private Transform _myTransform;

    public GameObject _nearestPriorityObject { private set; get; }

    public GameObject _nearestBuildingObject { private set; get; }

    private NavMeshPath _toPlayerPath;
    public NavMeshPath toPlayerPath { get { return _toPlayerPath; } }

    private NavMeshPath _toNearestBuildingPath;
    public NavMeshPath toNearestBuildingPath { get { return _toNearestBuildingPath; } }

    private NavMeshPath _priorityPath;
    public NavMeshPath priorityPath { get { return _priorityPath; } }
    #endregion

    #region parameters
    [SerializeField]
    bool _usingDetection = true;
    [SerializeField]
    float _farDetectionRadius = 5f;

    [SerializeField]
    float _nearDetectionRadius = 2f;

    private bool _playerAggro = false;
    #endregion

    private void Update()
    {
        //PLAYER PATH CALCULATION
        NavMesh.CalculatePath(_myTransform.position, PlayerController.playerTransform.position, NavMesh.AllAreas, _toPlayerPath);

        //NEAREST BUILDING PATH CALCULATION
        _toNearestBuildingPath = CalculateNearestBuildingPath();

        if (_usingDetection)
        {
            CalculatePriority();
        }
    }

    private void CalculatePriority()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_myTransform.position, _farDetectionRadius);
        int num = 0;
        bool detected = false;
        List<HealthComponent> list = new List<HealthComponent>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<HealthComponent>() != null && colliders[i].gameObject != gameObject && !list.Contains(colliders[i].GetComponent<HealthComponent>()))
            {
                list.Add(colliders[i].GetComponent<HealthComponent>());
                num++;
            }
            if (!detected && colliders[i].GetComponent<PlayerController>() != null && PlayerStateMachine.playerState != PlayerState.Dead)
            {
                detected = true;
            }
        }

        Collider2D[] nearColliders = Physics2D.OverlapCircleAll(_myTransform.position, _nearDetectionRadius);
        bool playerNear = false;
        for (int i = 0; i < nearColliders.Length && !playerNear; i++)
        {
            playerNear = nearColliders[i].GetComponent<PlayerController>() != null && PlayerStateMachine.playerState != PlayerState.Dead;
        }

        if (playerNear)
        {
            _playerAggro = true;
            _nearestPriorityObject = PlayerController.playerTransform.gameObject;
            _priorityPath = _toPlayerPath;
        }
        else if (detected && _playerAggro)
        {
            _nearestPriorityObject = PlayerController.playerTransform.gameObject;
            _priorityPath = _toPlayerPath;
        }
        else if (detected && num == 1)
        {
            _playerAggro = true;
            _nearestPriorityObject = PlayerController.playerTransform.gameObject;
            _priorityPath = _toPlayerPath;
        }
        else
        {
            _playerAggro = false;
            _nearestPriorityObject = _nearestBuildingObject;
            _priorityPath = _toNearestBuildingPath;
        }

        //Debug.Log(_playerAggro);

        //debug
        for (int i = 0; i < _priorityPath.corners.Length - 1; i++)
        {
            Debug.DrawLine(_priorityPath.corners[i], _priorityPath.corners[i + 1], Color.red);

        }
    }

    private NavMeshPath CalculateNearestBuildingPath()
    {
        //primera posicion de edificio es la mas cercana
        NavMeshPath nearest = new NavMeshPath();


        if (_buildingArray.Count > 0 && NavMesh.CalculatePath(_myTransform.position, _buildingArray[0].transform.position, NavMesh.AllAreas, nearest))
        {
            GameObject nearestObject = _buildingArray[0];
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
                        nearestObject = _buildingArray[i];
                    }
                }
            }

            _nearestBuildingObject = nearestObject;
        }
         
        

        return nearest;
    }

    private void Awake()
    {
        _toPlayerPath = new NavMeshPath();
        _myTransform = transform;
    }

    private void OnDrawGizmos()
    {
        if (_usingDetection)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _nearDetectionRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _farDetectionRadius);
        }
        
    }

}

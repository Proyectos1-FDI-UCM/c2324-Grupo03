using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //  Codigo de Maria :p
    #region references
    private GameObject _target;
    [SerializeField] private NavMeshAgent agent;
    #endregion
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update() {

        agent.SetDestination(_target.transform.position);


    }
    //  fin codigo de Maria :)

}

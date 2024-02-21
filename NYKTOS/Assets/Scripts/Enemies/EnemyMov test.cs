using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private GameObject _target;
    [SerializeField]
    private NavMeshAgent agent;

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
}

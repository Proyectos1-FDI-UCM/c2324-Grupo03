using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //  Codigo de Maria :p

    private HealthComponent _healthComponent;
    private float nextActionTime = 0.0f;
    private float period = 2f;
    #region references
    private GameObject _target;
    [SerializeField] private NavMeshAgent agent;
    #endregion
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _target = GameObject.FindGameObjectWithTag("Player");
        nextActionTime = Time.time + period;
    }

    

    // Update is called once per frame
    void Update() {

        agent.SetDestination(_target.transform.position);
        if (Time.time > nextActionTime) {
            _healthComponent.Damage(2);
            nextActionTime = Time.time + period;
        }
    }
    //  fin codigo de Maria :)

}

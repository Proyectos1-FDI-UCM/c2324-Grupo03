using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //  Codigo de Maria :p

    #region parameters
    private float nextActionTime = 0.0f;
    private float period = 2f;

    //Codigo de Iker
    //Distancia minima para que no atraviese al jugador y prepare el ataque
    [SerializeField]
    private float _minDistanceToAttackPlayer = 1f;
    [SerializeField]
    private float _minDistanceToAttackAltar = 1.25f;
    //Fin Codigo de Iker
    #endregion

    #region references
    [SerializeField] 
    private NavMeshAgent agent;
    private Animator _animator;
    [SerializeField] private float nextAction = 0.0f;
    private float periodo = 0.7f;
    //Codigo de Iker
    private Transform _myTransform;
    private Transform _playerTransform;
    private GameObject _altarTutorial;
    private bool LejosPlayer = true;
    private bool LejosAltar = true;
    //Fin Codigo de Iker
    #endregion


    void Start() {
        _animator = GetComponent<Animator>();
        _myTransform = transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        nextAction = Time.time + periodo;

        // Codigo de Iker
        _altarTutorial = IAManager.Instance.altartutorial;
        // Fin Codigo de Iker
    }

    public void PlayerDetected(Transform transform)
    {
        _playerTransform = transform;
    }

    public void PlayerNotDetected(Transform transform)
    {
        _playerTransform = null;
    }

    // Update is called once per frame
    void Update() {

        //Codigo de Iker
        //Distancia del enemigo al altar
        float distanceEnemyAltar = Vector3.Distance(_myTransform.position, _altarTutorial.transform.position);

        //Distancia del enemigo al jugador
        float distanceEnemyPlayer;
        if (_playerTransform != null)
        {
            distanceEnemyPlayer = Vector3.Distance(_myTransform.position, _playerTransform.position);
        } else
        {
            distanceEnemyPlayer = 100.0f;
        }
        
        //Condiciones de prioridad
        if (_playerTransform != null && LejosPlayer && LejosAltar)
        {
            agent.SetDestination(_playerTransform.position);
        }
        else if (_playerTransform == null && LejosPlayer && LejosAltar)
        {
            agent.SetDestination(_altarTutorial.transform.position);
            //Nota: en caso de NullReferenceException, copiar el objeto IAManager de la escena de Iker y pegar en tu escena
        }

        //Condicion de parada
        if (distanceEnemyPlayer > _minDistanceToAttackPlayer)
        {
            LejosPlayer = true;
        } else
        {
            LejosPlayer = false;
        }

        if (distanceEnemyAltar > _minDistanceToAttackAltar)
        {
            LejosAltar = true;
        }
        else
        {
            LejosAltar = false;
        }

        if (!LejosPlayer && _playerTransform != null) agent.SetDestination(_myTransform.position);
        if (!LejosAltar) agent.SetDestination(_myTransform.position);
        //Fin Codigo de Iker

        Vector3 movementDirection = agent.velocity.normalized; //direccion de dnd va

        if (Time.time > nextAction) //en realidad cuando vea a Link
        {
            _animator.SetFloat("xAxis", movementDirection.x);
            _animator.SetFloat("yAxis", movementDirection.y);

            nextAction = Time.time + periodo;
        }
        

       
    }
}


    //  fin codigo de Maria :)



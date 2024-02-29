using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //  Codigo de Maria :p

    #region parameters
    private float nextActionTime = 0.0f;
    private float period = 2f;

    //Codigo de Iker
    //Distancia minima para perseguir al jugador
    private float _minDistance = 4.0f;
    //Distancia minima para que no atraviese al jugador y prepare el ataque
    private float _minDistanceToAttack = 1.0f;
    //Fin Codigo de Iker
    #endregion

    #region references
    [SerializeField] 
    private NavMeshAgent agent;
    private Transform _myTransform;
    private Animator _animator;
    [SerializeField] private float nextAction = 0.0f;
    private float periodo = 0.7f;
    //Codigo de Iker
    private GameObject _player;
    private GameObject _altarTutorial;
    Vector3 playerPosition;
    Vector3 altarPosition;
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
        _player = IAManager.Instance.player;
        _altarTutorial = IAManager.Instance.altartutorial;
        //GameObject specificAltar = IAManager.Instance.altar[0];
        // Fin Codigo de Iker
    }



    // Update is called once per frame
    void Update() {

        //Codigo de Iker
        //Toma el Vector3 del jugador y del altar a partir del GameManager
        if (altarPosition != null && playerPosition != null)
        {
            playerPosition = _player.transform.position;
            altarPosition = _altarTutorial.transform.position;
            //NOTA: NullReferenceException aquí, lee lo de abajo
        }
        else
        {
            Debug.Log("Copia el objeto IAManager de la escena Iker y pegalo en tu escena");
        }

        //Distancia del enemigo al jugador
        float distanceEnemyPlayer = Vector3.Distance(_myTransform.position, playerPosition);
        //Distancia del enemigo al altar, creo que es redundante
        float distanceEnemyAltar = Vector3.Distance(_myTransform.position, altarPosition);

        // Si esta lo suficientemente cerca del jugador, se acerca a el hasta que pueda atacarlo
        if (distanceEnemyPlayer < _minDistance && distanceEnemyPlayer > _minDistanceToAttack)
        {
            agent.SetDestination(playerPosition);

            
        } 
        //Si esta lo suficientemente lejos del jugador, tomara de prioridad el altar mas cercano
        else if (distanceEnemyPlayer > _minDistance)
        {
            agent.SetDestination(altarPosition);
        }
        // Si esta al lado del jugador se para para prepararse para atacar
        else if (distanceEnemyPlayer <= _minDistanceToAttack)
        {
            //SE QUEDA QUIETO, QUE LOCURA
            agent.SetDestination(transform.position);
        }
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



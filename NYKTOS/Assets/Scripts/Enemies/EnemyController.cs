using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    //  Codigo de Maria :p

    private HealthComponent _healthComponent;
    private float nextActionTime = 0.0f;
    private float period = 2f;
    #region references
    [SerializeField] 
    private NavMeshAgent agent;
    private Transform _myTransform;

    //Codigo de Iker
    Vector3 playerPosition;
    Vector3 altarPosition;
    //Declaracion de interfaces
    private IPlayerAttributes playerAttributes;
    private IAltarAttributes altarAttributes;
    //Declaracion de Scripts implicados en el uso de interfaces NO SE PUEDE APROVECHAR ESTO
    [SerializeField] 
    private PlayerController playerController;
    [SerializeField]
    private AltarComponent altarComponent;
    #endregion

    #region
    //Distancia minima para perseguir al jugador y distancia minima para que no atraviese al jugador y prepare el ataque
    private float _minDistance = 4.0f;
    private float _minDistanceToAttack = 1.0f;
    #endregion
    //Fin Codigo de Iker

    void Start() {
        _myTransform = transform;
        agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        nextActionTime = Time.time + period;

        //Codigo de Iker
        //Se busca el objecto que tiene el script que contiene el metodo para llamar al vector, se tiene que optimizar esta busqueda
        playerAttributes = FindObjectOfType<PlayerController>();
        altarAttributes = FindObjectOfType<AltarComponent>();
        /*
         * NO VALE 
         * playerAttributes = playerController;
         * altarAttributes = AltarComponent;
         * Ya que no deja poner el script en el inspector
         */
        //Fin Codigo de Iker
        
    }

    

    // Update is called once per frame
    void Update() {

        //Codigo de Iker
        //Toma el Vector3 de los atributos a partir de un metodo hecho en PlayerController y AltarComponent
        playerPosition = playerAttributes.GetPlayerPosition();
        altarPosition = altarAttributes.GetAltarPosition();

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

        if (Time.time > nextActionTime) {//para hacer daño
            _healthComponent.Damage(2);
            nextActionTime = Time.time + period;
        }
    }
    //  fin codigo de Maria :)

}

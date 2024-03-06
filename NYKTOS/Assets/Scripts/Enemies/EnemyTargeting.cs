using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    //IMPORTANTE: Poner estado de parada del enemigo cuando este a una distancia muy cercana al jugador con la maquina de estados

    //Guia de lo que necesita guille en su escena
    //Necesita poner el IAManager y poner el altar y el jugador
    //Importante el IAManager tiene que ser un objeto vacío creado desde cero, sino dará nullreferenceexceiption.
    //Ponerle este script al vespertilio con la maquina de estados y ponerle el transform del jugador (el de nexo no hace falta)


    #region references
    private IAManager _IAManager;
    private MoveToPlayerBehaviour _MoveToMyObjective;
    private Transform _ObjectiveTransform;
    

    private Transform _myTransform;
    [SerializeField]
    private Transform _playerTransform;
    private Transform _defenseTransform;
    private GameObject _altar;
    private Transform _altarTransform;
    [SerializeField]
    private Transform _nexusTransform;
    #endregion

    #region parameters
    private float DistanceToPlayer;
    private float DistanceToDefense;
    private float DistanceToAltar;
    private float DistanceToNexus;
    #endregion

    #region properties
    public enum TypeOfEnemy
    {
        Vespertilio, //Prioridad general, al que tengas mas cerca entre edificios y jugador
        Vitae, //Prioridad jugador al estar a una distancia determinada, sino atacará a los edificios
        Poly, //Prioridad jugador de forma alejada, Estará a una distancia considerable del jugador y si se acerca mucho se irá alejando poco a poco
        Celer, //Prioridad jugador si o si
        AraneaMadre, //Prioridad ninguna (paseo aleatorio) (Se puede hacer que persiga a algo y cambiar de objetivo antes de que llegue al mismo)
        AraneaHija, //Prioridad edificios sí o sí
        Uge //Prioridad jugador sí o sí
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Hay que ponerle este script al prefab del vespertilio, el move to player behauviour es un estado que se encuentra dentro del enemigo
        _MoveToMyObjective = GetComponentInChildren<MoveToPlayerBehaviour>();
        //Hacer el _targetTransform publico implica que pueda interactuar el enemyDetects con el moveToPlayerBehauviour
        _ObjectiveTransform = _MoveToMyObjective._targetTransform.transform;
        _myTransform = transform;
        _altar = IAManager.Instance.altartutorial;
        
        _playerTransform = FindObjectOfType<PlayerController>().transform;
        _altarTransform = _altar.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        PreferedAltar();
        PreferedDefense();

        DistanceToPlayer = Vector2.Distance(_myTransform.position, _playerTransform.position);
        //DistanceToDefense = Vector2.Distance(_myTransform.position, _defenseTransform.position);
        DistanceToAltar = Vector2.Distance(_myTransform.position, _altar.transform.position);
        //DistanceToNexus = Vector2.Distance(_myTransform.position, _nexusTransform.position);

        //Hay que encontrar una manera de poder distrubuir la prioridad de manera mas efectiva
        //Prioridad general para Vespertilio, hacer una condición para detectar el tipo de enemigo y ejecutar el metodo correspondiente
        VespertilioPriorityTutorialAltar(DistanceToPlayer,DistanceToAltar);
    }

    private void VespertilioPriorityTutorialAltar(float DistanceToPlayer, float DistanceToAltar)
    {
        if (DistanceToPlayer < DistanceToAltar)
        {
            _ObjectiveTransform = _playerTransform;
            _MoveToMyObjective._targetTransform = _ObjectiveTransform;
            _MoveToMyObjective.PerformBehaviour();
        }
        else
        {
            _ObjectiveTransform = _altarTransform;
            _MoveToMyObjective._targetTransform = _ObjectiveTransform;
            _MoveToMyObjective.PerformBehaviour();
        }
    }

    private void VespertilioPriorityTutorial(float DistanceToPlayer, float DistanceToDefense, float DistanceToAltar)
    {
        if (DistanceToPlayer < DistanceToDefense && DistanceToPlayer < DistanceToAltar)
        {
            _ObjectiveTransform = _playerTransform;
        }
        else if (DistanceToDefense < DistanceToPlayer && DistanceToDefense < DistanceToAltar)
        {
            _ObjectiveTransform = _defenseTransform;
        }
        else
        {
            _ObjectiveTransform = _altarTransform;
        }
    }


    private void VespertilioPriority(float DistanceToPlayer, float DistanceToDefense, float DistanceToAltar, float DistanceToNexus)
    {
        if (DistanceToPlayer < DistanceToDefense && DistanceToPlayer < DistanceToAltar && DistanceToPlayer < DistanceToNexus)
        {
            _ObjectiveTransform = _playerTransform;
        }
        else if (DistanceToDefense < DistanceToPlayer && DistanceToDefense < DistanceToAltar && DistanceToDefense < DistanceToNexus)
        {
            _ObjectiveTransform = _defenseTransform;
        }
        else if (DistanceToAltar < DistanceToPlayer && DistanceToAltar < DistanceToDefense && DistanceToAltar < DistanceToNexus)
        {
            _ObjectiveTransform = _altarTransform;
        }
        else if (DistanceToNexus < DistanceToPlayer && DistanceToNexus < DistanceToDefense && DistanceToNexus < DistanceToAltar)
        {
            _ObjectiveTransform = _nexusTransform;
        }
    }

    //Hacer los otros tipos de prioridad
    private void VitaePriority() { }
    private void PolyPriority() { }
    private void CelerPriority() { }
    private void AraneaMadrePriority() { }
    private void AraneaHijaPriority() { }
    private void UgePriority() { }

    private Transform PreferedDefense()
    {
        //Acceder a todas las defensas con un array y decidir la más cercana para que se tome en cuenta en el update
        return _defenseTransform;
    }

    private Transform PreferedAltar()
    {
        //Acceder a todos los altares con un array y decidir la más cercana para que se tome en cuenta en el update
        _altarTransform = _altar.transform;
        return _altarTransform;
    }
}

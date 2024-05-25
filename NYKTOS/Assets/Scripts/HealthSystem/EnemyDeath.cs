
using UnityEngine;

/// <summary>
/// Script que controla la muerte de los enemigos
/// </summary>

[System.Serializable]
public class EnemyDeath : MonoBehaviour, IDeath
{
    private bool _isDead = false; // bool para que la StateMachine sepa cuando el enemigo ha muerto
    public bool isDead { get { return _isDead; } } // acceso publico al bool _isDead
    
    [SerializeField]
    private SpawnLimit _spawnLimit;
    [SerializeField]
    private VoidEmitter _enemyDeathEmitter;

    public void Death() //Cambia el estado del enemigo a muerto
    {
        _isDead = true;
        if(TryGetComponent<CrystalBag>(out CrystalBag enemyBag)) //Drop de cristales
        {
            enemyBag.InstantiateCrystal(transform.position);
        }
        _spawnLimit?.RemoveConcurrentEnemy(); //Deja de contar a este enemigo para que puedan spawnear más
    }

    void Start()
    {
        _enemyDeathEmitter.Perform.AddListener(Death);
    }

    void OnDestroy() 
    {
        _enemyDeathEmitter.Perform.RemoveListener(Death);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo de Iker
public class TurretTargetingComponent : MonoBehaviour
{
    private Transform _myTransform;
    private Transform _enemyTransform;
    private bool _enemyDetected = false;
    [SerializeField]
    private float RotationVelocity = 5f;
    private Vector3 directionToEnemy;
    private List<Transform> _detectedEnemies = new List<Transform>();

    void Start()
    {
        _myTransform = transform;
    }

    public Vector3 DirectionToEnemy()
    {
        return directionToEnemy;
    }

    public Transform EnemyTransform()
    {
        return _enemyTransform;
    }

    /// <summary>
    /// Si detecta un enemigo, se creara un vector de direccion hacia el enemigo y rotara en función del angulo que forma ese vector,
    /// haciendo que la torreta siempre apunte a un enemigo proximo
    /// </summary>
    void Update()
    {
        if (_enemyTransform != null)
        {
            directionToEnemy = (_enemyTransform.position - _myTransform.position).normalized;

            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

            _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, targetRotation, RotationVelocity);
        }
    }

    /// <summary>
    /// Si el collider circular (rango de visión de la torreta) obtiene un componente que proviene de un enemigo, añade el transform a su lista de enemigos detectados.
    /// En caso de no haber enemigos detectados, su transform se quedará quieto en función del último enemigo al que ha atacado.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            _detectedEnemies.Add(enemy.transform);
            if (_enemyTransform == null)
            {
                _enemyTransform = enemy.transform;
            }
        }
    }

    /// <summary>
    /// Si un enemigo sale de su collider circular (rango de visión de la torreta), su transform se removerá de la lista de enemigos.
    /// Si una torreta tiene mas de un transform de enemigos, se tomará de prioridad siempre al primero de la lista.
    /// En caso de que muera un enemigo que estuvo en su primera posición de la lista, se pasará al siguiente.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyPriorityComponent enemy))
        {
            _detectedEnemies.Remove(enemy.transform);

            if (_enemyTransform == enemy.transform)
            {
                if (_detectedEnemies.Count > 0)
                {
                    _enemyTransform = _detectedEnemies[0];
                }
                else
                {
                    _enemyTransform = null;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class RBMovement : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;
    #endregion

    #region properties
    private float xAxis = 0f;
    private float yAxis = 0f;
    /// <summary>
    /// Velocidad actual del jugador. Es la que resulta de los calculos, y siempre debe volver al valor de movementSpeed.
    /// </summary>
    private float currentSpeed;


    private Vector2 privateMovementDirection;
    public Vector2 movementDirection //Valor publico de lectura de movementDirection
    {
        get { return privateMovementDirection; }
    }
    #endregion

    #region parameters
    /// <summary>
    /// Velocidad seteada de la entidad. Nunca cambia.
    /// </summary>
    [SerializeField] private float _movementSpeed = 5f;
    /// <summary>
    /// Variable de la velocidad base de la entidad. Es constante, es decir, aunque la velocidad actual sea distinta, movementSpeed siempre sera igual a la velocidad base.
    /// </summary>
    public float movementSpeed { get { return _movementSpeed; } }

    [SerializeField] private float _knockBackSpeed = 10f;
    public float knockBackTime
    {
        get { return _knockBackTime; }
    }
    [SerializeField] private float _knockBackTime = 0.5f;
    #endregion

    /// <summary>
    ///Ajustar el movimiento en xAxis (-1,0,1)
    /// </summary>
    /// <param name="num"></param>
    public void xAxisMovement(float num) 
    {
        xAxis = num;
        Move();
    }
    /// <summary>
    ///Ajustar el movimiento en yAxis (-1,0,1)
    /// </summary>
    /// <param name="num"></param>
    public void yAxisMovement(float num)
    {
        yAxis = num;
        Move();
    }

    public void Move() 
    {
        privateMovementDirection = new Vector2(xAxis, yAxis).normalized;
        _myRigidbody.velocity = privateMovementDirection * currentSpeed;
    }

    public void TeleportTo(Vector2 position) //Pone la posicion del jugador en las coordenadas que se le pasan
    {
        _myTransform.position = position;
    }

    void Awake()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = _movementSpeed;
    }
    #region cambios de velocidad
    public void ChangeSpeed(float newSpeed, float time)
    {
        currentSpeed = newSpeed;

        Invoke(nameof(ResetSpeed), time);
    }

    private void ResetSpeed()
    {
        currentSpeed = _movementSpeed;
    }
    #endregion

    public void StopVelocity()
    {
        _myRigidbody.velocity = Vector2.zero;
    }
    public void Knockback(Vector2 pushPosition)
    {
        Vector2 knockbackDirection = (new Vector2 (_myTransform.position.x, _myTransform.position.y) - pushPosition).normalized;

        _myRigidbody.velocity = knockbackDirection * _knockBackSpeed;

        Invoke("StopVelocity", _knockBackTime);
    }

    #region fixes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null)
        {
            _myRigidbody.velocity = privateMovementDirection * currentSpeed;
        }
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMovement : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;
    private PlayerStateMachine _playerState;
    #endregion

    #region properties
    private float xAxis = 0f;
    private float yAxis = 0f;    
    
    private Vector2 privateMovementDirection;
    public Vector2 movementDirection //Valor publico de lectura de movementDirection
    {
        get { return privateMovementDirection; }
    }
    #endregion

    #region parameters
    public float currentMovementSpeed = 1f;
    public float previousSpeed;

    [SerializeField] private float knockBackSpeed = 10f;
    #endregion

    public void ResetSpeed()
    {
        currentMovementSpeed = previousSpeed;
    }

    

    public void xAxisMovement(float num) //Ajustar el movimiento en xAxis (-1,0,1)
    {
        xAxis = num;
        Move();
    }

    public void yAxisMovement(float num) //Ajustar el movimiento en yAxis (-1,0,1)
    {
        yAxis = num;
        Move();
    }

    public void Move() 
    {
        Debug.Log("me muevoo");
        privateMovementDirection = new Vector2(xAxis, yAxis).normalized;
        _myRigidbody.velocity = privateMovementDirection * currentMovementSpeed;
    }

    public void StopMoving()
    {
        previousSpeed = currentMovementSpeed;
        currentMovementSpeed = 0;
        Move();
    }

    public void TeleportTo(Vector2 position) //Pone la posicion del jugador en las coordenadas que se le pasan
    {
        _myTransform.position = position;
    }

    void Awake()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerState = GetComponent<PlayerStateMachine>();
    }

    public void Knockback(Vector2 pushPosition)
    {
        Vector2 knockbackDirection = (new Vector2 (_myTransform.position.x, _myTransform.position.y) - pushPosition).normalized;

        _myRigidbody.velocity = knockbackDirection * knockBackSpeed;
    }

    #region fixes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null)
        {
            _myRigidbody.velocity = privateMovementDirection * currentMovementSpeed;
        }
    }
    #endregion

}

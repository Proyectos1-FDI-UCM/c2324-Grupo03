using System.Collections;
using System.Collections.Generic;
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
    #endregion

    #region parameters
    public float movementSpeed = 1f;
    public Vector2 movementDirection //Valor publico de lectura de movementDirection
    {
        get { return privateMovementDirection; }
    }
    private Vector2 privateMovementDirection;
    #endregion

    public void xAxisMovement(float num) //Ajustar el movimiento en xAxis (-1,0,1)
    {
        xAxis = num;
        privateMovementDirection = new Vector2(xAxis, yAxis).normalized;
        _myRigidbody.velocity = privateMovementDirection * movementSpeed;
    }

    public void yAxisMovement(float num) //Ajustar el movimiento en yAxis (-1,0,1)
    {
        yAxis = num;
        privateMovementDirection = new Vector2(xAxis, yAxis).normalized;
        _myRigidbody.velocity = privateMovementDirection * movementSpeed;
    }

    public void MoveTo() 
    {

    }

    public void TeleportTo(Vector2 position) //Pone la posicion del jugador en las coordenadas que se le pasan
    {
        _myTransform.position = position;
    }

    void Awake()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
    #region fixes
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null)
        {
            _myRigidbody.velocity = privateMovementDirection * movementSpeed;
        }
    }
    #endregion

}

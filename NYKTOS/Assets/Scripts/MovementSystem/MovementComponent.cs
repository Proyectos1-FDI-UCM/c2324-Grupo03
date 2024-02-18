using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// !!! ESTO SON SOLO PRUEBITAS QUE HE ESTADO HACIENDO - Andrea


public class MovementComponent : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private Rigidbody2D _myRigidbody;
    #endregion

    #region properties
    private Vector2 movementDirection; //Direccion del movimiento normalizada, dandose en las ocho direcciones
    //private float xAxis = 0f;
    //private float yAxis = 0f;
    #endregion

    #region parameters
    [SerializeField]
    private float movementSpeed = 1f;
    public Vector2 pMovementDirection //Valor publico de lectura de movementDirection
    {
        get { return movementDirection; }
    }

    [SerializeField]
    private float blinkRange = 5f;
    #endregion

    public void SetDirection(Vector2 dir)
    {
        movementDirection = dir;
    }

    public void TeleportTo(Vector2 position) //Pone la posicion del jugador en las coordenadas que se le pasan
    {
        _myTransform.position = position;
    }

    public void Blink() //Teletransporta al objeto en la direccion de su movimiento con el rango definido en BlinkRange
    {
        TeleportTo(new Vector2(_myTransform.position.x, _myTransform.position.y)
            + movementDirection * blinkRange);
    }

    void Awake()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _myRigidbody.MovePosition(new Vector2(_myTransform.position.x, _myTransform.position.y) 
            + movementDirection* movementSpeed*Time.fixedDeltaTime);
    }

}

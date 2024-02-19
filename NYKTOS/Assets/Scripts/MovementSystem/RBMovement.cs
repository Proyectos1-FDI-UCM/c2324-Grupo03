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
        get { return _myRigidbody.velocity.normalized; }
    }

    [SerializeField]
    private float blinkRange = 2f;
    #endregion

    public void xAxisMovement(float num) //Ajustar el movimiento en xAxis (-1,0,1)
    {
        xAxis = num;
        _myRigidbody.velocity = new Vector2(xAxis, yAxis).normalized*movementSpeed;
    }

    public void yAxisMovement(float num) //Ajustar el movimiento en yAxis (-1,0,1)
    {
        yAxis = num;
        _myRigidbody.velocity = new Vector2(xAxis, yAxis).normalized * movementSpeed;
    }

    public void MoveTo() 
    {

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

    private void Update()
    {
        print(movementDirection);
    }

}

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

    void Awake()
    {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

}

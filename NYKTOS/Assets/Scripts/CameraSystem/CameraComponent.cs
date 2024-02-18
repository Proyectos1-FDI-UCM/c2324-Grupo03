//Iker
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    [SerializeField]
    private Transform _player;
    #endregion

    #region properties
    private Vector3 playerPosition;
    #endregion

    #region parameters

    //Como esta variable vuelve a su cantidad original en la condicion de la distancia con el jugador, por eso no esta serializado.
    private float followBlinkTime = 1f;

    //Esta variables no estan serializadas porque su modificación provocaría un lerp continuo o que no haga lerp nunca.
    private float distanceToPlayer = 0.25f;
    private float distanceToCamera = 0.25f;

    //Esta variable sirve para medir el tiempo que queramos que duré el lerp (modificar para que quedé bien con el BlinkRange que se le ponga al player)
    //A menor tiempo, mayor duración del lerp
    //A mayor tiempo, menor duración del lerp
    [SerializeField]
    private float incrementalTime = 5f;

    //Para calcular la distancia entre el jugador y camara
    private float Distance;

    //Zona de Cronometro
    private bool changetoBlink = false;
    private float Timer = 0.0f;
    #endregion


    void Start()
    {
        _myTransform = transform;
    }


    void FixedUpdate()
    {
        //Sacando la distancia entre el jugador y cámara
        Distance = Vector2.Distance(_myTransform.position, _player.position);

        //Si la distancia es menor que la distanceToCamera, el followBlinkTime se resetea
        if (Distance < distanceToCamera)
        {
            //Reseteo del tiempo del lerp
            followBlinkTime = 1f;
            
        }

        //Mientras este en estado de lerp, un cronometro llevará la cuenta de lo que tardé en volver al movimiento sin lerp de cámara
        if (changetoBlink)
        {
            Timer += Time.deltaTime;
            Debug.Log(Timer);  
        }

        //Si la distancia es mayor que 0.5, la cámara no esta "fija" en el jugador por hacer el blink, producimos el lerp
        if (Distance < distanceToPlayer)
        {
            Movement();
            changetoBlink = false;
            Timer = 0.0f;
        }
        else
        {
            MovementBlink();
            //Si el tiempo del lerp aumenta, la cámara se acercará mas rapido al jugador, hasta llegar a una Distance < 0.5
            followBlinkTime += incrementalTime /10;
            changetoBlink = true;
        }
    }

    //Movimiento fijo de la cámara sin hacer blink en el personaje
    void Movement()
    {
        playerPosition = _player.position;
        _myTransform.position = new Vector3(playerPosition.x, playerPosition.y, _myTransform.position.z);
    }

    //Movimiento de lerp ajustado para cuando haya blink en el personaje por haber una Distance > 0.5
    void MovementBlink()
    {    
        playerPosition = _player.position;

        /*
        float distanceX = Mathf.Abs(_myTransform.position.x - playerPosition.x);
        float distanceY = Mathf.Abs(_myTransform.position.y - playerPosition.y);

        float minX = playerPosition.x - distanceToPlayer - distanceX;
        float minY = playerPosition.y - distanceToPlayer - distanceY;
        float maxX = playerPosition.x + distanceToPlayer + distanceX;
        float maxY = playerPosition.y + distanceToPlayer + distanceY;
        */

        float xPosition = Mathf.Lerp(_myTransform.position.x, playerPosition.x, followBlinkTime * Time.deltaTime);
        //float xPositionSmooth = Mathf.Clamp(xPosition, minX, maxX);
        float yPosition = Mathf.Lerp(_myTransform.position.y, playerPosition.y, followBlinkTime * Time.deltaTime);
        //float yPositionSmooth = Mathf.Clamp(yPosition, minY, maxY);
        float zPosition = _myTransform.position.z;
        _myTransform.position = new Vector3(xPosition, yPosition, zPosition);
        //_myTransform.position = new Vector3(xPositionSmooth, yPositionSmooth, zPosition);

        
    }
}

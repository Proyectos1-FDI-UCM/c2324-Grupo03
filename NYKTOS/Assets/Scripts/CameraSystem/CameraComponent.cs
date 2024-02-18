//Iker
using System.Collections;
using System.Collections.Generic;
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
    private float followBlinkTime = 5f;

    //Esta variable no esta serializada porque su modificaci�n provocar�a un lerp continuo o que no haga lerp nunca.
    private float distanceToPlayer = 0.5f;

    //Esta variable sirve para medir el tiempo que queramos que dur� el lerp (modificar para que qued� bien con el BlinkRange que se le ponga al player)
    //A menor tiempo, mayor duraci�n del lerp
    //A mayor tiempo, menor duraci�n del lerp
    [SerializeField]
    private float incrementalTime = 5f;
    
    #endregion


    void Start()
    {
        _myTransform = transform;
    }


    void LateUpdate()
    {
        //Sacando la distancia entre el jugador y c�mara
        float Distance = Vector2.Distance(_myTransform.position, _player.position);

        //Si la distancia es mayor que 0.5, la c�mara no esta "fija" en el jugador por hacer el blink, producimos el lerp
        if (Distance < distanceToPlayer)
        {
            Movement();
            //Reseteo del tiempo del lerp
            followBlinkTime = 5f;
        } else
        {
            MovementBlink();
            //Si el tiempo del lerp aumenta, la c�mara se acercar� mas rapido al jugador, hasta llegar a una Distance < 0.5
            followBlinkTime += incrementalTime * Time.fixedDeltaTime;
        }
    }

    //Movimiento fijo de la c�mara sin hacer blink en el personaje
    void Movement()
    {
        playerPosition = _player.position;
        _myTransform.position = new Vector3(playerPosition.x, playerPosition.y, _myTransform.position.z);
    }

    //Movimiento de lerp ajustado para cuando haya blink en el personaje por haber una Distance > 0.5
    void MovementBlink()
    {    
        playerPosition = _player.position;
        float xPosition = Mathf.Lerp(_myTransform.position.x, playerPosition.x, followBlinkTime * Time.deltaTime);
        float yPosition = Mathf.Lerp(_myTransform.position.y, playerPosition.y, followBlinkTime * Time.deltaTime);
        float zPosition = _myTransform.position.z;
        _myTransform.position = new Vector3(xPosition, yPosition, zPosition);
    }
}

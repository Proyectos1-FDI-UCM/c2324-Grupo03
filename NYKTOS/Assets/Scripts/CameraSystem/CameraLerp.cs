using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraLerp : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private Transform _myTransform;

    #region properties
    private Vector3 playerPosition;
    #endregion

    #region parameters
    //Como esta variable vuelve a su cantidad original en la condicion de la distancia con el jugador, por eso no esta serializado.
    private float followBlinkTime = 10f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //playerPosition = _player.position;

        /*
        float distanceX = Mathf.Abs(_myTransform.position.x - playerPosition.x);
        float distanceY = Mathf.Abs(_myTransform.position.y - playerPosition.y);
        */

        /*
        float minX = playerPosition.x - distanceX;
        float minY = playerPosition.y - distanceY;
        float maxX = playerPosition.x + distanceX;
        float maxY = playerPosition.y + distanceY;
        */

        /*
        float minX = playerPosition.x - _myTransform.position.x;
        float maxX = playerPosition.x + _myTransform.position.x;
        float minY = playerPosition.y - _myTransform.position.y;
        float maxY = playerPosition.y + _myTransform.position.y;

        float xPosition = Mathf.Lerp(_myTransform.position.x, playerPosition.x, followBlinkTime * Time.deltaTime);
        float xPositionSmooth = Mathf.Clamp(xPosition, 0, maxX);
        float yPosition = Mathf.Lerp(_myTransform.position.y, playerPosition.y, followBlinkTime * Time.deltaTime);
        float yPositionSmooth = Mathf.Clamp(yPosition, 0, maxY);
        float zPosition = _myTransform.position.z;
        //_myTransform.position = new Vector3(xPosition, yPosition, zPosition);
        _myTransform.position = new Vector3(xPositionSmooth, yPositionSmooth, zPosition);
        */

        Vector2 Position = Vector2.Lerp(_myTransform.position, _player.position, followBlinkTime * Time.deltaTime);
        _myTransform.position = new Vector3(Position.x, Position.y, _myTransform.position.z);

    }
}

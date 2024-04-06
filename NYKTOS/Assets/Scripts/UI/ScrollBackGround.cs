using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed = 5f;
    private float _distanceToScroll;
    private Transform _myTransform;
    private Vector3 _previousTransform;

    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _previousTransform = _myTransform.position;
        _distanceToScroll = GetComponent<BoxCollider2D>().size.x/2;
    }


    void Update()
    {
        _myTransform.position -= new Vector3(_scrollSpeed * Time.deltaTime, 0);
        if (_myTransform.position.x < _previousTransform.x - _distanceToScroll) 
        {
            _myTransform.position = _previousTransform;
            Debug.Log(_myTransform.position.x);
        }
    }
}

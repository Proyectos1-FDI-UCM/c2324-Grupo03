using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{

    //clock variables

    private bool _timerOn = false;
    private float _currentTime;

    private RectTransform _clockTransform;


    // Start is called before the first frame update
    void Start()
    {
        _clockTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime >=0)
        {
            
        }
        if ( _timerOn)
        {
            ChangeTime();
        }
    }

    private void ChangeTime()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime < 0)
        {
            _timerOn = false;
            _clockTransform.rotation = Quaternion.identity;
        }
    }

    public void ActivateTimer(float maxTime)
    {
        _currentTime = maxTime;
        _timerOn = true;
        
    }
   



}

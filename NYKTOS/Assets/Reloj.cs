using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{

    //clock variables

    private bool _timerOn = false;
    private float _currentTime;
    float angle;
    float timeVelocity;

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
        angle = angle -timeVelocity * Time.deltaTime;
        _currentTime -= Time.deltaTime;
        _clockTransform.rotation = Quaternion.Euler (0,0,angle);
        

        if(_currentTime < 0)
        {
            _timerOn = false;
            angle = 135;
            
        }
    }

    public void ActivateTimer(float maxTime)
    {
        _currentTime = maxTime;
        _timerOn = true;
        angle = 45;
        timeVelocity = (270 / maxTime);
        
    }

    public void ResetTimer()
    {
        angle = 90;
        _timerOn = false;
        _clockTransform.rotation = Quaternion.Euler(0, 0, angle);
    }




}

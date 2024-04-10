using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{
    [SerializeField]
    private Image _clockHand;

    //clock variables

    private bool _timerOn = false;
    private float _currentTime;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if( _timerOn)
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
        }
    }

    public void ActivateTimer(float maxTime)
    {
        _currentTime = maxTime;
        _timerOn = true;
        
    }


}

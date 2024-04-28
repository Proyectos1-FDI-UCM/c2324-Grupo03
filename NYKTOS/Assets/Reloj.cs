using UnityEngine;

public class Reloj : MonoBehaviour
{
    [SerializeField]
    private FloatEmitter _timerStart;
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
        _timerStart.Perform.AddListener(ActivateTimer);

        ResetTimer();
    }

    void Update()
    {
        if (_currentTime >=0)
        {
            // Aqui falta algo
        }
        if ( _timerOn)
        {
            ChangeTime();
        }
    }

    void OnDestroy()
    {
        _timerStart.Perform.RemoveAllListeners();
    }

    private void ChangeTime()
    {
        angle = angle - timeVelocity * Time.deltaTime;
        _currentTime -= Time.deltaTime;
        _clockTransform.rotation = Quaternion.Euler (0,0,angle);
        

        if(_currentTime < 0)
        {
            _timerOn = false;
            ResetTimer();
        }
    }

    public void ActivateTimer(float maxTime)
    {
        Debug.Log($"[RELOJ] Activado ");

        _currentTime = maxTime;
        _timerOn = true;
        angle = 45;
        timeVelocity = 270 / maxTime;
        
    }

    public void ResetTimer()
    {
        angle = 90;
        _timerOn = false;
        _clockTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

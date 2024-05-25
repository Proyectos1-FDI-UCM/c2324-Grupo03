using UnityEngine;

/// <summary>
/// Script que controla el reloj del HUD.
/// </summary>
/// 
public class Reloj : MonoBehaviour
{
    [SerializeField]
    private FloatEmitter _timerStart;
    
    //Clock variables
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
        if ( _timerOn)
        {
            ChangeTime();
        }
    }

    void OnDestroy()
    {
        _timerStart.Perform.RemoveAllListeners();
    }

    private void ChangeTime() //Actualiza el ángulo y el tiempo restante del temporizador, y resetea el temporizador cuando el tiempo llega a cero
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

    public void ActivateTimer(float maxTime) //Activa el reloj con un tiempo máximo específico (que se le pasa desde un evento).
    {
        
        _currentTime = maxTime;
        _timerOn = true;
        angle = 45;
        timeVelocity = 270 / maxTime; //Calcula a la velocidad que tiene que ir el reloj para terminar su vuelta en el tiempo especificado.
        
    }


    public void ResetTimer() 
    {
        angle = 90;
        _timerOn = false;
        _clockTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEditor;
using System.Security.Cryptography;
using System;
using UnityEngine.WSA;
using Unity.VisualScripting;

//Codigo de Iker
public class ControlCinemachine : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _firstCinematicEmitter;
    public CinemachineVirtualCamera _cinematicCamera;
    public PlayableDirector _timeline;
    private static bool _oneTimeCinematic = false;
    public static bool OneTimeCinematic 
    { 
        get { return _oneTimeCinematic; }
        set { _oneTimeCinematic = value;}
    }
    private bool _startTimer = false;
    
    [SerializeField]
    private float _waitToStart = 2f;
    private float _animationDuration = 60f;

    [SerializeField]
    private GameObject _uiGameplay;

    [SerializeField] private UnityEvent onCinematicStart = new UnityEvent();
    [SerializeField] private UnityEvent onCinematicFinish = new UnityEvent();

    /// <summary>
    ///Mantenemos activo la interfaz 
    ///Mantenemos la camara de la cinematica desactivada
    ///Paramos la linea de tiempo
    ///Estos tres parametros asi hasta que la cinematica se active
    ///
    ///Añadimos un Listener para que se active el metodo de la cinematica cuando el estado del juego sea el de día
    /// </summary>
    void Start()
    {
        _uiGameplay.SetActive(true);
        _cinematicCamera.enabled = false;
        _timeline.Stop();
        _firstCinematicEmitter.Perform.AddListener(FirstCinematicOn);
    }
    

    void Update()
    {
        if(_oneTimeCinematic)
        {
            ///Si el tiempo de espera para reproducir la cinematica ha empezado empieza un contador regresivo para que al llegar al limite se reproduzca la cinematica
            if (_startTimer)
            {
                _waitToStart -= Time.deltaTime;
            }

            ///Se empieza la cinematica cuando el tiempo de espera sea 0
            if (_waitToStart <= 0)
            {
                StartCinematic();
            }

            ///Si se ha pasado el tiempo de la cinematica siendo mayor que el tiempo de la animación se desactiva la perspectiva de la cinematica y se deja de contar el tiempo (esto deberia ir en la anterior condición)
            if (_timeline.time >= _animationDuration)
            {
                FirstCinematicOff();
                _startTimer = false;
            }
        }
    }

    void OnDestroy()
    {
        _firstCinematicEmitter.Perform.RemoveListener(FirstCinematicOn);
    }
    /// <summary>
    /// Cuando empieza la cinematica, comprobamos que solo podemos reproducirla una vez
    /// El tiempo de espera para reproducir la cinematica debe ser 0
    /// Si estas dos condiciones son ciertas: Se desactiva la UI para ver la cinematica, comienza la timeline de la cinematica y se cambia a la camara de la cinematica.
    /// </summary>

    private void StartCinematic()
    {
        if (_oneTimeCinematic && _waitToStart <= 0)
        {
            _uiGameplay.SetActive(false);
            _timeline.Play();
            _cinematicCamera.enabled = true;
        }
    }
    /// <summary>
    /// Comprobamos que solo se va a producir la cinematica una vez
    /// Esto se activa en el con un listener en el start cuando empieza el estado de juego, por eso es necesario un booleano que se ponga a false cuando ya se reprodució la cinematica
    /// Avisamos por un evento que la cinematica ha empezado
    /// </summary>

    public void FirstCinematicOn()
    {
        if (_oneTimeCinematic)
        {
            _startTimer = true;
            Debug.Log("[CINEMATICS CONTROLLER] DEBUG");
            onCinematicStart?.Invoke();
        }
    }

    /// <summary>
    ///Volvemos a activar la interfaz
    ///Ponemos a false el booleano de la cinematica para que ya no vuelva a reproducirse nunca mas
    ///Desactivamos la camara de la cinematica
    ///Se para la linea de tiempo para que no este siempre reproduciendose
    ///Se desactiva el objeto de la cinematica para volver a la perspectiva del jugador
    ///Avisamos por evento que la cinematica ha terminado
    /// </summary>
    private void FirstCinematicOff()
    {
        _uiGameplay.SetActive(true);
        _oneTimeCinematic = false;
        _cinematicCamera.enabled = false;
        _timeline.Stop();

        gameObject.SetActive(false);
        onCinematicFinish?.Invoke();
    }
}

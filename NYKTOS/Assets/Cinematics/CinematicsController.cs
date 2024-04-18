using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class ControlCinemachine : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _firstCinematicEmitter;
    public CinemachineVirtualCamera _cinematicCamera;
    public PlayableDirector _timeline;
    private static bool _oneTimeCinematic = true;
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
            if (_startTimer)
            {
                _waitToStart -= Time.deltaTime;
            }

            if (_waitToStart <= 0)
            {
                StartCinematic();
            }

            if (_timeline.time >= _animationDuration)
            {
                FirstCinematicOff();
                _startTimer = false;
            }
        }
    }

    private void StartCinematic()
    {
        if (_oneTimeCinematic && _waitToStart <= 0)
        {
            _uiGameplay.SetActive(false);
            _timeline.Play();
            _cinematicCamera.enabled = true;
        }
    }

    public void FirstCinematicOn()
    {
        _startTimer = true;
    }

    private void FirstCinematicOff()
    {
        _uiGameplay.SetActive(true);
        _oneTimeCinematic = false;
        _cinematicCamera.enabled = false;
        _timeline.Stop();

        gameObject.SetActive(false);
    }
}

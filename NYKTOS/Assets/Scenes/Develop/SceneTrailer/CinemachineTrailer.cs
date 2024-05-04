using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class CinemachineTrailer : MonoBehaviour
{
    public CinemachineVirtualCamera _cinematicCamera;
    public PlayableDirector _timeline;
    private static bool _oneTimeCinematic = true;
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
    }

    void Update()
    {
        if (_oneTimeCinematic)
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
            _uiGameplay.SetActive(true);
            _timeline.Play();
            _cinematicCamera.enabled = true;
        }
    }

    public void FirstCinematicOn()
    {
        if (_oneTimeCinematic)
        {
            _startTimer = true;
            Debug.Log("[CINEMATICS CONTROLLER] DEBUG");
        }
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

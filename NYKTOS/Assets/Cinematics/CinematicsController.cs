using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class ControlCinemachine : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter FirstCinematicEmitter;
    public CinemachineVirtualCamera cinematicCamera;
    public PlayableDirector timeline;
    private bool oneTimeCinematic = true;
    private bool startTimer = false;
    [SerializeField]
    private float waitToStart = 2f;
    private float AnimationDuration = 45f;
    void Start()
    {
        cinematicCamera.enabled = false;
        timeline.Stop();
        FirstCinematicEmitter.Perform.AddListener(FirstCinematicOn);
    }

    void Update()
    {
        if (startTimer && oneTimeCinematic)
        {
            waitToStart -= Time.deltaTime;
        }

        if (waitToStart <= 0 && oneTimeCinematic)
        {
            StartCinematic();
        }

        if (timeline.time >= AnimationDuration && oneTimeCinematic)
        {
            FirstCinematicOff();
            startTimer = false;
        }
    }

    private void StartCinematic()
    {
        if (oneTimeCinematic && waitToStart <= 0)
        {
            timeline.Play();
            cinematicCamera.enabled = true;
        }
    }

    public void FirstCinematicOn()
    {
        startTimer = true;
    }

    private void FirstCinematicOff()
    {
        oneTimeCinematic = false;
        Debug.Log(oneTimeCinematic);
    }
}

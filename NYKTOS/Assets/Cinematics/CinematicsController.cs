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
        if (startTimer && waitToStart >= 0)
        {
            waitToStart -= Time.deltaTime;
            Debug.Log("Espera, espera");
        }

        if (waitToStart <= 0)
        {
            StartCinematic();
            Debug.Log("Cinematica activada");
        }

        if (timeline.time >= AnimationDuration)
        {
            FirstCinematicOff();
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
        cinematicCamera.enabled = false;
        timeline.Stop();
        timeline.time = 0f;
        oneTimeCinematic = false;
        Debug.Log(oneTimeCinematic);
    }
}

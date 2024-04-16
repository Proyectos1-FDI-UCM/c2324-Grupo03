using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class ControlCinemachine : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter FirstCinematicEmitter;
    public CinemachineVirtualCamera cinematicCamera;
    public PlayableDirector timeline;
    private float AnimationDuration = 8f;
    void Start()
    {
        cinematicCamera.enabled = false;
        timeline.Stop();
        FirstCinematicEmitter.Perform.AddListener(FirstCinematicOn);
    }

    void Update()
    {
        if (timeline.time >= AnimationDuration)
        {
            FirstCinematicOff();
        }
    }

    public void FirstCinematicOn()
    {
        // Activa la cámara cinemática
        timeline.Play();
        cinematicCamera.enabled = true;
    }

    private void FirstCinematicOff()
    {
        cinematicCamera.enabled = false;
        timeline.Stop();
        timeline.time = 0f;
    }
}

using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class ControlCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera cinematicCamera;
    public PlayableDirector timeline;
    private float Timer = 40f;
    private float ActualTimer = 0f;
    private float ActualTimerStart;
    private bool CinematicActive = false;
    void Start()
    {
        cinematicCamera.enabled = false;
        timeline.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Activa la cámara cinemática
            cinematicCamera.enabled = true;
            timeline.Play();
            CinematicActive = true;
            ActualTimer = Timer;
            ActualTimer -= Time.deltaTime;
        }

        if (CinematicActive) { ActualTimer += Time.deltaTime; }


        if (ActualTimer <= 0 || Input.GetKeyDown(KeyCode.Escape))
        {
            cinematicCamera.enabled = false;
            timeline.Stop();
            timeline.time = 0f;
            CinematicActive = false;
            ActualTimer = 0f;
        }


    }
}

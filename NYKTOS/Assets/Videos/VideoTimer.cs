using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

/// <summary>
/// Script que controla los videos de derrota y victoria. Se inician autom�ticamente al cambiar de escena cuando se cumplen las condiciones para ganar o perder.
/// </summary>
/// 

public class VideoTimer : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _start;

    [SerializeField]
    private float _exitWaitTime = 5f;

    public VideoPlayer video;
    
    [SerializeField]
    private UnityEvent _exitEvent = new UnityEvent();
    public UnityEvent ExitEvent { get { return _exitEvent; } }

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();

        _start.Perform.AddListener(StartVideo);
        
        video.loopPointReached += End;
        
    }

    private void StartVideo()
    {
        video.Play();
    }

    void End (VideoPlayer vp) //Cuando termine el v�deo, este se queda pausado en el �ltimo frame y espera un tiempo antes de volver a la escena del Man� principal
    {
        video.Pause();
        
        Invoke(nameof(ChangeScene), _exitWaitTime);
    }

    private void ChangeScene() //Cambio de escena al men� principal
    {
        _exitEvent.Invoke();
    }

    private void OnDestroy()
    {
        _start.Perform.RemoveListener(StartVideo);
        video.loopPointReached -= End;
    }
}

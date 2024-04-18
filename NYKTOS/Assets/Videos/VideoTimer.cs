using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

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

    void End (VideoPlayer vp) 
    {
        video.Pause();
        
        Invoke(nameof(ChangeScene), _exitWaitTime);
    }

    private void ChangeScene()
    {
        _exitEvent.Invoke();
    }

    private void OnDestroy()
    {
        _start.Perform.RemoveListener(StartVideo);
        video.loopPointReached -= End;
    }
}

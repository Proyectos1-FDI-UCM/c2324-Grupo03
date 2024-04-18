using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoTimer : MonoBehaviour
{
    [SerializeField]
    private VoidEmitter _start;

    public VideoPlayer video;
    
    
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
        
    }

    private void OnDestroy()
    {
        _start.Perform.RemoveListener(StartVideo);
        video.loopPointReached -= End;
    }
}

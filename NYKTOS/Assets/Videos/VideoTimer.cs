using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoTimer : MonoBehaviour
{
    public VideoPlayer video;
    
    
    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        
        
        video.loopPointReached += End;
        
    }
     void End (VideoPlayer vp) 
    {
        video.Pause();
        
    }
}

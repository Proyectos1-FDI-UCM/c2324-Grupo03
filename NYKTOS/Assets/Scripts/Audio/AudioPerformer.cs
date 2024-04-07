using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Events;

public class AudioPerformer : MonoBehaviour
{
    static private AudioPerformer instance;
    AudioPlayer[] players;
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            players = Resources.LoadAll<AudioPlayer>("AudioResources");
            Debug.Log("[AUDIO PERFORMER]" + players.Length);
            for (int i = 0; i < players.Length; i++)
            {
                AudioSource currentSource = gameObject.AddComponent<AudioSource>();
                players[i].playAudio.AddListener((AudioClip clip, float volume, bool loop, bool isMusic) =>
                {
                    currentSource.clip = clip;
                    currentSource.volume = volume;
                    currentSource.loop = loop;
                    if (isMusic)
                    {
                        currentSource.Play();
                    }
                    else currentSource.PlayOneShot(clip, volume);
                    
                });

                players[i].stopAudio.AddListener(() => currentSource.Stop());

                players[i].pauseAudio.AddListener(() => currentSource.Pause());

                players[i].unPauseAudio.AddListener(() => currentSource.UnPause());
            }
        }
    }
}

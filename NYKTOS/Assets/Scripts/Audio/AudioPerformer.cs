using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Se encuentra en su propio objeto, y lo que hace es leer todos los AudioPlayers de la carpeta Resources y crear un AudioSource de unity para cada uno de ellos.
/// A traves de su suscripcion de eventos, se pueden controlar de forma remota los sonidos que se reproducen desde aqui.
/// </summary>
public class AudioPerformer : MonoBehaviour
{
    static private AudioPerformer instance;
    AudioPlayer[] players;

    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] AudioMixerGroup musicMixer;
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

            for (int i = 0; i < players.Length; i++)
            {
                AudioSource currentSource = gameObject.AddComponent<AudioSource>();
                
                if (players[i] != null)
                {
                    if (players[i].audioType == AudioPlayer.AudioType.Music)
                    {
                        currentSource.clip = players[i].clip[0];
                        currentSource.volume = players[i].volume;
                        currentSource.loop = players[i].loop;

                        currentSource.outputAudioMixerGroup = musicMixer;
                    }
                    else currentSource.outputAudioMixerGroup = sfxMixer;

                    //suscripcion a eventos
                    players[i].playAudio.AddListener((AudioClip clip, float volume, bool loop, bool isMusic) =>
                    {
                        if (isMusic)
                        {
                            currentSource.Play();
                        }
                        else
                        {
                            currentSource.clip = clip;
                            currentSource.volume = volume;
                            currentSource.loop = loop;
                            currentSource.PlayOneShot(clip, volume);
                        }
                        

                    });

                    players[i].stopAudio.AddListener(() => currentSource.Stop());

                    players[i].pauseAudio.AddListener(() => currentSource.Pause());

                    players[i].unPauseAudio.AddListener(() => currentSource.UnPause());
                }
                
            }
        }
    }

    /// <summary>
    /// Se quitan todas las referencias.
    /// </summary>
    void OnDestroy()
    {
        if(players != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].playAudio.RemoveAllListeners();
                players[i].stopAudio.RemoveAllListeners();
                players[i].pauseAudio.RemoveAllListeners();
                players[i].unPauseAudio.RemoveAllListeners();
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class AudioPlayer : ScriptableObject
{
    #region parameters
    [SerializeField]
    private AudioClip[] _clip;
    public AudioClip[] clip => _clip;

    [SerializeField]
    private float _volume;
    public float volume => _volume;

    [SerializeField]
    bool _loop = false;
    public bool loop => _loop;

    private enum ReproductionType
    {
        First, Random
    }
    [SerializeField]
    private ReproductionType _reproductionType = ReproductionType.First;

    public enum AudioType
    {
        FX, Music
    }
    [SerializeField]
    private AudioType _audioType = AudioType.Music;

    public AudioType audioType { get { return _audioType; } }
    #endregion

    #region events
    UnityEvent<AudioClip, float, bool, bool> _playAudio = new UnityEvent<AudioClip, float, bool, bool>();
    public UnityEvent<AudioClip, float, bool, bool> playAudio { get { return _playAudio; } }
    UnityEvent _stopAudio = new UnityEvent();
    public UnityEvent stopAudio => _stopAudio;

    UnityEvent _pauseAudio = new UnityEvent();
    public UnityEvent pauseAudio => _pauseAudio;

    UnityEvent _unPauseAudio = new UnityEvent();
    public UnityEvent unPauseAudio => _unPauseAudio;
    #endregion

    public void Play()
    {
        bool isMusic = _audioType == AudioType.Music;
        if(_clip.Length == 0)
        {
            Debug.LogError($"Falta al menos un clip de audio en {name}");
        }
        else if(_reproductionType == ReproductionType.First)
        {
            playAudio?.Invoke(_clip[0], _volume, _loop, isMusic);
        }
        else if (_reproductionType == ReproductionType.Random)
        {
            System.Random rnd = new System.Random();
            playAudio?.Invoke(_clip[rnd.Next(0, _clip.Length)], _volume, _loop, isMusic);
        }
    }

    public void Stop()
    {
        _stopAudio?.Invoke();
    }

    public void Pause()
    {
        _pauseAudio?.Invoke();
    }

    public void UnPause()
    {
        _unPauseAudio?.Invoke();
    }

}

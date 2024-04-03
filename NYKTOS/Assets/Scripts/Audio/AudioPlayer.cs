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
    public AudioClip[] clip {  get { return _clip; } }

    [SerializeField]
    private float _volume;
    public float volume {  get { return _volume; } }


    private enum ReproductionType
    {
        Default, Random
    }
    [SerializeField]
    private ReproductionType _reproductionType = ReproductionType.Default;
    #endregion

    #region events
    UnityEvent<AudioClip, float> _playAudio;
    public UnityEvent<AudioClip, float> playAudio { get { return _playAudio; } }
    UnityEvent _stopAudio;
    public UnityEvent stopAudio { get { return _stopAudio; } }
    #endregion

    public void Play()
    {
        if(_clip.Length == 0)
        {
            Debug.LogError($"Falta al menos un clip de audio en {name}");
        }
        else if(_reproductionType == ReproductionType.Default)
        {
            playAudio?.Invoke(_clip[0], volume);
        }
        else if (_reproductionType == ReproductionType.Random)
        {
            System.Random rnd = new System.Random();
            playAudio?.Invoke(_clip[rnd.Next(0, _clip.Length)], volume);
        }
    }

    public void Stop()
    {
        _stopAudio?.Invoke();
    }

}

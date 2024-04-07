using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    private void Start() {
        SetVolume(0.01f);
    }
    public void SetVolume(float volume) 
    {
        _audioMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
        //Debug.Log(volume);
    }
}

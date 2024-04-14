using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    //Configuracion del sonido de Maria

    [SerializeField]
    private AudioMixer _audioMixer;

    private void Start() {
        SetVolume(0.5f, VolumeType.Master);
        SetVolume(0.5f, VolumeType.Effects);
        SetVolume(0.5f, VolumeType.Music);
    }
    public void SetMasterVolumeSlider(float volume) {
        SetVolume(volume, VolumeType.Master);
    }

    public void SetEffectsVolumeSlider(float volume) {
        SetVolume(volume, VolumeType.Effects);
    }

    public void SetMusicVolumeSlider(float volume) {
        SetVolume(volume, VolumeType.Music);
    }
    public void SetVolume(float volume, VolumeType type) {
        switch (type) {
            case VolumeType.Master:
                _audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
                
                break;
            case VolumeType.Effects:
                _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
                
                break;
            case VolumeType.Music:
                _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
                break;
        }
    }
}
public enum VolumeType {
    Master,
    Effects,
    Music
}




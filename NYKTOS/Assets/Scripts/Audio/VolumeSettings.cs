using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
/// <summary>
/// Clase responsable del ajuste de volumen
/// </summary>
public class VolumeSettings : MonoBehaviour {
    #region references

    private float _originalMasterVolume = 0.5f;
    private float _originalSFXVolume = 0.5f;
    private float _originalMusicVolume = 0.5f;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private VolumeSettingsScriptable _volumeSettingsScriptable;

    [SerializeField]
    private Slider _masterSlider;

    [SerializeField]
    private Slider _SFXSlider;

    [SerializeField]
    private Slider _musicSlider;
    #endregion

    void Awake() {

        LoadVolumeSettings();
    }

    public void LoadVolumeSettings() {
        _masterSlider.value = _volumeSettingsScriptable.masterVolume;
        _SFXSlider.value = _volumeSettingsScriptable.SFXVolume;
        _musicSlider.value = _volumeSettingsScriptable.musicVolume;

        ApplyVolumeSettings();

        Debug.Log("Master Volume: " + _volumeSettingsScriptable.masterVolume);
        Debug.Log("SFX Volume: " + _volumeSettingsScriptable.SFXVolume);
        Debug.Log("Music Volume: " + _volumeSettingsScriptable.musicVolume);
    }
    public void SetMasterVolume(float volume) {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
        Debug.Log("master: " + volume);
        _volumeSettingsScriptable.masterVolume = _masterSlider.value;
    }
    public void SetMusicVolume(float volume) {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        _volumeSettingsScriptable.musicVolume = _musicSlider.value;
    }
    public void SetSFXVolume(float volume) {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        _volumeSettingsScriptable.SFXVolume = _SFXSlider.value;
    }
    public void ApplyVolumeSettings() {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(_volumeSettingsScriptable.masterVolume) * 20f);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(_volumeSettingsScriptable.SFXVolume) * 20f);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(_volumeSettingsScriptable.musicVolume) * 20f);
    }

    void OnApplicationQuit() {
        _volumeSettingsScriptable.masterVolume = _originalMasterVolume;
        _volumeSettingsScriptable.SFXVolume = _originalSFXVolume;
        _volumeSettingsScriptable.musicVolume = _originalMusicVolume;
    }

}





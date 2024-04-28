using UnityEngine;

/// <summary>
/// Scriptable para guardar la informacion de los sliders de volumen
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/SoundSettings")]
public class VolumeSettingsScriptable : ScriptableObject 
{
    //Son siempre numeros entre 0,0001 y 1
    [Range(0.0001f, 1f)]

    [SerializeField]
    private float _masterVolume = 0.5f;
    public float masterVolume {
        get { return _masterVolume; }
        set { _masterVolume = value; }
    }

    [Range(0.0001f, 1f)]

    [SerializeField]
    private float _musicVolume = 0.5f;
    public float musicVolume {
        get { return _musicVolume; }
        set { _musicVolume = value; }
    }

    [Range(0.0001f, 1f)]

    [SerializeField]
    private float _SFXVolume = 0.5f;
    public float SFXVolume {
        get { return _SFXVolume; }
        set { _SFXVolume = value; }
    }

}

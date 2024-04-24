using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Clase responsable del ajuste de volumen
/// </summary>
public class VolumeSettings : MonoBehaviour
{
	#region references
	[SerializeField]
	private AudioMixer _audioMixer;
	#endregion

	private void Start()
	{
		SetVolume(0.5f, VolumeType.Master);
		SetVolume(0.5f, VolumeType.Effects);
		SetVolume(0.5f, VolumeType.Music);
	}

	/// <summary>
	/// Método para cambiar el volumen del master, es llamado por el slider General del SettingsMenu
	/// </summary>
	/// <param name="volume"> parametro float que se recibe del slider del Settings Menu </param>
	public void SetMasterVolumeSlider(float volume)
	{
		SetVolume(volume, VolumeType.Master);
	}

	/// <summary>
	/// Método para cambiar el volumen de los efectos, es llamado por el slider SFX del SettingsMenu
	/// </summary>
	/// <param name="volume"> parametro float que se recibe del slider del Settings Menu </param>
	public void SetEffectsVolumeSlider(float volume)
	{
		SetVolume(volume, VolumeType.Effects);
	}
	/// <summary>
	/// Método para cambiar el volumen de la musica, es llamado por el slider Musica del SettingsMenu
	/// </summary>
	/// <param name="volume"> parametro float que se recibe del slider del Settings Menu </param>
	public void SetMusicVolumeSlider(float volume)
	{
		SetVolume(volume, VolumeType.Music);
	}

	/// <summary>
	/// Método que cambia el volumen en función del tipo del audio
	/// </summary>
	/// <param name="volume"> parametro float que se recibe del slider del Settings Menu </param>
	/// <param name="type"> el tipo de volumen (Master, Effects o Music) que estan definidos por el Enum VolumeType </param>
	public void SetVolume(float volume, VolumeType type)
	{
		switch (type)
		{
			case VolumeType.Master:
				_audioMixer.SetFloat("Volume", Mathf.Log10(volume * 2f) * 20f);

				break;
			case VolumeType.Effects:
				_audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume * 2f) * 20f);

				break;
			case VolumeType.Music:
				_audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume * 2f) * 20f);
				break;
		}
	}
}

/// <summary>
/// Enum de los distintos tipos de audio
/// </summary>
public enum VolumeType
{
	Master, // Volumen general
	Effects,
	Music
}




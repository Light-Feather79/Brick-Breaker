using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public static event Action<float, Sound> SoundSettingsChanged;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        _musicSlider.value = GameData.Instance.MusicValue;
        _SFXSlider.value = GameData.Instance.SFXValue;
    }

    public void OnMusicVolumeChanded(float volume)
    {
        _audioMixer.SetFloat("volumeMusic", volume);
        SoundSettingsChanged?.Invoke(volume, Sound.Music);
    }

    public void OnSFXVolumeChanded(float volume)
    {
        _audioMixer.SetFloat("volumeSFX", volume);
        SoundSettingsChanged?.Invoke(volume, Sound.SFX);
    }
}

public enum Sound
{
    Music,
    SFX,
}
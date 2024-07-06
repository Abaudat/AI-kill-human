using System.Collections.Generic;
using System.Linq;
using TMPEffects.Components;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider textScrollSpeedSlider, musicVolumeSlider, soundVolumeSlider;
    public AnnotatedSlider textScrollSpeedAnnotatedSlider, musicVolumeAnnotatedSlider, soundVolumeAnnotatedSlider;
    public TMPWriter tmpWriter;
    public List<AudioSource> musicAudioSources;
    public AudioSource testAudioSource;
    public AudioClip testAudioClip;

    private void Awake()
    {
        LoadSettings();
    }

    public void ChangeTextScrollSpeed(float charsPerSecond)
    {
        float secondsPerChar = 1 / charsPerSecond;
        PlayerPrefs.SetFloat("textScrollSpeed", secondsPerChar);
    }

    public void ChangeMusicVolume(float volumeInPercent)
    {
        float volumeInDecimal = volumeInPercent / 100;
        PlayerPrefs.SetFloat("musicVolume", volumeInDecimal);
        musicAudioSources.ForEach(audioSource => audioSource.volume = volumeInDecimal);
    }

    public void ChangeSoundVolume(float volumeInPercent)
    {
        float volumeInDecimal = volumeInPercent / 100;
        PlayerPrefs.SetFloat("soundVolume", volumeInDecimal);
        FindObjectsOfType<AudioSource>()
            .Where(x => x.tag != "Music")
            .ToList()
            .ForEach(audioSource => audioSource.volume = volumeInDecimal);
    }

    public void ChangeSoundVolumeAndEmitTestSound(float volumeInPercent)
    {
        ChangeSoundVolume(volumeInPercent);
        testAudioSource.PlayOneShot(testAudioClip);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("textScrollSpeed"))
        {
            float charsPerSecond = 1 / PlayerPrefs.GetFloat("textScrollSpeed");
            textScrollSpeedSlider.SetValueWithoutNotify(charsPerSecond);
            textScrollSpeedAnnotatedSlider.PropagateValueToAnnotation(charsPerSecond);
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float volumeInPercent = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("musicVolume"));
            musicVolumeSlider.SetValueWithoutNotify(volumeInPercent);
            musicVolumeAnnotatedSlider.PropagateValueToAnnotation(volumeInPercent);
            ChangeMusicVolume(volumeInPercent);
        }
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            float volumeInPercent = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("soundVolume"));
            soundVolumeSlider.SetValueWithoutNotify(volumeInPercent);
            soundVolumeAnnotatedSlider.PropagateValueToAnnotation(volumeInPercent);
            ChangeSoundVolume(volumeInPercent);
        }
    }
}

using System.Collections.Generic;
using TMPEffects.Components;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider textScrollSpeedSlider, musicVolumeSlider;
    public AnnotatedSlider textScrollSpeedAnnotatedSlider, musicVolumeAnnotatedSlider;
    public TMPWriter tmpWriter;
    public Dialogue textScrollSpeedChangedDialogue;
    public List<AudioSource> musicAudioSources;

    private DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        LoadSettings();
    }

    public void ChangeTextScrollSpeed(float charsPerSecond)
    {
        float secondsPerChar = 1 / charsPerSecond;
        PlayerPrefs.SetFloat("textScrollSpeed", secondsPerChar);
        dialogueManager.StartDialogue(textScrollSpeedChangedDialogue);
    }

    public void ChangeMusicVolume(float volumeInPercent)
    {
        float volumeInDecimal = volumeInPercent / 100;
        PlayerPrefs.SetFloat("musicVolume", volumeInDecimal);
        musicAudioSources.ForEach(audioSource => audioSource.volume = volumeInDecimal);
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
    }
}

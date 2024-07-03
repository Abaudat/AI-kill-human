using TMPEffects.Components;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider textScrollSpeedSlider;
    public AnnotatedSlider textScrollSpeedAnnotatedSlider;
    public TMPWriter tmpWriter;
    public Dialogue textScrollSpeedChangedDialogue;

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
        Debug.Log("Text scroll speed " + secondsPerChar);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("textScrollSpeed"))
        {
            float charsPerSecond = 1 / PlayerPrefs.GetFloat("textScrollSpeed");
            textScrollSpeedSlider.SetValueWithoutNotify(charsPerSecond);
            textScrollSpeedAnnotatedSlider.PropagateValueToAnnotation(charsPerSecond);
        }
    }
}

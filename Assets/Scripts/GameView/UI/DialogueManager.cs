using System;
using System.Collections.Generic;
using TMPEffects.CharacterData;
using TMPEffects.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public RawImage speakerImage;
    public TMP_Text speakerName, dialogText;
    public TMPWriter tmpWriter;
    public AudioSource audioSource;
    public AudioClip textBlip, skipClip, nextLineClip;

    private Queue<string> remainingSentences = new Queue<string>();
    private Action callback;

    public void StartDialogue(Dialogue dialogue)
    {
        if (PlayerPrefs.HasKey("textScrollSpeed"))
        {
            Debug.Log(tmpWriter);
            Debug.Log(tmpWriter.DefaultDelays);
            tmpWriter.DefaultDelays.SetDelay(PlayerPrefs.GetFloat("textScrollSpeed"));
            Debug.Log($"Delay set to {PlayerPrefs.GetFloat("textScrollSpeed")}");
        }
        dialoguePanel.SetActive(true);
        dialogue.GetSentences().ForEach(sentence => remainingSentences.Enqueue(sentence));
        DisplayNextLine();
    }

    public void StartDialogueWithCallback(Dialogue dialogue, Action callback)
    {
        this.callback = callback;
        StartDialogue(dialogue);
    }

    public void DisplayNextLineOrSkip()
    {
        if (tmpWriter.IsWriting)
        {
            if (tmpWriter.MaySkip)
            {
                audioSource.PlayOneShot(skipClip);
                tmpWriter.SkipWriter(false);
            }
        }
        else
        {
            audioSource.PlayOneShot(nextLineClip);
            DisplayNextLine();
        }
    }

    bool encounteredSeparator = true;

    public void OnCharacterDisplayed(TMPWriter writer, CharData charData)
    {
        if (encounteredSeparator)
        {
            encounteredSeparator = false;
            float semitonePitchMultiplier = 1.059463f;
            int[] pentatonicSemitones = new[] { 0, 2, 4, 7, 9 };
            audioSource.pitch = Mathf.Pow(semitonePitchMultiplier, pentatonicSemitones[UnityEngine.Random.Range(0, pentatonicSemitones.Length)]);
            audioSource.PlayOneShot(textBlip);
        }
        else
        {
            encounteredSeparator = charData.info.character == ' ' || charData.info.character == '\n';
        }
    }

    private void DisplayNextLine()
    {
        if (remainingSentences.Count > 0)
        {
            dialogText.text = remainingSentences.Dequeue();
        }
        else
        {
            if (callback != null)
            {
                callback.Invoke();
                callback = null;
            }
            dialoguePanel.SetActive(false);
        }
    }
}

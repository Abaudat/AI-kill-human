using System;
using System.Collections.Generic;
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

    private Queue<string> remainingSentences = new Queue<string>();
    private Action callback;

    public void StartDialogue(Dialogue dialogue)
    {
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
            tmpWriter.SkipWriter(false);
        }
        else
        {
            DisplayNextLine();
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

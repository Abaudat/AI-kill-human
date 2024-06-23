using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public RawImage speakerImage;
    public TMP_Text speakerName, dialogText;

    private Queue<string> remainingSentences = new Queue<string>();

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log($"{dialogue}");
        dialoguePanel.SetActive(true);
        dialogue.sentences.ForEach(sentence => remainingSentences.Enqueue(sentence));
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (remainingSentences.Count > 0)
        {
            dialogText.text = remainingSentences.Dequeue();
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}

using Core;
using TMPro;
using UnityEngine;

public class WordButton : MonoBehaviour
{
    public Word word;
    public TMP_Text wordText;

    private CurrentSentenceManager currentSentenceManager;

    private void Awake()
    {
        currentSentenceManager = FindObjectOfType<CurrentSentenceManager>();
    }

    public void Populate(Word word)
    {
        this.word = word;
        this.wordText.text = word.ToString();
    }

    public void PlayWord()
    {
        currentSentenceManager.AppendWord(word);
    }
}

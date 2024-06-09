using Core;
using TMPro;
using UnityEngine;

public class CurrentSentenceManager : MonoBehaviour
{
    public TMP_Text sentenceVisualText;

    public Sentence currentSentence;

    private CoreInterface coreInterface;

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
    }

    private void Start()
    {
        ResetSentence();
    }

    public void AppendWord(Word word)
    {
        currentSentence = currentSentence.Append(word);
        sentenceVisualText.text = currentSentence.ToString();
    }

    public void ResetSentence()
    {
        currentSentence = Sentence.Of(coreInterface.GetAiWord());
        sentenceVisualText.text = currentSentence.ToString();
    }

    public void PlaySentence()
    {
        coreInterface.PlaySentence(currentSentence);
        ResetSentence();
    }
}

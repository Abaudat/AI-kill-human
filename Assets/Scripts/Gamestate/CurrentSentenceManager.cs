using Core;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CurrentSentenceManager : MonoBehaviour
{
    public GameObject terminalWordPrefab;
    public Transform terminalWordsRoot;
    public Animator terminalAnimator;

    public Sentence currentSentence;

    private GlobalSound globalSound;
    private CoreInterface coreInterface;

    private List<TerminalWord> terminalWords = new List<TerminalWord>();

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
        globalSound = FindObjectOfType<GlobalSound>();
        terminalWords = terminalWordsRoot.GetComponentsInChildren<TerminalWord>().ToList();
    }

    private void Start()
    {
        ResetSentence();
    }

    public void AppendWord(Word word)
    {
        currentSentence = currentSentence.Append(word);
        Refresh();
        terminalAnimator.SetTrigger("Dropped");
        globalSound.PlayAddWordToSentence();
    }

    public void ResetSentence()
    {
        currentSentence = Sentence.Of(coreInterface.GetAiWord());
        Refresh();
    }

    public void PlaySentence()
    {
        coreInterface.PlaySentence(currentSentence);
        ResetSentence();
    }

    private void Refresh()
    {
        foreach (TerminalWord terminalWord in terminalWords)
        {
            Destroy(terminalWord.gameObject);
        }
        terminalWords.Clear();
        foreach (Word word in currentSentence.words)
        {
            TerminalWord terminalWord = Instantiate(terminalWordPrefab, terminalWordsRoot).GetComponent<TerminalWord>();
            terminalWord.Populate(word.ToString());
            terminalWords.Add(terminalWord);
        }
    }
}

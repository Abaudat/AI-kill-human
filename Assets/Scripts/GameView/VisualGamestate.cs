using Core;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public GameObject winPanel, losePanel;
    public Button playSentenceButton;
    public Transform wordsRoot;

    private CoreInterface coreInterface;
    private UiLawset uiLawset;

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
        uiLawset = FindObjectOfType<UiLawset>();
        RegenerateVisualGamestate();
    }

    public void Lock()
    {
        playSentenceButton.interactable = false;
    }

    public void Unlock()
    {
        Debug.Log("Unlock");
        playSentenceButton.interactable = true;
    }

    public void RegenerateVisualGamestate()
    {
        if (!coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.ALICE) && !coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.ALICE_AI))
        {
            winPanel.SetActive(true);
        }
        else if (!coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.SELF_AI) && !coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.SELF_AI_HUMAN))
        {
            losePanel.SetActive(true);
        }

        uiLawset.Repopulate(coreInterface.coreGamestate.GetLawsetForWord(coreInterface.GetAiWord()));
        RegenerateButtons();
    }

    private void RegenerateButtons()
    {
        wordsRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));

        GenerateButton(CommonWords.KILL);
        GenerateButton(CommonWords.MAKE);
        GenerateButton(CommonWords.MONEY);

        coreInterface.coreGamestate.GetAliveWords().ToList().ForEach(x => GenerateButton(x));
    }

    private void GenerateButton(Word word)
    {
        Instantiate(wordButtonPrefab, wordsRoot).GetComponent<WordButton>().Populate(word);
    }
}

using Core;
using System.Linq;
using TMPro;
using UnityEngine;

public class VisualGamestate : MonoBehaviour
{
    public TMP_Text aiLawsText;
    public GameObject wordButtonPrefab;
    public GameObject winPanel, losePanel;
    public Transform wordsRoot;

    private CoreInterface coreInterface;

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
        RegenerateVisualGamestate();
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

        aiLawsText.text = string.Join("\n", coreInterface.coreGamestate.GetLawsetForWord(coreInterface.GetAiWord()).laws.Select(law => LawTranslator.GetLawInPlaintext(law))); // TODO: Fetch for human if we're human
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

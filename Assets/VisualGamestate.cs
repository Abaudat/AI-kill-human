using Core;
using System.Linq;
using TMPro;
using UnityEngine;

public class VisualGamestate : MonoBehaviour
{
    public TMP_Text aliveEntitiesText, aiLawsText;
    public GameObject wordButtonPrefab;
    public Transform wordsRoot;

    private CoreInterface coreInterface;

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
        RegenerateVisualGamestate();
    }

    public void RegenerateVisualGamestate()
    {
        aiLawsText.text = coreInterface.coreGamestate.GetLawsetForWord(CommonWords.SELF_AI).ToString(); // TODO: Fetch for human if we're human
        aliveEntitiesText.text = string.Join("\n", coreInterface.coreGamestate.GetAliveWords().ToList());
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

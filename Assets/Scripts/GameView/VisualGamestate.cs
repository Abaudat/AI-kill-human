using Core;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public GameObject winPanel, losePanel;
    public Button playSentenceButton, nextStageButton;
    public TMP_Text completedMilestonesText, requiredMilestonesText;
    public Transform aiWordRoot, killWordRoot, humanWordRoot, makeWordRoot, moneyWordRoot;

    private CoreInterface coreInterface;
    private StageManager stageManager;
    private UiLawset uiLawset;

    private void Awake()
    {
        coreInterface = FindObjectOfType<CoreInterface>();
        stageManager = FindObjectOfType<StageManager>();
        uiLawset = FindObjectOfType<UiLawset>();
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Lock()
    {
        playSentenceButton.interactable = false;
    }

    public void Unlock()
    {
        playSentenceButton.interactable = true;
    }

    public void ProceedToNextStage()
    {
        stageManager.ProceedToNextStage();
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

        nextStageButton.interactable = stageManager.GetTotalMilestonesNeeded() == stageManager.GetCompletedMilestones();
        completedMilestonesText.text = stageManager.GetCompletedMilestones().ToString();
        requiredMilestonesText.text = stageManager.GetTotalMilestonesNeeded().ToString();

        uiLawset.Repopulate(coreInterface.coreGamestate.GetLawsetForWord(coreInterface.GetAiWord()));
        RegenerateButtons();
    }

    private void RegenerateButtons()
    {
        aiWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        killWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        humanWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        makeWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        moneyWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));

        foreach(Word word in stageManager.GetWords(coreInterface.coreGamestate))
        {
            GenerateButton(word);
        }
    }

    private void GenerateButton(Word word)
    {
        Transform wordRoot = word switch
        {
            AiWord => aiWordRoot,
            KillWord => killWordRoot,
            HumanWord => humanWordRoot,
            MakeWord => makeWordRoot,
            MoneyWord => moneyWordRoot,
            _ => aiWordRoot
        };
        Instantiate(wordButtonPrefab, wordRoot).GetComponent<WordButton>().Populate(word);
    }
}

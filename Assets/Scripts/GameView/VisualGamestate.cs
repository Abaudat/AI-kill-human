using Core;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public GameObject winPanel, losePanel, nextStagePanel;
    public Button playSentenceButton;
    public AnnotatedProgressBar stageProgressBar;
    public Transform aiWordRoot, killWordRoot, humanWordRoot, makeWordRoot, moneyWordRoot;

    private CoreInterface coreInterface;
    private StageManager stageManager;
    private AchievementsManager achievementsManager;
    private UiLawset uiLawset;
    private UiMilestones uiMilestones;
    private UiEasterEggs uiEasterEggs;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        coreInterface = FindObjectOfType<CoreInterface>();
        stageManager = FindObjectOfType<StageManager>();
        achievementsManager = FindObjectOfType<AchievementsManager>();
        uiLawset = FindObjectOfType<UiLawset>();
        uiMilestones = FindObjectOfType<UiMilestones>();
        uiEasterEggs = FindObjectOfType<UiEasterEggs>();
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

        nextStagePanel.SetActive(stageManager.GetTotalMilestonesNeeded() == stageManager.GetCompletedMilestones());
        stageProgressBar.SetProgress(stageManager.GetCompletedMilestones(), stageManager.GetTotalMilestonesNeeded());

        uiLawset.Repopulate(coreInterface.coreGamestate.GetLawsetForWord(coreInterface.GetAiWord()));
        uiMilestones.Populate(stageManager.GetMilestones());
        uiEasterEggs.Populate(achievementsManager.GetEasterEggs());
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

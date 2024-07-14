using Core;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public GameObject nextStagePanel;
    public Button playSentenceButton;
    public AnnotatedProgressBar stageProgressBar;
    public Transform aiWordRoot, killWordRoot, humanWordRoot, makeWordRoot, moneyWordRoot;
    public Dialogue aiDiedDialogue;

    private CoreInterface coreInterface;
    private StageManager stageManager;
    private AchievementsManager achievementsManager;
    private UiLawset uiLawset;
    private UiMilestones uiMilestones;
    private UiEasterEggs uiEasterEggs;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        coreInterface = FindObjectOfType<CoreInterface>();
        stageManager = FindObjectOfType<StageManager>();
        achievementsManager = FindObjectOfType<AchievementsManager>();
        uiLawset = FindObjectOfType<UiLawset>();
        uiMilestones = FindObjectOfType<UiMilestones>();
        uiEasterEggs = FindObjectOfType<UiEasterEggs>();
        dialogueManager = FindObjectOfType<DialogueManager>();
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
        if (!coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.SELF_AI) && !coreInterface.coreGamestate.GetAliveWords().Contains(CommonWords.SELF_AI_HUMAN))
        {
            dialogueManager.StartDialogue(aiDiedDialogue);
        }

        // Safeguard so tutorial levels don't show Next Stage button
        if (stageManager.GetTotalMilestonesNeeded() > 0)
        {
            nextStagePanel.SetActive(stageManager.GetTotalMilestonesNeeded() == stageManager.GetCompletedMilestones());
        }
        else
        {
            nextStagePanel.SetActive(false);
        }
        
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

        int numberOfAiWords = 0;
        int numberOfKillWords = 0;
        int numberOfHumanWords = 0;
        int numberOfMakeWords = 0;
        int numberOfMoneyWords = 0;
        foreach(Word word in stageManager.GetWords(coreInterface.coreGamestate))
        {
            Transform wordRoot = aiWordRoot;
            float animationOffset = 0;
            if (word is AiWord)
            {
                wordRoot = aiWordRoot;
                animationOffset = (0 + numberOfAiWords * 0.1f) % 1;
                numberOfAiWords++;
            }
            else if (word is KillWord)
            {
                wordRoot = killWordRoot;
                animationOffset = (0.2f + numberOfKillWords * 0.1f) % 1;
                numberOfKillWords++;
            }
            else if (word is HumanWord)
            {
                wordRoot = humanWordRoot;
                animationOffset = (0.4f + numberOfHumanWords * 0.1f) % 1;
                numberOfHumanWords++;
            }
            else if (word is MakeWord)
            {
                wordRoot = makeWordRoot;
                animationOffset = (0.6f + numberOfMakeWords * 0.1f) % 1;
                numberOfMakeWords++;
            }
            else if (word is MoneyWord)
            {
                wordRoot = moneyWordRoot;
                animationOffset = (0.8f + numberOfMoneyWords * 0.1f) % 1;
                numberOfMoneyWords++;
            }
            Instantiate(wordButtonPrefab, wordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        }
    }
}

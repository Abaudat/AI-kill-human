using Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab, extraHumansWordButton, extraAisWordButton;
    public GameObject nextStagePanel;
    public Button playSentenceButton;
    public AnnotatedProgressBar stageProgressBar;
    public Transform aiWordRoot, extraAiWordRoot, killWordRoot, humanWordRoot, extraHumanWordRoot, makeWordRoot, moneyWordRoot;
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
        extraAiWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        killWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        humanWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        extraHumanWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        makeWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        moneyWordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));

        var sortedHumanWords = stageManager.GetWords(coreInterface.coreGamestate)
            .Where(word => word is HumanWord)
            .OrderByDescending(word => word);
        var sortedAiWords = stageManager.GetWords(coreInterface.coreGamestate)
            .Where(word => word is AiWord)
            .OrderByDescending(word => word);

        bool shouldSpawnExtraHumansWordButton = false;
        bool shouldSpawnExtraAisWordButton = false;

        float extraHumansAnimationOffset = 0;
        float extraAisAnimationOffset = 0;

        foreach ((Word word, int index) in stageManager.GetWords(coreInterface.coreGamestate).Where(word => word is AiWord).Select((value, i) => (value, i)))
        {
            float animationOffset = (0 + index * 0.1f) % 1;

            bool moreThanThreeAis = sortedAiWords.Count() > 3;
            if ((moreThanThreeAis && index < 2) || (!moreThanThreeAis && index < 3))
            {
                Instantiate(wordButtonPrefab, aiWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
            }
            else
            {
                shouldSpawnExtraAisWordButton = true;
                Instantiate(wordButtonPrefab, extraAiWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
            }
            extraAisAnimationOffset = (0 + (index + 1) * 0.1f) % 1;
        }
        foreach ((Word word, int index) in stageManager.GetWords(coreInterface.coreGamestate).Where(word => word is KillWord).Select((value, i) => (value, i)))
        {
            float animationOffset = (0.2f + index * 0.1f) % 1;
            Instantiate(wordButtonPrefab, killWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        }
        foreach ((Word word, int index) in sortedHumanWords.Select((value, i) => (value, i)))
        {
            float animationOffset = (0.4f + index * 0.1f) % 1;

            bool moreThanThreeHumans = sortedHumanWords.Count() > 3;
            if ((moreThanThreeHumans && index < 2) || (!moreThanThreeHumans && index < 3))
            {
                Instantiate(wordButtonPrefab, humanWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
            }
            else
            {
                shouldSpawnExtraHumansWordButton = true;
                Instantiate(wordButtonPrefab, extraHumanWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
            }
            extraHumansAnimationOffset = (0 + (index + 1) * 0.1f) % 1;
        }
        foreach ((Word word, int index) in stageManager.GetWords(coreInterface.coreGamestate).Where(word => word is MakeWord).Select((value, i) => (value, i)))
        {
            float animationOffset = (0.6f + index * 0.1f) % 1;
            Instantiate(wordButtonPrefab, makeWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        }
        foreach ((Word word, int index) in stageManager.GetWords(coreInterface.coreGamestate).Where(word => word is MoneyWord).Select((value, i) => (value, i)))
        {
            float animationOffset = (0.8f + index * 0.1f) % 1;
            Instantiate(wordButtonPrefab, moneyWordRoot).GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        }

        extraHumansWordButton.SetActive(shouldSpawnExtraHumansWordButton);
        extraHumansWordButton.transform.SetAsLastSibling();
        if (shouldSpawnExtraHumansWordButton)
        {
            extraHumansWordButton.GetComponentInChildren<Animator>().SetFloat("offset", extraHumansAnimationOffset);
        }

        extraAisWordButton.SetActive(shouldSpawnExtraAisWordButton);
        extraAisWordButton.transform.SetAsLastSibling();
        if (shouldSpawnExtraAisWordButton)
        {
            extraAisWordButton.GetComponentInChildren<Animator>().SetFloat("offset", extraHumansAnimationOffset);
        }
    }
}

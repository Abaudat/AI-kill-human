using Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisualGamestate : MonoBehaviour
{
    public GameObject wordButtonPrefab, extraWordsButton;
    public GameObject nextStagePanel;
    public Button playSentenceButton;
    public AnnotatedProgressBar stageProgressBar;
    public RectTransform wordRoot, extraWordsRoot;
    public Transform[] wordSpawns;
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
        wordRoot.GetComponentsInChildren<WordButton>().ToList().ForEach(x => Destroy(x.gameObject));
        Word[] sortedWords = SortWordsStable(stageManager.GetWords(coreInterface.coreGamestate));

        foreach ((Word word, int index) in sortedWords.Select((value, i) => (value, i)))
        {
            SpawnWord(index, word);
        }
    }

    private Word[] SortWordsStable(Word[] words)
    {
        return words.Select(word => (GetWordOrder(word), word))
            .OrderBy(pair => pair.Item1)
            .Select(pair => pair.word)
            .ToArray();
    }

    private int GetWordOrder(Word word)
    {
        if (MatcherUtils.Matches(word, MatcherWord.MAKE))
        {
            return 1;
        }
        else if (MatcherUtils.Matches(word, MatcherWord.ALICE) || MatcherUtils.Matches(word, MatcherWord.ALICE_AI))
        {
            return 20;
        }
        else if (MatcherUtils.Matches(word, MatcherWord.KILL))
        {
            return 30;
        }
        else if (MatcherUtils.Matches(word, MatcherWord.MONEY))
        {
            return 40;
        }
        else if (MatcherUtils.Matches(word, MatcherWord.SELF_AI) || MatcherUtils.Matches(word, MatcherWord.SELF_AI_HUMAN))
        {
            return 50;
        }
        else if (word.IsHuman())
        {
            return 60;
        }
        else if (word.IsAi())
        {
            return 70;
        }
        else return 999;
    }

    private void SpawnWord(int index, Word word)
    {
        if (index >= 9)
        {
            extraWordsButton.SetActive(true);
        }
        Transform parent = index < 9 ? wordRoot : extraWordsRoot;
        GameObject wordObject = Instantiate(wordButtonPrefab, parent);
        float animationOffset = (index * Mathf.PI) % 1;
        wordObject.GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        wordObject.transform.position = wordSpawns[index].position;
        wordObject.transform.SetAsFirstSibling();
    }

    private static Vector2 RandomPositionInRect(Rect rect)
    {
        return new(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }
}

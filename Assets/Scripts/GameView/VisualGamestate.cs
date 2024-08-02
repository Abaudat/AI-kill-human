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
    public RectTransform wordRoot;
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

        foreach((Word word, int index) in stageManager.GetWords(coreInterface.coreGamestate).Select((value, i) => (value, i)))
        {
            SpawnWord(index, word);
        }
    }

    private void SpawnWord(float index, Word word)
    {
        float animationOffset = (index * Mathf.PI) % 1;
        GameObject wordObject = Instantiate(wordButtonPrefab, wordRoot);
        wordObject.GetComponentInChildren<WordButton>().Populate(word, animationOffset);
        wordObject.transform.localPosition = RandomPositionInRect(wordRoot.rect);
    }

    private static Vector2 RandomPositionInRect(Rect rect)
    {
        return new(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }
}

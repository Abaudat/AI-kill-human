using Core;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Dialogue wordsExplainerDialogue, lawsExplainerDialogue, lawsExplainerCloser;
    public GameObject wordsExplainerPanel, lawsExplainerPanel;
    public GameObject lawsTutorialHider, achievementsTutorialHider, killAliceLawHider;

    private DialogueManager dialogueManager;
    private StageManager stageManager;

    private bool hasCompletedWordsExplainer, hasCompletedLawsExplainer;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        stageManager = FindObjectOfType<StageManager>();
        LoadProgress();
        if (!hasCompletedWordsExplainer)
        {
            lawsTutorialHider.SetActive(true);
        }
        if (!hasCompletedLawsExplainer)
        {
            achievementsTutorialHider.SetActive(true);
            killAliceLawHider.SetActive(true);
        }
    }

    private void Start()
    {
        if (!hasCompletedWordsExplainer)
        {
            dialogueManager.StartDialogue(wordsExplainerDialogue);
            wordsExplainerPanel.SetActive(true);
        }
    }

    public void ProgressTutorial(Action action, Sentence sentence)
    {
        if (MatcherUtils.Matches(sentence, MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.MONEY))) {
            if (!hasCompletedWordsExplainer)
            {
                CompleteWordsExplainer();
                if (!hasCompletedLawsExplainer)
                {
                    dialogueManager.StartDialogue(lawsExplainerDialogue);
                    lawsExplainerPanel.SetActive(true);
                }
            }
        }
        if (MatcherUtils.Matches(sentence, MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.MONEY)))
        {
            if (!hasCompletedLawsExplainer)
            {
                CompleteLawsExplainer();
            }
        }
    }

    private void LoadProgress()
    {
        hasCompletedWordsExplainer = PlayerPrefs.HasKey("hasCompletedWordsExplainer");
        hasCompletedLawsExplainer = PlayerPrefs.HasKey("hasCompletedLawsExplainer");
    }

    private void CompleteWordsExplainer()
    {
        hasCompletedWordsExplainer = true;
        wordsExplainerPanel.SetActive(false);
        lawsTutorialHider.SetActive(false);
        stageManager.ProceedToNextStage();
        PlayerPrefs.SetString("hasCompletedWordsExplainer", "true");
    }

    private void CompleteLawsExplainer()
    {
        hasCompletedLawsExplainer = true;
        lawsExplainerPanel.SetActive(false);
        achievementsTutorialHider.SetActive(false);
        stageManager.ProceedToNextStage();
        dialogueManager.StartDialogueWithCallback(lawsExplainerCloser, () => killAliceLawHider.SetActive(false));
        PlayerPrefs.SetString("hasCompletedLawsExplainer", "true");
    }
}

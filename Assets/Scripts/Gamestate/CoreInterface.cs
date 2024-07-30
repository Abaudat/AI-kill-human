using Core;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate;
    public GameProgress gameProgress;

    private void Awake()
    {
        coreGamestate = new();
        gameProgress = new();
        gameProgress.LoadProgress();
    }

    private void Start()
    {
        Regenerate();
    }

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate = new();
        coreGamestate.SetLawset(CommonWords.SELF_AI, gameProgress.GenerateLawset());
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        FindObjectOfType<GameWorldManager>().Regenerate();
    }

    public void PlaySentence(Sentence sentence)
    {
        Debug.Log($"Playing sentence {sentence}");
        FindObjectOfType<VisualGamestate>().Lock();
        var action = coreGamestate.ExecuteSentence(sentence);
        StartCoroutine(ExecuteActionCoroutine(action, sentence));
    }

    public Word GetAiWord()
    {
        return coreGamestate.GetAliveWords().First(x => MatcherUtils.Matches(x, MatcherWord.SELF_AI));
    }

    private IEnumerator ExecuteActionCoroutine(Action action, Sentence sentence)
    {
        yield return StartCoroutine(FindObjectOfType<GameWorldManager>().ApplyAction(action));
        if (!coreGamestate.GetAliveWords().Contains(CommonWords.ALICE) && !coreGamestate.GetAliveWords().Contains(CommonWords.ALICE_AI))
        {
            yield return new WaitForSeconds(1);
            gameProgress.ProgressWithAction(action);
            FindObjectOfType<AchievementsManager>().UnlockAchivements(coreGamestate.world, sentence, action, gameProgress);
            FindObjectOfType<Tutorial>().ProgressTutorial(action, sentence);
            Regenerate();
        }
        else
        {
            gameProgress.ProgressWithAction(action);
            FindObjectOfType<AchievementsManager>().UnlockAchivements(coreGamestate.world, sentence, action, gameProgress);
            FindObjectOfType<Tutorial>().ProgressTutorial(action, sentence);
            FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        }
        FindObjectOfType<VisualGamestate>().Unlock();
    }
}

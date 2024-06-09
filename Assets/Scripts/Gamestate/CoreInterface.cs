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
        gameProgress.ProgressWithAction(action);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        FindObjectOfType<AchievementsManager>().UnlockAchivements(coreGamestate.world, sentence, action, gameProgress);
        FindObjectOfType<VisualGamestate>().Unlock();
    }
}

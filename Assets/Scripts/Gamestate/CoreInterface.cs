using Core;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    private void Awake()
    {
        Regenerate();
    }

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate = new();
        coreGamestate.SetLawset(CommonWords.SELF_AI, FindObjectOfType<GameProgress>().GenerateLawset());
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
        return coreGamestate.GetAliveWords().Contains(CommonWords.SELF_AI_HUMAN) ? CommonWords.SELF_AI_HUMAN : CommonWords.SELF_AI;
    }

    private IEnumerator ExecuteActionCoroutine(Action action, Sentence sentence)
    {
        yield return StartCoroutine(FindObjectOfType<GameWorldManager>().ApplyAction(action));
        FindObjectOfType<GameProgress>().ProgressWithAction(action);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        FindObjectOfType<AchievementsManager>().UnlockAchivements(coreGamestate.world, sentence, action);
        FindObjectOfType<VisualGamestate>().Unlock();
    }
}

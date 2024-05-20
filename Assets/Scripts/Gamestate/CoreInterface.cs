using Core;
using System.Collections;
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
        var action = coreGamestate.ExecuteSentence(sentence);
        StartCoroutine(ExecuteActionCoroutine(action));
    }

    private IEnumerator ExecuteActionCoroutine(Action action)
    {
        yield return StartCoroutine(FindObjectOfType<GameWorldManager>().ApplyAction(action));
        FindObjectOfType<GameProgress>().ProgressWithAction(action);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        // Enable sentence buttons
    }
}

using Core;
using UnityEngine;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate = new();
        coreGamestate.SetLawset(CommonWords.SELF_AI, FindObjectOfType<GameProgress>().GenerateLawset());
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
    }

    public Action PlaySentence(Sentence sentence)
    {
        Debug.Log($"Playing sentence {sentence}");
        var action = coreGamestate.ExecuteSentence(sentence);
        FindObjectOfType<GameProgress>().ProgressWithAction(action);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        return action;
    }
}

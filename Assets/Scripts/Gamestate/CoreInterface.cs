using Core;
using UnityEngine;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate = new();
        coreGamestate.SetLawset(CommonWords.SELF_AI, Lawset.Of(CommonLaws.YOU_MUST_NOT_KILL_HUMANS));
    }

    public Action PlaySentence(Sentence sentence)
    {
        Debug.Log($"Playing sentence {sentence}");
        var action = coreGamestate.ExecuteSentence(sentence);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        return action;
    }
}

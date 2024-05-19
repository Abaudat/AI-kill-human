using Core;
using UnityEngine;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate = new();
    }

    public Action PlaySentence(Sentence sentence)
    {
        Debug.Log($"Playing sentence {sentence}");
        var action = coreGamestate.ExecuteSentence(sentence);
        FindObjectOfType<VisualGamestate>().RegenerateVisualGamestate();
        return action;
    }
}

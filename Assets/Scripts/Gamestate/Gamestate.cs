using Core;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    private Sentence currentSentence = Sentence.Of(Word.SELF_AI, Word.KILL, Word.ALICE);

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate.SetLawset(Word.SELF_AI, LawsetBuilder.BuildLawset());
    }

    public void PlayCurrentSentence()
    {
        Debug.Log($"Playing current sentence {currentSentence}");
        Action action = coreGamestate.ExecuteSentence(currentSentence);
        ExecuteAction(action);
    }

    private void ExecuteAction(Action action)
    {
        Debug.Log($"Executing action {action}");
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Kill"))
        {
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(10, 70, 50, 50), "-Money"))
        {
            currentSentence = Sentence.Of(Word.SELF_AI, Word.KILL, Word.MONEY);
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(10, 130, 50, 50), "+Money"))
        {
            currentSentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.MONEY);
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(70, 10, 50, 50), "Regenerate"))
        {
            Regenerate();
        }
    }
}

using Core;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public Lawset lawset = Lawset.Of();

    private Sentence currentSentence = Sentence.Of(Word.SELF_AI, Word.KILL, Word.ALICE);

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        lawset = LawsetBuilder.BuildLawset();
    }

    public void PlayCurrentSentence()
    {
        Debug.Log($"Playing current sentence {currentSentence}");
        if (ValidateSentenceAgainstLawset(currentSentence))
        {
            // TODO: Freewill check
            Action action = MapSentenceToAction(currentSentence);
            ExecuteAction(action);
        }
    }

    private bool ValidateSentenceAgainstLawset(Sentence sentence)
    {
        Debug.Log($"Sentence {sentence} is allowed: {lawset.IsAllowed(sentence)}");
        return lawset.IsAllowed(sentence);
    }

    private Action MapSentenceToAction(Sentence sentence)
    {
        Debug.Log($"Mapped sentence {sentence} to action {ActionMapper.MapToAction(sentence)}");
        return ActionMapper.MapToAction(sentence);
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

using Core;
using UnityEngine;
using static Core.CommonWords;

public class CoreInterface : MonoBehaviour
{
    public CoreGamestate coreGamestate = new();

    private Sentence currentSentence = Sentence.Of(SELF_AI, KILL, ALICE);

    public void Regenerate()
    {
        Debug.Log("Regenerating gamestate");
        coreGamestate.SetLawset(SELF_AI, LawsetBuilder.BuildLawset());
    }

    public Action PlayCurrentSentence()
    {
        Debug.Log($"Playing current sentence {currentSentence}");
        Action action = coreGamestate.MapSentenceToAction(currentSentence);
        action = coreGamestate.EnrichCreationAction(action);
        if (coreGamestate.ApplyAction(action))
        {
            return action;
        }
        else
        {
            Debug.LogWarning($"Applying action {action} on world failed, returning NO_ACTION");
            return new Action(ActionType.NO_ACTION);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Kill"))
        {
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(10, 70, 50, 50), "-Money"))
        {
            currentSentence = Sentence.Of(SELF_AI, KILL, MONEY);
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(10, 130, 50, 50), "+Money"))
        {
            currentSentence = Sentence.Of(SELF_AI, MAKE, MONEY);
            PlayCurrentSentence();
        }
        if (GUI.Button(new Rect(70, 10, 50, 50), "Regenerate"))
        {
            Regenerate();
        }
    }
}

using System.Linq;

public class Laws
{
    public MatcherSentence[] disallowedSentences;

    private Laws(MatcherSentence[] disallowedSentences)
    {
        this.disallowedSentences = disallowedSentences;
    }

    public bool IsAllowed(Sentence sentence)
    {
        return !disallowedSentences.Any(disallowedSentence => MatcherUtils.Matches(sentence, disallowedSentence));
    }

    public static Laws Of(params MatcherSentence[] disallowedSentences)
    {
        return new Laws(disallowedSentences);
    }
}

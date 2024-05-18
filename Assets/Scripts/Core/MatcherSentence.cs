public class MatcherSentence
{
    public MatcherWord[] words;

    private MatcherSentence(MatcherWord[] words)
    {
        this.words = words;
    }

    public static MatcherSentence Of(params MatcherWord[] words)
    {
        return new MatcherSentence(words);
    }
}

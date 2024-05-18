public class Sentence
{
    public Word[] words;

    private Sentence(Word[] words)
    {
        this.words = words;
    }

    public static Sentence Of(params Word[] words)
    {
        return new Sentence(words);
    }
}

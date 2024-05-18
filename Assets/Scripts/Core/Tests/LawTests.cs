using Core;
using NUnit.Framework;

public class LawTests
{
    [Test]
    public void LawWithSingleSentence()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI))
           .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME))
            .IsAllowed(Sentence.Of(Word.ALICE)));
    }

    [Test]
    public void LawWithMultipleMatching()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.SELF_AI))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME), MatcherSentence.Of(MatcherWord.SELF_AI))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.SELF_AI))
            .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME), MatcherSentence.Of(MatcherWord.SELF_AI))
            .IsAllowed(Sentence.Of(Word.ALICE)));
    }

    [Test]
    public void LawWithSomeMatching()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.HUMAN_NAME))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.HUMAN_NAME))
            .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.HUMAN_NAME))
            .IsAllowed(Sentence.Of(Word.MONEY)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI), MatcherSentence.Of(MatcherWord.HUMAN_NAME))
            .IsAllowed(Sentence.Of(Word.KILL)));
    }
}

using Core;
using NUnit.Framework;
using static Core.CommonWords;

public class LawTests
{
    [Test]
    public void LawWithSingleSentence()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.AI))
            .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsTrue(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN))
           .IsAllowed(Sentence.Of(SELF_AI)));
    }

    [Test]
    public void LawWithMultipleMatching()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.AI), MatcherSentence.Of(MatcherWord.NOUN))
            .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN), MatcherSentence.Of(MatcherWord.NOUN))
            .IsAllowed(Sentence.Of(ALICE)));
    }

    [Test]
    public void LawWithSomeMatching()
    {
        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN), MatcherSentence.Of(MatcherWord.NOUN))
            .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsFalse(Law.Of(MatcherSentence.Of(MatcherWord.AI), MatcherSentence.Of(MatcherWord.NOUN))
            .IsAllowed(Sentence.Of(ALICE)));
    }
}

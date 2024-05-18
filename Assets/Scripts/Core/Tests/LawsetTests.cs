using Core;
using NUnit.Framework;
using static Core.CommonWords;

public class LawsetTests
{
    [Test]
    public void LawsetWithSingleLaw()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)))
            .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)))
           .IsAllowed(Sentence.Of(SELF_AI)));
    }

    [Test]
    public void LawsetWithMultipleMatching()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)), Law.Of(MatcherSentence.Of(MatcherWord.AI)))
           .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)))
            .IsAllowed(Sentence.Of(SELF_AI)));
    }

    [Test]
    public void LawsetWithSomeMatching()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)))
            .IsAllowed(Sentence.Of(SELF_AI)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)))
            .IsAllowed(Sentence.Of(MONEY)));
    }
}

using Core;
using NUnit.Framework;

public class LawsetTests
{
    [Test]
    public void LawsetWithSingleLaw()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME)))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
           .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME)))
            .IsAllowed(Sentence.Of(Word.ALICE)));
    }

    [Test]
    public void LawsetWithMultipleMatching()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
           .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME)), Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
            .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI_NAME)), Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)))
            .IsAllowed(Sentence.Of(Word.ALICE)));
    }

    [Test]
    public void LawsetWithSomeMatching()
    {
        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)))
            .IsAllowed(Sentence.Of(Word.SELF_AI)));

        Assert.IsFalse(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)))
            .IsAllowed(Sentence.Of(Word.ALICE)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)))
            .IsAllowed(Sentence.Of(Word.MONEY)));

        Assert.IsTrue(Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)), Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)))
            .IsAllowed(Sentence.Of(Word.KILL)));
    }
}

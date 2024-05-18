using Core;
using NUnit.Framework;

public class MatcherTests
{
    [Test]
    public void SimpleWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.SELF_AI),
            MatcherSentence.Of(MatcherWord.SELF_AI)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.OTHER_AI),
            MatcherSentence.Of(MatcherWord.OTHER_AI)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.ALICE),
            MatcherSentence.Of(MatcherWord.ALICE)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.OTHER_HUMAN),
            MatcherSentence.Of(MatcherWord.OTHER_HUMAN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.MAKE),
            MatcherSentence.Of(MatcherWord.MAKE)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.KILL),
            MatcherSentence.Of(MatcherWord.KILL)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.MONEY),
            MatcherSentence.Of(MatcherWord.MONEY)
            ));
    }

    [Test]
    public void CompositeWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.SELF_AI),
            MatcherSentence.Of(MatcherWord.AI_NAME)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
           Sentence.Of(Word.OTHER_AI),
           MatcherSentence.Of(MatcherWord.AI_NAME)
           ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.ALICE),
            MatcherSentence.Of(MatcherWord.HUMAN_NAME)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
           Sentence.Of(Word.OTHER_HUMAN),
           MatcherSentence.Of(MatcherWord.HUMAN_NAME)
           ));
    }

    [Test]
    public void MultiWordSentenceIsMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(Word.SELF_AI, Word.SELF_AI),
            MatcherSentence.Of(MatcherWord.AI_NAME, MatcherWord.AI_NAME)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
           Sentence.Of(Word.SELF_AI, Word.OTHER_AI),
           MatcherSentence.Of(MatcherWord.AI_NAME, MatcherWord.AI_NAME)
           ));
    }

    [Test]
    public void MismatchedWordsDontMatch()
    {
        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(Word.ALICE),
            MatcherSentence.Of(MatcherWord.AI_NAME)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(Word.OTHER_HUMAN),
            MatcherSentence.Of(MatcherWord.AI_NAME)
            ));
    }
}

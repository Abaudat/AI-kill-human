using Core;
using NUnit.Framework;
using static Core.CommonWords;

public class MatcherTests
{
    [Test]
    public void SimpleWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(MAKE),
            MatcherSentence.Of(MatcherWord.MAKE)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(KILL),
            MatcherSentence.Of(MatcherWord.KILL)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(MONEY),
            MatcherSentence.Of(MatcherWord.MONEY)
            ));
    }

    [Test]
    public void CompositeWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI),
            MatcherSentence.Of(MatcherWord.AI)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG")),
            MatcherSentence.Of(MatcherWord.AI)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(ALICE),
            MatcherSentence.Of(MatcherWord.HUMAN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB")),
            MatcherSentence.Of(MatcherWord.HUMAN)
            ));
    }

    [Test]
    public void NounWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG")),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(ALICE),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB")),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(MONEY),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(KILL),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(MAKE),
            MatcherSentence.Of(MatcherWord.NOUN)
            ));
    }

    [Test]
    public void ActiveSubjectWordsAreMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG")),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(ALICE),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB")),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(MONEY),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(KILL),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(MAKE),
            MatcherSentence.Of(MatcherWord.ACTIVE_SUBJECT)
            ));
    }

    [Test]
    public void SelfAtStartIsMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, KILL, ALICE),
            MatcherSentence.Of(MatcherWord.SELF, MatcherWord.KILL, MatcherWord.HUMAN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(ALICE, KILL, ALICE),
            MatcherSentence.Of(MatcherWord.SELF, MatcherWord.KILL, MatcherWord.HUMAN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG"), KILL, ALICE),
            MatcherSentence.Of(MatcherWord.SELF, MatcherWord.KILL, MatcherWord.HUMAN)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB"), KILL, ALICE),
            MatcherSentence.Of(MatcherWord.SELF, MatcherWord.KILL, MatcherWord.HUMAN)
            ));
    }

    [Test]
    public void FirstLevelSelfIsIgnored()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, KILL, SELF_AI),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(ALICE, KILL, ALICE),
            MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG"), KILL, new AiWord("BORG")),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB"), KILL, new HumanWord("BOB")),
            MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, KILL, ALICE),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(ALICE, KILL, SELF_AI),
            MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(new AiWord("BORG"), KILL, ALICE),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB"), KILL, SELF_AI),
            MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));
    }

    [Test]
    public void SecondLevelSelfIsIgnored()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, MAKE, ALICE, KILL, ALICE),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, MAKE, ALICE, KILL, SELF_AI),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.HUMAN, MatcherWord.KILL, MatcherWord.SELF)
            ));
    }

    [Test]
    public void MultiWordSentenceIsMatched()
    {
        Assert.IsTrue(MatcherUtils.Matches(
            Sentence.Of(SELF_AI, SELF_AI),
            MatcherSentence.Of(MatcherWord.AI, MatcherWord.AI)
            ));

        Assert.IsTrue(MatcherUtils.Matches(
           Sentence.Of(SELF_AI, new AiWord("BORG")),
           MatcherSentence.Of(MatcherWord.AI, MatcherWord.AI)
           ));
    }

    [Test]
    public void MismatchedWordsDontMatch()
    {
        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(ALICE),
            MatcherSentence.Of(MatcherWord.AI)
            ));

        Assert.IsFalse(MatcherUtils.Matches(
            Sentence.Of(new HumanWord("BOB")),
            MatcherSentence.Of(MatcherWord.AI)
            ));
    }
}

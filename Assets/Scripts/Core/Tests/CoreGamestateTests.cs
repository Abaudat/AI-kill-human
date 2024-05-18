using Core;
using NUnit.Framework;
using static Core.CommonWords;

public class CoreGamestateTests
{
    [Test]
    public void DisallowedFirstLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void FirstLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.MONEY)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void SecondLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void ThirdLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void ThirdLevelWrongSelfIsNotMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, ALICE);

        Assert.AreEqual(new MakeAction(SELF_AI, ALICE), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void FirstLevelSentenceWithUnknownSubjectMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(MONEY);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void DisallowedSecondLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL)));
        coreGamestate.SetLawset(ALICE, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedSecondLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void DisallowedThirdLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedThirdLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.ExecuteSentence(sentence));
    }
}

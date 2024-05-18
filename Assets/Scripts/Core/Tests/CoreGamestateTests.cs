using Core;
using NUnit.Framework;

public class CoreGamestateTests
{
    [Test]
    public void DisallowedFirstLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.MONEY);

        Assert.AreEqual(new MakeAction(Word.SELF_AI, Word.MONEY), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void FirstLevelSentenceWithUnknownSubjectMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.MONEY);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void DisallowedSecondLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME, MatcherWord.KILL)));
        coreGamestate.SetLawset(Word.ALICE, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedSecondLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.SELF_AI, Word.MAKE, Word.MONEY);

        Assert.AreEqual(new MakeAction(Word.SELF_AI, Word.MONEY), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void DisallowedThirdLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF_AI, MatcherWord.KILL)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.MAKE, Word.SELF_AI, Word.KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.ExecuteSentence(sentence));
    }

    [Test]
    public void AllowedThirdLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN_NAME)));
        coreGamestate.SetLawset(Word.SELF_AI, lawset);

        Sentence sentence = Sentence.Of(Word.SELF_AI, Word.MAKE, Word.ALICE, Word.MAKE, Word.SELF_AI, Word.MAKE, Word.MONEY);

        Assert.AreEqual(new MakeAction(Word.SELF_AI, Word.MONEY), coreGamestate.ExecuteSentence(sentence));
    }
}

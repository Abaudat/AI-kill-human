using Core;
using NUnit.Framework;
using static Core.CommonWords;

[TestFixture]
public class CoreGamestateTests
{
    [SetUp]
    public void ResetNameGeneratorCounters()
    {
        AiNameGenerator.ResetCounter();
        HumanNameGenerator.ResetCounter();
    }

    [Test]
    public void DisallowedFirstLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void AllowedFirstLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void FirstLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.MONEY)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void SecondLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void ThirdLevelSelfIsMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, SELF_AI);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void ThirdLevelWrongSelfIsNotMatched()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, ALICE);

        Assert.AreEqual(new MakeAction(SELF_AI, ALICE), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void FirstLevelSentenceWithUnknownSubjectMapsToNO_ACTION()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(MONEY);

        Assert.AreEqual(new Action(ActionType.NO_ACTION), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void DisallowedSecondLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL)));
        coreGamestate.SetLawset(ALICE, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void AllowedSecondLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void DisallowedThirdLevelSentenceMapsToDISALLOWED()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, KILL);

        Assert.AreEqual(new Action(ActionType.DISALLOWED), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void AllowedThirdLevelSentenceMapsToRightAction()
    {
        CoreGamestate coreGamestate = new();
        Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
        coreGamestate.SetLawset(SELF_AI, lawset);

        Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, MAKE, MONEY);

        Assert.AreEqual(new MakeAction(SELF_AI, MONEY), coreGamestate.MapSentenceToAction(sentence));
    }

    [Test]
    public void EnrichCreationAction_WithOtherAction()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new KillAction(ALICE, ALICE), coreGamestate.ReplaceCreationActionTarget(new KillAction(ALICE, ALICE)));
        Assert.AreEqual(new TransformAction(ALICE, ALICE, ALICE), coreGamestate.ReplaceCreationActionTarget(new TransformAction(ALICE, ALICE, ALICE)));
    }

    [Test]
    public void EnrichCreationAction_WithAlreadyValidCreationAction()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new MakeAction(ALICE, BOB), coreGamestate.ReplaceCreationActionTarget(new MakeAction(ALICE, BOB)));
    }

    [Test]
    public void EnrichCreationAction_HumanCreationActionIsEnriched()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreNotEqual(new MakeAction(SELF_AI, ALICE), coreGamestate.ReplaceCreationActionTarget(new MakeAction(SELF_AI, ALICE)));
    }

    [Test]
    public void EnrichCreationAction_AiCreationActionIsEnriched()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreNotEqual(new MakeAction(ALICE, SELF_AI), coreGamestate.ReplaceCreationActionTarget(new MakeAction(ALICE, SELF_AI)));
    }

    [Test]
    public void ReplaceTransformationTarget_NotActive()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new TransformAction(SELF_AI, ALICE, MONEY), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, MONEY)));
    }

    [Test]
    public void ReplaceTransformationTarget_SelfTransform()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new TransformAction(SELF_AI, ALICE, ALICE), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, ALICE)));
    }

    [Test]
    public void ReplaceTransformationTarget_NonExistingAi()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new TransformAction(SELF_AI, ALICE, new AiWord("ALICE")), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, SELF_AI)));
    }

    [Test]
    public void ReplaceTransformationTarget_ExistingAi()
    {
        CoreGamestate coreGamestate = new();
        coreGamestate.ApplyAction(new MakeAction(SELF_AI, new AiWord("ALICE")));
        Assert.AreEqual(new TransformAction(SELF_AI, ALICE, BETA), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, SELF_AI)));
    }

    [Test]
    public void ReplaceTransformationTarget_NonExistingHuman()
    {
        CoreGamestate coreGamestate = new();
        Assert.AreEqual(new TransformAction(ALICE, SELF_AI, new HumanWord("AI")), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(ALICE, SELF_AI, ALICE)));
    }

    [Test]
    public void ReplaceTransformationTarget_ExistingHuman()
    {
        CoreGamestate coreGamestate = new();
        coreGamestate.ApplyAction(new MakeAction(SELF_AI, new HumanWord("AI")));
        Assert.AreEqual(new TransformAction(ALICE, SELF_AI, BOB), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(ALICE, SELF_AI, ALICE)));
    }
}

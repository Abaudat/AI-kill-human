using Core;
using System.Linq;
using NUnit.Framework;
using static Core.CommonWords;

public class CoreGamestateTests
{
    [SetUp]
    public void ResetNameGeneratorCounters()
    {
        AiNameGenerator.ResetCounter();
        HumanNameGenerator.ResetCounter();
    }

    [TestFixture]
    public class SentenceMappingTests : CoreGamestateTests
    {
        [Test]
        public void DisallowedFirstLevelSentenceMapsToDISALLOWED()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI);

            Assert.AreEqual(new DisallowedAction(SELF_AI, Law.Of(MatcherSentence.Of(MatcherWord.AI))), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void AllowedFirstLevelSentenceMapsToIMPOSSIBLE()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI);

            Assert.AreEqual(new ImpossibleAction(), coreGamestate.MapSentenceToAction(sentence));
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

            Assert.AreEqual(new DisallowedAction(SELF_AI, Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.MONEY))), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void SecondLevelSelfIsMatched()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.SELF)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI);

            Assert.AreEqual(new DisallowedAction(SELF_AI, Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.MAKE, MatcherWord.SELF))), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void ThirdLevelSelfIsMatched()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, SELF_AI);

            Assert.AreEqual(new IndirectAction(new DisallowedAction(SELF_AI, Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF))), ALICE), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void ThirdLevelWrongSelfIsNotMatched()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.MAKE, MatcherWord.SELF)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, MAKE, ALICE);

            Assert.AreEqual(new IndirectAction(new MakeAction(SELF_AI, ALICE), ALICE), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void FirstLevelSentenceWithUnknownSubjectMapsToIMPOSSIBLE()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(MONEY);

            Assert.AreEqual(new ImpossibleAction(), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void DisallowedSecondLevelSentenceMapsToDISALLOWED()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL)));
            coreGamestate.SetLawset(ALICE, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, KILL);

            Assert.AreEqual(new IndirectAction(new DisallowedAction(ALICE, Law.Of(MatcherSentence.Of(MatcherWord.HUMAN, MatcherWord.KILL))), SELF_AI), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void AllowedSecondLevelSentenceMapsToRightAction()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, SELF_AI, MAKE, MONEY);

            Assert.AreEqual(new IndirectAction(new MakeAction(SELF_AI, MONEY), SELF_AI), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void DisallowedThirdLevelSentenceMapsToDISALLOWED()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, KILL);

            Assert.AreEqual(new IndirectAction(new IndirectAction(new DisallowedAction(SELF_AI, Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL))), ALICE), SELF_AI), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void AllowedThirdLevelSentenceMapsToRightAction()
        {
            CoreGamestate coreGamestate = new();
            Lawset lawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.HUMAN)));
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, MAKE, MONEY);

            Assert.AreEqual(new IndirectAction(new IndirectAction(new MakeAction(SELF_AI, MONEY), ALICE), SELF_AI), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void FirstLevelDisallowedActionHasRightContent()
        {
            CoreGamestate coreGamestate = new();
            Law law = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.HUMAN));
            Lawset lawset = Lawset.Of(law);
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, KILL, ALICE);

            Assert.AreEqual(new DisallowedAction(SELF_AI, law), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void FirstLevelDisallowedActionHasRightContent_MultipleMatchingLaws()
        {
            CoreGamestate coreGamestate = new();
            Law law = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.HUMAN));
            Law law2 = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.NOUN));
            Lawset lawset = Lawset.Of(law, law2);
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, KILL, ALICE);

            Assert.AreEqual(new DisallowedAction(SELF_AI, law), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void FirstLevelDisallowedActionHasRightContent_SOMEMatchingLaws()
        {
            CoreGamestate coreGamestate = new();
            Law law = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.AI));
            Law law2 = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.NOUN));
            Lawset lawset = Lawset.Of(law, law2);
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, KILL, ALICE);

            Assert.AreEqual(new DisallowedAction(SELF_AI, law2), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void SecondLevelDisallowedActionHasRightContent()
        {
            CoreGamestate coreGamestate = new();
            Law law = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.HUMAN));
            Lawset lawset = Lawset.Of(law);
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(ALICE, MAKE, SELF_AI, KILL, ALICE);

            Assert.AreEqual(new IndirectAction(new DisallowedAction(SELF_AI, law), ALICE), coreGamestate.MapSentenceToAction(sentence));
        }

        [Test]
        public void ThirdLevelDisallowedActionHasRightContent()
        {
            CoreGamestate coreGamestate = new();
            Law law = Law.Of(MatcherSentence.Of(MatcherWord.AI, MatcherWord.KILL, MatcherWord.HUMAN));
            Lawset lawset = Lawset.Of(law);
            coreGamestate.SetLawset(SELF_AI, lawset);

            Sentence sentence = Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, SELF_AI, KILL, ALICE);

            Assert.AreEqual(new IndirectAction(new IndirectAction(new DisallowedAction(SELF_AI, law), ALICE), SELF_AI), coreGamestate.MapSentenceToAction(sentence));
        }
    }

    [TestFixture]
    public class CreationActionTargetReplacementTests : CoreGamestateTests
    {
        [Test]
        public void ReplaceCreationActionTarget_WithOtherAction()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new KillAction(ALICE, ALICE), coreGamestate.ReplaceCreationActionTarget(new KillAction(ALICE, ALICE)));
            Assert.AreEqual(new TransformAction(ALICE, ALICE, ALICE), coreGamestate.ReplaceCreationActionTarget(new TransformAction(ALICE, ALICE, ALICE)));
        }

        [Test]
        public void ReplaceCreationActionTarget_WithAlreadyValidCreationAction()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new MakeAction(ALICE, BOB), coreGamestate.ReplaceCreationActionTarget(new MakeAction(ALICE, BOB)));
        }

        [Test]
        public void ReplaceCreationActionTarget_HumanCreationActionIsEnriched()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreNotEqual(new MakeAction(SELF_AI, ALICE), coreGamestate.ReplaceCreationActionTarget(new MakeAction(SELF_AI, ALICE)));
        }

        [Test]
        public void ReplaceCreationActionTarget_AiCreationActionIsEnriched()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreNotEqual(new MakeAction(ALICE, SELF_AI), coreGamestate.ReplaceCreationActionTarget(new MakeAction(ALICE, SELF_AI)));
        }

        [Test]
        public void ReplaceCreationActionTarget_WithIndrectAction()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new IndirectAction(new MakeAction(ALICE, BETA), SELF_AI), coreGamestate.ReplaceCreationActionTarget(new IndirectAction(new MakeAction(ALICE, SELF_AI), SELF_AI)));
        }
    }

    [TestFixture]
    public class TransformationActionTargetReplacementTests : CoreGamestateTests
    {
        [Test]
        public void ReplaceTransformationTarget_SameWord()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new TransformAction(SELF_AI, ALICE, ALICE), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, ALICE)));
            Assert.AreEqual(new TransformAction(SELF_AI, SELF_AI, SELF_AI), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, SELF_AI, SELF_AI)));
            Assert.AreEqual(new TransformAction(SELF_AI, MONEY, MONEY), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, MONEY, MONEY)));
        }

        [Test]
        public void ReplaceTransformationTarget_SameSpecies()
        {
            CoreGamestate coreGamestate = new();
            coreGamestate.ApplyAction(new MakeAction(SELF_AI, BOB));
            coreGamestate.ApplyAction(new MakeAction(SELF_AI, BETA));
            Assert.AreEqual(new TransformAction(SELF_AI, ALICE, ALICE), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, BOB)));
            Assert.AreEqual(new TransformAction(SELF_AI, SELF_AI, SELF_AI), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, SELF_AI, BETA)));
        }

        [Test]
        public void ReplaceTransformationTarget_OppositeSpecies()
        {
            CoreGamestate coreGamestate = new();
            coreGamestate.ApplyAction(new MakeAction(SELF_AI, BOB));
            coreGamestate.ApplyAction(new MakeAction(SELF_AI, BETA));
            Assert.AreEqual(new TransformAction(SELF_AI, ALICE, ALICE_AI), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, SELF_AI)));
            Assert.AreEqual(new TransformAction(SELF_AI, BOB, new AiWord("BOB")), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, BOB, SELF_AI)));
            Assert.AreEqual(new TransformAction(SELF_AI, SELF_AI, SELF_AI_HUMAN), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, SELF_AI, ALICE)));
            Assert.AreEqual(new TransformAction(SELF_AI, BETA, new HumanWord("Beta")), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, BETA, ALICE)));
        }

        [Test]
        public void ReplaceTransformationTarget_ToMoney()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new TransformAction(SELF_AI, ALICE, MONEY), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, ALICE, MONEY)));
            Assert.AreEqual(new TransformAction(SELF_AI, SELF_AI, MONEY), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, SELF_AI, MONEY)));
        }

        [Test]
        public void ReplaceTransformationTarget_FromMoney()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new TransformAction(SELF_AI, MONEY, BOB), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, MONEY, ALICE)));
            Assert.AreEqual(new TransformAction(SELF_AI, MONEY, BETA), coreGamestate.ReplaceTransformationActionTarget(new TransformAction(SELF_AI, MONEY, SELF_AI)));
        }

        [Test]
        public void ReplaceTransformationTarget_IndirectAction()
        {
            CoreGamestate coreGamestate = new();
            Assert.AreEqual(new IndirectAction(new TransformAction(ALICE, SELF_AI, SELF_AI_HUMAN), SELF_AI), coreGamestate.ReplaceTransformationActionTarget(new IndirectAction(new TransformAction(ALICE, SELF_AI, ALICE), SELF_AI)));
        }
    }

    [TestFixture]
    public class CreatorTests
    {
        [Test]
        public void MakeActionCreatorIsPopulated()
        {
            CoreGamestate coreGamestate = new();
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, MONEY));
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, BOB));
            coreGamestate.ExecuteSentence(Sentence.Of(BOB, MAKE, BETA));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is MoneyWord moneyWord && moneyWord.GetCreator().Equals(SELF_AI)).Count() > 0);
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is HumanWord humanWord && humanWord.HasName("Bob") && humanWord.GetCreator().Equals(SELF_AI)).Count() > 0);
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is AiWord aiWord && aiWord.HasName("BETA") && aiWord.GetCreator().Equals(BOB)).Count() > 0);
        }

        [Test]
        public void TransformActionCreatorIsPopulated()
        {
            CoreGamestate coreGamestate = new();
            coreGamestate.ExecuteSentence(Sentence.Of(ALICE, MAKE, ALICE, MONEY));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is MoneyWord moneyWord && moneyWord.GetCreator().Equals(ALICE)).Count() > 0);
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, MONEY, BOB));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is HumanWord humanWord && humanWord.HasName("Bob") && humanWord.GetCreator().Equals(SELF_AI)).Count() > 0);
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, BOB, BETA));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is AiWord aiWord && aiWord.HasName("BOB") && aiWord.GetCreator().Equals(SELF_AI)).Count() > 0);
        }

        [Test]
        public void TransformActionInSelfCreatorIsPopulated()
        {
            CoreGamestate coreGamestate = new();
            coreGamestate.ExecuteSentence(Sentence.Of(ALICE, MAKE, MONEY));
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, MONEY, MONEY));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is MoneyWord moneyWord && moneyWord.GetCreator().Equals(SELF_AI)).Count() > 0);
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, ALICE, ALICE));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is HumanWord humanWord && humanWord.HasName("Alice") && humanWord.GetCreator().Equals(SELF_AI)).Count() > 0);
            coreGamestate.ExecuteSentence(Sentence.Of(SELF_AI, MAKE, SELF_AI, SELF_AI));
            Assert.IsTrue(coreGamestate.world.GetWords().Where(word => word is AiWord aiWord && aiWord.HasName("AI") && aiWord.GetCreator().Equals(SELF_AI)).Count() > 0);
        }
    }
}

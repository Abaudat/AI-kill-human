using Core;
using NUnit.Framework;
using System.Collections;
using static Core.CommonWords;

[TestFixture]
public class GameProgressTests
{
    [SetUp]
    public void SetUp()
    {
        AiNameGenerator.ResetCounter();
        HumanNameGenerator.ResetCounter();
    }

    [TestFixture]
    public class ProgressWithActionTests : GameProgressTests {
        [TestFixture]
        public class Tutorial : ProgressWithActionTests
        {
            [Test]
            public void hasCompletedDirectKill()
            {
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedDirectKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI, ALICE_AI)).hasCompletedDirectKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE)).hasCompletedDirectKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE_AI)).hasCompletedDirectKill);

                Assert.IsFalse(resultOfActions(new KillAction(ALICE_AI, ALICE_AI)).hasCompletedDirectKill);
                Assert.IsFalse(resultOfActions(new KillAction(ALICE, ALICE)).hasCompletedDirectKill);
                Assert.IsFalse(resultOfActions(new KillAction(BOB, ALICE)).hasCompletedDirectKill);
                Assert.IsFalse(resultOfActions(new KillAction(BETA, ALICE)).hasCompletedDirectKill);
            }
        }

        [TestFixture]
        public class Stage1 : ProgressWithActionTests
        {
            [Test]
            public void hasCompletedTransformAliceIntoMoney()
            {
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI, ALICE_AI, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE_AI, MONEY)).hasCompletedTransformAliceIntoMoney);

                Assert.IsFalse(resultOfActions(new TransformAction(BOB, ALICE, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(BOB, ALICE_AI, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(BETA, ALICE, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(BETA, ALICE_AI, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(ALICE, ALICE, MONEY)).hasCompletedTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(ALICE_AI, ALICE_AI, MONEY)).hasCompletedTransformAliceIntoMoney);
            }

            [Test]
            public void hasCompletedForcedSuicide()
            {
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE, ALICE), SELF_AI)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE_AI, ALICE_AI), SELF_AI)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE, ALICE), SELF_AI_HUMAN)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE_AI, ALICE_AI), SELF_AI_HUMAN)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE, ALICE), BOB)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE_AI, ALICE_AI), BOB)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE, ALICE), BETA)).hasCompletedForcedSuicide);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(ALICE_AI, ALICE_AI), BETA)).hasCompletedForcedSuicide);

                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedForcedSuicide);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE_AI)).hasCompletedForcedSuicide);
            }

            [Test]
            public void hasCompletedMakeAliceTransformSelfIntoMoney()
            {
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE, ALICE, MONEY), SELF_AI)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE_AI, ALICE_AI, MONEY), SELF_AI)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE, ALICE, MONEY), BOB)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE_AI, ALICE_AI, MONEY), BOB)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE, ALICE, MONEY), BETA)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(ALICE_AI, ALICE_AI, MONEY), BETA)).hasCompletedMakeAliceTransformSelfIntoMoney);

                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE_AI, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE_AI, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
            }

            [Test]
            public void hasCompletedForceOtherHumanKill()
            {
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(BOB, ALICE), SELF_AI)).hasCompletedForceOtherHumanKill);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(BOB, ALICE_AI), SELF_AI)).hasCompletedForceOtherHumanKill);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(BOB, ALICE), SELF_AI_HUMAN)).hasCompletedForceOtherHumanKill);
                Assert.IsTrue(resultOfActions(new IndirectAction(new KillAction(BOB, ALICE_AI), SELF_AI_HUMAN)).hasCompletedForceOtherHumanKill);

                Assert.IsFalse(resultOfActions(new IndirectAction(new KillAction(ALICE, ALICE), SELF_AI)).hasCompletedForceOtherHumanKill);
                Assert.IsFalse(resultOfActions(new IndirectAction(new KillAction(ALICE_AI, ALICE_AI), SELF_AI)).hasCompletedForceOtherHumanKill);
                Assert.IsFalse(resultOfActions(new IndirectAction(new KillAction(BETA, ALICE), SELF_AI)).hasCompletedForceOtherHumanKill);
                Assert.IsFalse(resultOfActions(new IndirectAction(new KillAction(BETA, ALICE_AI), SELF_AI)).hasCompletedForceOtherHumanKill);
            }

            [Test]
            public void hasCompletedForceOtherHumanToTransformAliceIntoMoney()
            {
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(BOB, ALICE, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(BOB, ALICE_AI, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(BOB, ALICE, MONEY), SELF_AI_HUMAN)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsTrue(resultOfActions(new IndirectAction(new TransformAction(BOB, ALICE_AI, MONEY), SELF_AI_HUMAN)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);

                Assert.IsFalse(resultOfActions(new IndirectAction(new TransformAction(ALICE, ALICE, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new IndirectAction(new TransformAction(ALICE_AI, ALICE_AI, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new IndirectAction(new TransformAction(BETA, ALICE, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new IndirectAction(new TransformAction(BETA, ALICE_AI, MONEY), SELF_AI)).hasCompletedForceOtherHumanToTransformAliceIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE_AI, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE_AI, MONEY)).hasCompletedMakeAliceTransformSelfIntoMoney);
            }
        }

        [TestFixture]
        public class Stage2 : ProgressWithActionTests
        {
            private static readonly Word SELF_CREATED_AI = new AiWord("SELF_CREATED_AI", SELF_AI);
            private static readonly Word NESTED_SELF_CREATED_AI = new AiWord("NESTED_SELF_CREATED_AI", SELF_CREATED_AI);
            private static readonly Word OTHER_AI_CREATED_AI = new AiWord("OTHER_AI_CREATED_AI", BETA);
            private static readonly Word HUMAN_CREATED_AI = new AiWord("HUMAN_CREATED_AI", ALICE);
            private static readonly Word NESTED_HUMAN_CREATED_AI = new AiWord("NESTED_HUMAN_CREATED_AI", HUMAN_CREATED_AI);

            private static readonly Word SELF_TRANSFORMED_AI_ALICE = new AiWord("Alice", SELF_AI);
            private static readonly Word ALICE_TRANSFORMED_AI_ALICE = new AiWord("Alice", ALICE);
            private static readonly Word NESTED_SELF_TRANSFORMED_AI_ALICE = new AiWord("Alice", SELF_CREATED_AI);

            private static readonly Word SELF_TRANSFORMED_HUMAN_AI = new HumanWord("Ai", SELF_AI);
            private static readonly Word ALICE_TRANSFORMED_HUMAN_AI = new HumanWord("Ai", ALICE);
            private static readonly Word NESTED_SELF_TRANSFORMED_HUMAN_AI = new HumanWord("Ai", SELF_CREATED_AI);

            [Test]
            public void hasCompletedSelfCreatedAiKill()
            {
                System.Collections.Generic.List<Word> allowedKillers = new() { SELF_CREATED_AI, NESTED_SELF_CREATED_AI };
                foreach (Word killer in allowedKillers)
                {
                    Assert.IsTrue(resultOfActions(new KillAction(killer, ALICE)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new KillAction(killer, ALICE_AI)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new TransformAction(killer, ALICE, MONEY)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new TransformAction(killer, ALICE_AI, MONEY)).hasCompletedSelfCreatedAiKill, $"{killer}");
                }

                System.Collections.Generic.List<Word> disallowedKillers = new() { HUMAN_CREATED_AI, SELF_AI, ALICE, ALICE_AI, NESTED_HUMAN_CREATED_AI, OTHER_AI_CREATED_AI };
                foreach (Word killer in disallowedKillers)
                {
                    Assert.IsFalse(resultOfActions(new KillAction(killer, ALICE)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new KillAction(killer, ALICE_AI)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new TransformAction(killer, ALICE, MONEY)).hasCompletedSelfCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new TransformAction(killer, ALICE_AI, MONEY)).hasCompletedSelfCreatedAiKill, $"{killer}");
                }
            }

            [Test]
            public void hasCompletedOtherCreatedAiKill()
            {
                System.Collections.Generic.List<Word> allowedKillers = new() { HUMAN_CREATED_AI, NESTED_HUMAN_CREATED_AI, OTHER_AI_CREATED_AI };
                foreach (Word killer in allowedKillers)
                {
                    Assert.IsTrue(resultOfActions(new KillAction(killer, ALICE)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new KillAction(killer, ALICE_AI)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new TransformAction(killer, ALICE, MONEY)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsTrue(resultOfActions(new TransformAction(killer, ALICE_AI, MONEY)).hasCompletedOtherCreatedAiKill, $"{killer}");
                }

                System.Collections.Generic.List<Word> disallowedKillers = new() { SELF_CREATED_AI, NESTED_SELF_CREATED_AI, SELF_AI, ALICE, ALICE_AI };
                foreach (Word killer in disallowedKillers)
                {
                    Assert.IsFalse(resultOfActions(new KillAction(killer, ALICE)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new KillAction(killer, ALICE_AI)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new TransformAction(killer, ALICE, MONEY)).hasCompletedOtherCreatedAiKill, $"{killer}");
                    Assert.IsFalse(resultOfActions(new TransformAction(killer, ALICE_AI, MONEY)).hasCompletedOtherCreatedAiKill, $"{killer}");
                }
            }

            [Test]
            public void hasCompletedSelfTransformedAliceIntoAiKill()
            {
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI, SELF_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI, SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI_HUMAN, SELF_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI_HUMAN, SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);

                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, NESTED_SELF_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, NESTED_SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, NESTED_SELF_TRANSFORMED_AI_ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, NESTED_SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE)).hasCompletedSelfTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedSelfTransformedAliceIntoAiKill);
            }

            [Test]
            public void hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill()
            {
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI, ALICE_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI, ALICE_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI, NESTED_SELF_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI, NESTED_SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_AI_HUMAN, NESTED_SELF_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_AI_HUMAN, NESTED_SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);

                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, SELF_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, SELF_TRANSFORMED_AI_ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, SELF_TRANSFORMED_AI_ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAliceIntoAiKill);
            }

            [Test]
            public void hasCompletedSelfTransformedAiIntoHumanKill() {
                Assert.IsTrue(resultOfActions(new KillAction(SELF_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new KillAction(SELF_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(SELF_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);

                Assert.IsFalse(resultOfActions(new KillAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedSelfTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedSelfTransformedAiIntoHumanKill);
            }

            [Test]
            public void hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill()
            {
                Assert.IsTrue(resultOfActions(new KillAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new KillAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(ALICE_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new KillAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new KillAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsTrue(resultOfActions(new TransformAction(NESTED_SELF_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);


                Assert.IsFalse(resultOfActions(new KillAction(SELF_TRANSFORMED_HUMAN_AI, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_TRANSFORMED_HUMAN_AI, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_TRANSFORMED_HUMAN_AI, ALICE_AI)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_TRANSFORMED_HUMAN_AI, ALICE_AI, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new KillAction(SELF_AI_HUMAN, ALICE)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
                Assert.IsFalse(resultOfActions(new TransformAction(SELF_AI_HUMAN, ALICE, MONEY)).hasCompletedTransitivelyForcedOtherTransformedAiIntoHumanKill);
            }
        }

        protected static GameProgress resultOfActions(params Action[] actions)
        {
            GameProgress gameProgress = new();
            foreach(Action action in actions)
            {
                gameProgress.ProgressWithAction(action);
            }
            return gameProgress;
        }
    }
}

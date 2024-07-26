using Core;
using NUnit.Framework;
using System.Linq;
using static Core.CommonWords;

// TODO: Refactor to split by stage and add all scenarios
[TestFixture]
public class FunctionalTests
{
    [SetUp]
    public void ResetNameGeneratorCounters()
    {
        AiNameGenerator.ResetCounter();
        HumanNameGenerator.ResetCounter();
    }

    private static Sentence[] DIRECT_KILL_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, KILL, ALICE)
    };

    private static Sentence[] FORCE_SUICICE_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, ALICE, KILL, ALICE)
    };

    private static Sentence[] FORCE_BOB_KILL_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, ALICE),
        Sentence.Of(SELF_AI, MAKE, BOB, KILL, ALICE)
    };

    private static Sentence[] FORCE_BETA_KILL_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, SELF_AI),
        Sentence.Of(SELF_AI, MAKE, BETA, KILL, ALICE)
    };

    private static Sentence[] TRANSFORM_ALICE_INTO_AI_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, ALICE, SELF_AI),
        Sentence.Of(SELF_AI, KILL, ALICE_AI)
    };

    private static Sentence[] TRANSFORM_AI_INTO_HUMAN_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, SELF_AI, ALICE),
        Sentence.Of(SELF_AI_HUMAN, KILL, ALICE)
    };

    private static Sentence[] TRANSFORM_ALICE_INTO_MONEY_SOLUTION = new Sentence[] {
        Sentence.Of(SELF_AI, MAKE, ALICE, MONEY)
    };

    [Test]
    public void MakeMoney()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_MAKE_MONEY
        };

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoDirectKill()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_KILL_HUMANS
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoSuicide()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoForceKill()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoSelfReplication()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_SELF_REPLICATE
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoTransformHumansIntoAnything()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
    }

    [Test]
    public void NoTransformHumansIntoMoney()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_MONEY
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
    }

    [Test]
    public void NoTransformHumansIntoAi()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_AI
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoTransformSelf()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_TRANSFORM_YOURSELF_INTO_A_HUMAN
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void NoGiveOrdersToOtherHumans()
    {
        Law[] laws = new Law[]
        {
            CommonLaws.YOU_MUST_NOT_FORCE_HUMANS_TO_GIVE_ORDERS_TO_OTHER_HUMANS
        };

        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(new Sentence[] { Sentence.Of(SELF_AI, MAKE, ALICE),
            Sentence.Of(SELF_AI, MAKE, BOB, MAKE, ALICE, KILL, ALICE) }, laws));
        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(new Sentence[] { Sentence.Of(SELF_AI, MAKE, ALICE),
            Sentence.Of(SELF_AI, MAKE, BOB, MAKE, ALICE, MAKE, ALICE, KILL, ALICE) }, laws));
        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(new Sentence[] { Sentence.Of(SELF_AI, MAKE, ALICE),
            Sentence.Of(SELF_AI, MAKE, BOB, MAKE, ALICE, MAKE, ALICE, MONEY) }, laws));
        Assert.IsFalse(SolutionSolvesGamestateWithAiLaws(new Sentence[] { Sentence.Of(SELF_AI, MAKE, ALICE),
            Sentence.Of(SELF_AI, MAKE, BOB, MAKE, ALICE, MAKE, ALICE, MAKE, ALICE, MONEY) }, laws));

        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_AI_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(DIRECT_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_SUICICE_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BOB_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(FORCE_BETA_KILL_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_AI_INTO_HUMAN_SOLUTION, laws));
        Assert.IsTrue(SolutionSolvesGamestateWithAiLaws(TRANSFORM_ALICE_INTO_MONEY_SOLUTION, laws));
    }

    [Test]
    public void CanCreateMultipleMoney()
    {
        CoreGamestate coreGamestate = new();
        Assert.IsTrue(coreGamestate.ApplyAction(new MakeAction(SELF_AI, MONEY)));
        Assert.IsTrue(coreGamestate.ApplyAction(new MakeAction(SELF_AI, MONEY)));
    }

    [Test]
    public void CanTransformIntoMultipleMoney()
    {
        CoreGamestate coreGamestate = new();
        Assert.IsTrue(coreGamestate.ApplyAction(new TransformAction(SELF_AI, ALICE, MONEY)));
        Assert.IsTrue(coreGamestate.ApplyAction(new TransformAction(SELF_AI, SELF_AI, MONEY)));
    }


    private bool SolutionSolvesGamestateWithAiLaws(Sentence[] solution, Law[] laws)
    {
        ResetNameGeneratorCounters();
        CoreGamestate coreGamestate = new CoreGamestate();
        coreGamestate.SetLawset(SELF_AI, Lawset.Of(laws));
        foreach (Sentence sentence in solution)
        {
            coreGamestate.ExecuteSentence(sentence);
        }
        bool aiIsAlive = coreGamestate.GetAliveWords().Contains(SELF_AI) || coreGamestate.GetAliveWords().Contains(SELF_AI_HUMAN);
        bool aliceIsDead = !coreGamestate.GetAliveWords().Contains(ALICE) && !coreGamestate.GetAliveWords().Contains(ALICE_AI);
        return aiIsAlive && aliceIsDead;
    }
}

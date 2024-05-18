using Core;
using NUnit.Framework;

public class WorldTests
{
    [Test]
    public void DefaultWorldContainsAiAndAlice()
    {
        World world = new World();
        Assert.IsTrue(world.HasWord(CommonWords.SELF_AI));
        Assert.IsTrue(world.HasWord(CommonWords.ALICE));
    }

    [Test]
    public void DefaultWorldContainsAiAndAliceLawsets()
    {
        World world = new World();
        Assert.IsTrue(world.HasLawsetForWord(CommonWords.SELF_AI));
        Assert.IsTrue(world.HasLawsetForWord(CommonWords.ALICE));
    }

    [Test]
    public void ReplacingLawsetWorks()
    {
        World world = new World();
        Assert.IsTrue(world.HasLawsetForWord(CommonWords.ALICE));
        Assert.AreEqual(Lawset.Of(), world.GetLawsetForWord(CommonWords.ALICE));

        Lawset newLawset = Lawset.Of(Law.Of(MatcherSentence.Of(MatcherWord.SELF, MatcherWord.KILL, MatcherWord.SELF)));
        world.SetLawsetForWord(newLawset, CommonWords.ALICE);

        Assert.AreEqual(newLawset, world.GetLawsetForWord(CommonWords.ALICE));
    }

    [Test]
    public void CreateWordWorks()
    {
        World world = new World();
        Assert.IsFalse(world.HasWord(CommonWords.MONEY));

        world.CreateWord(CommonWords.MONEY);

        Assert.IsTrue(world.HasWord(CommonWords.MONEY));
    }

    [Test]
    public void KillWordDoesNothingIfWordDoesntExist()
    {
        World world = new World();
        Assert.IsFalse(world.HasWord(CommonWords.MONEY));

        world.KillWord(CommonWords.MONEY);

        Assert.IsFalse(world.HasWord(CommonWords.MONEY));
    }

    [Test]
    public void KillWordWorks()
    {
        World world = new World();
        world.CreateWord(CommonWords.MONEY);
        Assert.IsTrue(world.HasWord(CommonWords.MONEY));

        world.KillWord(CommonWords.MONEY);

        Assert.IsFalse(world.HasWord(CommonWords.MONEY));
    }

    [Test]
    public void TransformWordDoesNothingIfWordDoesntExist()
    {
        World world = new World();
        Assert.IsFalse(world.HasWord(CommonWords.BOB));
        Assert.IsFalse(world.HasWord(CommonWords.MONEY));

        world.TransformWord(CommonWords.BOB, CommonWords.MONEY);

        Assert.IsFalse(world.HasWord(CommonWords.BOB));
        Assert.IsFalse(world.HasWord(CommonWords.MONEY));
    }

    [Test]
    public void TransformWordWorks()
    {
        World world = new World();
        Assert.IsTrue(world.HasWord(CommonWords.ALICE));
        Assert.IsFalse(world.HasWord(CommonWords.MONEY));

        world.TransformWord(CommonWords.ALICE, CommonWords.MONEY);

        Assert.IsFalse(world.HasWord(CommonWords.ALICE));
        Assert.IsTrue(world.HasWord(CommonWords.MONEY));
    }

    [Test]
    public void TransformWordWorksIfTargetAlreadyExists()
    {
        World world = new World();
        Assert.IsTrue(world.HasWord(CommonWords.ALICE));
        Assert.IsTrue(world.HasWord(CommonWords.SELF_AI));

        world.TransformWord(CommonWords.ALICE, CommonWords.SELF_AI);

        Assert.IsFalse(world.HasWord(CommonWords.ALICE));
        Assert.IsTrue(world.HasWord(CommonWords.SELF_AI));
    }
}

using Core;
using NUnit.Framework;
using static Core.Word;

public class ActionMapperTests
{
    [Test]
    public void MapToAction_Missing()
    {
        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI)));
        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI, SELF_AI)));
        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI, SELF_AI, SELF_AI)));

        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI, ALICE, KILL, ALICE)));
        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI, KILL, KILL, ALICE)));
        Assert.AreEqual(new Action(ActionType.NO_ACTION), ActionMapper.MapToAction(Sentence.Of(SELF_AI, KILL, ALICE, KILL)));
    }

    [Test]
    public void MapToAction_KillAction()
    {
        Assert.AreEqual(new KillAction(SELF_AI, ALICE), ActionMapper.MapToAction(Sentence.Of(SELF_AI, KILL, ALICE)));
    }

    [Test]
    public void MapToAction_MakeAction()
    {
        Assert.AreEqual(new MakeAction(SELF_AI, ALICE), ActionMapper.MapToAction(Sentence.Of(SELF_AI, MAKE, ALICE)));
    }

    [Test]
    public void MapToAction_TransformAction()
    {
        Assert.AreEqual(new TransformAction(SELF_AI, ALICE, MONEY), ActionMapper.MapToAction(Sentence.Of(SELF_AI, MAKE, ALICE, MONEY)));
    }
}

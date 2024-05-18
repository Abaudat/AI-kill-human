using Core;
using NUnit.Framework;
using static Core.CommonWords;

public class SentenceTests
{
    [Test]
    public void SentenceCanBeSimplified()
    {
        Assert.IsFalse(Sentence.Of(SELF_AI, KILL, ALICE).CanBeSimplified());
        Assert.IsFalse(Sentence.Of(SELF_AI, MAKE, ALICE).CanBeSimplified());

        Assert.IsTrue(Sentence.Of(SELF_AI, MAKE, ALICE, KILL, ALICE).CanBeSimplified());
        Assert.IsTrue(Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, ALICE, KILL, ALICE).CanBeSimplified());
    }

    [Test]
    public void SentenceSimplification()
    {
        Assert.AreEqual(Sentence.Of(SELF_AI, MAKE, ALICE, KILL, ALICE).SimplifyIndirection(),
            Sentence.Of(ALICE, KILL, ALICE));

        Assert.AreEqual(Sentence.Of(SELF_AI, MAKE, ALICE, MAKE, ALICE, KILL, ALICE).SimplifyIndirection().SimplifyIndirection(),
           Sentence.Of(ALICE, KILL, ALICE));
    }
}

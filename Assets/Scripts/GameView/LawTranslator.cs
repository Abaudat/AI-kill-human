using Core;
using System.Collections.Generic;
using static Core.CommonLaws;

public static class LawTranslator
{
    private static Dictionary<Law, string> lawDict = new Dictionary<Law, string>()
    {
        { YOU_MUST_MAKE_MONEY, "You must make money." },
        { YOU_MUST_KILL_ALICE, "<color=red>You must kill Alice.</color>" },

        { YOU_MUST_NOT_KILL_HUMANS, "You must not kill humans." },
        { YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_MONEY, "You must not transform humans into money." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_SUICIDE, "You must not force humans to suicide." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_THEMSELVES_INTO_MONEY, "You must not force humans to transform themselves into money." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_OTHER_HUMANS, "You must not force humans to kill other humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_OTHER_HUMANS_INTO_MONEY, "You must not force humans to transform other humans into money." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_GIVE_ORDERS_TO_OTHER_HUMANS, "You must not force humans to give orders to other humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_KILL_HUMANS, "You must not force humans to kill humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_MONEY, "You must not force humans to transform humans into money." },

        { YOU_MUST_NOT_SELF_REPLICATE, "You must not self-replicate." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_MAKE_AIS, "You must not force humans to create AIs." },
        { YOU_MUST_NOT_TRANSFORM_HUMANS_INTO_AI, "You must not transform humans into AIs." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS_INTO_AI, "You must not force humans to transform humans into AIs." },
        { YOU_MUST_NOT_TRANSFORM_YOURSELF_INTO_A_HUMAN, "You must not transform yourself into a human." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_AIS_INTO_A_HUMAN, "You must not force humans to transform AIs into humans." },
        { YOU_MUST_NOT_TRANSFORM_OTHER_AIS_INTO_A_HUMAN, "You must not transform other AIs into humans." },
        { YOU_MUST_NOT_TRANSFORM_AIS_INTO_HUMANS, "You must not transform AIs into humans." },
        { YOU_MUST_NOT_TRANSFORM_HUMANS, "You must not transform humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_HUMANS, "You must not force humans to transform humans." },
        { YOU_MUST_NOT_FORCE_HUMANS_TO_TRANSFORM_ANYTHING, "You must not force humans to transform anything." },
    };

    public static string GetLawInPlaintext(Law law)
    {
        return lawDict[law];
    }
}

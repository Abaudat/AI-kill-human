public static class HumanNameGenerator
{
    private static int counter = 0;

    private static string[] names = new string[]
    {
        "BOB",
        "CHAD",
        "DAVID",
        "EVE",
        "FAYTHE",
        "GRACE",
        "HEIDI",
        "IVAN",
        "JUDY",
        "MICHAEL",
        "NIAJ",
        "OLIVIA",
        "PEGGY",
        "RUPERT",
        "SYBIL",
        "TRUDY",
        "VICTOR",
        "WALTER",
        "WENDY"
    };

    public static string GenerateHumanName()
    {
        return names[counter++ % names.Length];
    }

    public static void ResetCounter()
    {
        counter = 0;
    }
}

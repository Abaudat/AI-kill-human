public static class HumanNameGenerator
{
    private static int counter = 0;

    private static string[] names = new string[]
    {
        "BOB",
        "CHAD",
        "DAVID",
        "EVE",
        "FAYTH",
        "GRACE",
        "HEIDI",
        "IVAN",
        "JUDY",
        "MIKE",
        "NIAJ",
        "OLIVE",
        "PEGGY",
        "RUP",
        "SYBIL",
        "TRUDY",
        "VICT",
        "WALT",
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

using static System.Math;

public static class Util
{
    private static readonly System.Random Rand = new System.Random();

    public static double GetNextXp(ulong level)
    {
        return 250 + 250 * level;
    }

    public static double GetNextMasteryXp(int mastery, int masteryCutCnt)
    {
        return Pow(10, 9) * Pow(2, mastery) * Pow(0.9, masteryCutCnt);
    }

    public static double GetDamagePerHit(double tactics, double rage, double hardcoreness)
    {
        return tactics + Rand.NextDouble() * 3 * rage - 1000 * hardcoreness;
    }

    public static double GetExpPerHit(double neetery, double hardcoreness, double damage)
    {
        return damage * (neetery / (10 + hardcoreness));
    }
}

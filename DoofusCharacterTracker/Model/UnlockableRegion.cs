namespace DoofusCharacterTracker.Model;

public enum UnlockableRegion
{
    Frigost,
    MoonIsland,
    Ohwymi,
    OtomaiIsland,
    OtomaiBridgeOfDeath,
    Pandala,
    WabbitIsland,
}

public static class UnlockableRegionExtensions
{
    public static string GetDescription(this UnlockableRegion region) => region switch
    {
        UnlockableRegion.MoonIsland => "Moon Island",
        UnlockableRegion.WabbitIsland => "Wabbit Island",
        UnlockableRegion.Pandala => "Pandala",
        UnlockableRegion.Frigost => "Frigost",
        UnlockableRegion.OtomaiIsland => "Otomai Island",
        UnlockableRegion.OtomaiBridgeOfDeath => "Otomai Bridge of Death",
        UnlockableRegion.Ohwymi => "Ohwymi",
        _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
    };
}

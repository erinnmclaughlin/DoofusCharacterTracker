namespace DoofusCharacterTracker.Functions;

public static class DirectoryHelpers
{
    private static readonly string AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static readonly string GameDataDirectory = $"{AppDataDirectory}{Path.DirectorySeparatorChar}DoofusCharacterTracker";

    public static void EnsureDirectoryExists()
    {
        Directory.CreateDirectory(GameDataDirectory);
    }
}

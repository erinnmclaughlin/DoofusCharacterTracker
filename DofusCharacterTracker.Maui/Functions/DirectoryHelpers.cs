namespace DofusCharacterTracker.Maui.Functions;

public static class DirectoryHelpers
{
    private static readonly string AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static readonly string GameDataDirectory = $"{AppDataDirectory}{Path.DirectorySeparatorChar}DofusCharacterTracker";

    public static void EnsureDirectoryExists()
    {
        Directory.CreateDirectory(GameDataDirectory);
    }
}

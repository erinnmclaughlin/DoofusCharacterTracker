using DofusCharacterTracker.Database;
using DofusCharacterTracker.Functions;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		DirectoryHelpers.EnsureDirectoryExists();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

		builder.Services.AddDbContextFactory<Db>(options =>
		{
			options.UseSqlite($"Data Source={DirectoryHelpers.GameDataDirectory}{Path.DirectorySeparatorChar}DofusCharacterTracker.db;Cache=Shared");
		});

		return builder.Build();
	}
}

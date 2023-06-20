using DoofusCharacterTracker.Database;
using Microsoft.EntityFrameworkCore;

namespace DoofusCharacterTracker;

public partial class App : Application
{
	public App(IDbContextFactory<Db> dbFactory)
	{
		using (var database = dbFactory.CreateDbContext())
		{
			database.Database.Migrate();
		}

		InitializeComponent();

		MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = base.CreateWindow(activationState);

		window.Title = "Doofus Character Tracker";

		return window;
	}
}

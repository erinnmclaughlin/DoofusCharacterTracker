using DofusCharacterTracker.Maui.Database;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Maui.Pages.EditCharacter;

public sealed partial class AlmanaxProgress
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;

    [Parameter, EditorRequired] public required Guid CharacterId { get; set; }

    public AlmanaxProgressModel? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        Model = await db.Characters
            .Where(c => c.Id == CharacterId)
            .Select(c => new AlmanaxProgressModel()
            {
                Progress = c.AlmanaxProgress
            })
            .FirstOrDefaultAsync();

        if(Model == null)
            Navigator.NavigateTo("/");
    }

    public async Task DoSaveChanges()
    {
        if(Model == null)
            return;

        await using var db = await DbFactory.CreateDbContextAsync();

        var character = await db.Characters
            .Include(c => c.Elements)
            .FirstOrDefaultAsync(c => c.Id == CharacterId);

        if(character == null)
        {
            Navigator.NavigateTo("/");
            return;
        }

        character.AlmanaxProgress = Model.Progress;

        await db.SaveChangesAsync();

        Navigator.NavigateTo("/");
    }
}

public sealed class AlmanaxProgressModel
{
    public required Database.Tables.AlmanaxProgress Progress { get; set; }
}

using DoofusCharacterTracker.Database;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DoofusCharacterTracker.Pages.EditCharacter;

public sealed partial class DungeonNotes
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;

    [Parameter, EditorRequired] public required Guid DungeonId { get; set; }

    public DungeonNotesModel? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        Model = await db.DungeonNotes
            .Where(n => n.Id == DungeonId)
            .Select(n => new DungeonNotesModel()
            {
                Name = n.DungeonName,
                Notes = n.Notes
            })
            .FirstOrDefaultAsync();

        if(Model == null)
        {
            Navigator.NavigateTo("/");
            return;
        }
    }

    public async Task DoSaveChanges()
    {
        if(Model == null)
            return;

        await using var db = await DbFactory.CreateDbContextAsync();

        await db.DungeonNotes
            .Where(n => n.Id == DungeonId)
            .ExecuteUpdateAsync(n => n
                .SetProperty(p => p.DungeonName, Model.Name)
                .SetProperty(p => p.Notes, Model.Notes)
            );

        Navigator.NavigateTo("/");
    }
}

public sealed class DungeonNotesModel
{
    public required string Name { get; set; }
    public required string Notes { get; set; }
}

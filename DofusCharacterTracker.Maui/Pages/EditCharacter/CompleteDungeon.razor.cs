using DofusCharacterTracker.Maui.Database;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Maui.Pages.EditCharacter;

public sealed partial class CompleteDungeon
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;

    [Parameter, EditorRequired] public required Guid DungeonId { get; set; }

    public DungeonModel? OldDungeon { get; set; }

    public bool AddFollowUpDungeon { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        OldDungeon = await db.DungeonNotes
            .Where(d => d.Id == DungeonId)
            .Select(d => new DungeonModel(d.CharacterId, d.DungeonName, d.Notes))
            .FirstOrDefaultAsync();

        if(OldDungeon == null)
        {
            Navigator.NavigateTo("/");
            return;
        }
    }

    public async Task DoCompleteDungeon()
    {
        if(OldDungeon == null)
            return;

        await using var db = await DbFactory.CreateDbContextAsync();

        await db.DungeonNotes
            .Where(n => n.Id == DungeonId)
            .ExecuteDeleteAsync();

        if(AddFollowUpDungeon)
            Navigator.NavigateTo($"/edit-character/{OldDungeon.CharacterId}/add-dungeon");
        else
            Navigator.NavigateTo("/");
    }
}

public sealed record DungeonModel(Guid CharacterId, string Name, string Notes);

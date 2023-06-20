using DoofusCharacterTracker.Database;
using DoofusCharacterTracker.Database.Tables;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DoofusCharacterTracker.Pages.EditCharacter;

public sealed partial class AddDungeon
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;

    [Parameter, EditorRequired] public required Guid CharacterId { get; set; }

    public CharacterSummary? Character { get; set; }

    public NewDungeonModel Model { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        Character = await db.Characters
            .Where(c => c.Id == CharacterId)
            .Select(c => new CharacterSummary(c.Name, c.Class, c.Level))
            .FirstOrDefaultAsync();

        if(Character == null)
            Navigator.NavigateTo("/");
    }

    public async Task DoAddDungeon()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        db.DungeonNotes.Add(new()
        {
            CharacterId = CharacterId,
            DungeonName = Model.Name,
            Notes = Model.Notes
        });

        await db.SaveChangesAsync();

        Navigator.NavigateTo("/");
    }
}

public sealed record CharacterSummary(string Name, Class Class, int Level);

public sealed class NewDungeonModel
{
    public string Name { get; set; } = "";
    public string Notes { get; set; } = "";
}

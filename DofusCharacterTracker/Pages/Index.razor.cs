using DofusCharacterTracker.Database;
using DofusCharacterTracker.Database.Tables;
using DofusCharacterTracker.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Pages;

public sealed partial class Index
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;

    public List<CharacterModel>? AllCharacters { get; private set; }
    public List<CharacterModel> FilteredCharacters { get; private set; } = new();

    public CharacterOrder OrderBy { get; set; } = CharacterOrder.Name;
    public string FilterDungeon { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        await LoadCharacters();
    }

    private async Task LoadCharacters()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        AllCharacters = (await db.Characters
            .Include(c => c.Elements)
            .Include(c => c.DungeonNotes)
            .Include(c => c.ProfessionNotes)
            .ToListAsync())
            .Select(c =>
            {
                var elementsDescription = string.Join('/', c.Elements!.Select(e => e.Element.ToString()));
                var lockedRegions = new List<UnlockableRegion>();
                if(!c.UnlockedFrigost) lockedRegions.Add(UnlockableRegion.Frigost);
                if(!c.UnlockedMoonIsland) lockedRegions.Add(UnlockableRegion.MoonIsland);
                if(!c.UnlockedOtomaiIsland) lockedRegions.Add(UnlockableRegion.OtomaiIsland);
                if(!c.UnlockedOtomaiBridgeOfDeath) lockedRegions.Add(UnlockableRegion.OtomaiBridgeOfDeath);
                if(!c.UnlockedPandala) lockedRegions.Add(UnlockableRegion.Pandala);
                if(!c.UnlockedWabbitIsland) lockedRegions.Add(UnlockableRegion.WabbitIsland);
                if(!c.UnlockedOhwymi) lockedRegions.Add(UnlockableRegion.Ohwymi);

                return new CharacterModel(
                    c.Id,
                    c.Name,
                    c.Class,
                    c.Level,
                    elementsDescription,
                    c.AlmanaxProgress,
                    lockedRegions,
                    c.DungeonNotes!.Select(d => (d.Id, d.DungeonName, d.Notes)).ToList()
                );
            })
            .ToList();

        UpdateOrderBy(OrderBy);
    }

    public void UpdateOrderBy(CharacterOrder o)
    {
        OrderBy = o;
        FilterAndSort();
    }

    public void FilterAndSort()
    {
        if(AllCharacters == null) return;

        switch(OrderBy)
        {
            case CharacterOrder.Name:
                FilteredCharacters = AllCharacters.OrderBy(c => c.Name).ToList();
                break;

            case CharacterOrder.Level:
                FilteredCharacters = AllCharacters.OrderByDescending(c => c.Level).ToList();
                break;

            case CharacterOrder.AlmanaxProgress:
                FilteredCharacters = AllCharacters.OrderByDescending(c => c.AlmanaxProgress).ToList();
                break;
        }

        if(!string.IsNullOrWhiteSpace(FilterDungeon))
            FilteredCharacters = FilteredCharacters.Where(c => c.NeededDungeons.Any(d => d.Dungeon.Contains(FilterDungeon, StringComparison.OrdinalIgnoreCase))).ToList();
    }
}

public sealed record CharacterModel(
    Guid Id,
    string Name,
    Class Class,
    int Level,
    string Elements,
    AlmanaxProgress AlmanaxProgress,
    List<UnlockableRegion> LockedRegions,
    List<(Guid Id, string Dungeon, string Notes)> NeededDungeons
);

public enum CharacterOrder
{
    Name,
    Level,
    AlmanaxProgress
}
